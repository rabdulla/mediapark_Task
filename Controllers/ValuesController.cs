using System;
using AutoMapper;
using CountryHolidays_API.Entities;
using CountryHolidays_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IHolidayService _holidayService;
        private readonly CountryHolidaysContext _ctx;
        private readonly IMapper _mapper;

        public ValuesController(ILogger<ValuesController> logger, IHolidayService holidayService, CountryHolidaysContext ctx, IMapper mapper)
        {
            _logger = logger;
            _holidayService = holidayService;
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet("GetHolidaysForYear/{countryCode}/{year:int}")]
        public async Task<IActionResult> GetHolidaysForYear(int year, string countryCode)
        {
            List<IGrouping<int, Holiday>> responce = null;

            _ctx.Holidays
                .Include(x => x.Country)
                .Include(x => x.Date);
            if (_ctx.Holidays.Any(x => x.Country.CountryCode == countryCode && x.Date.Year == year))
            {
                responce = _ctx.Holidays
                    .Where(x => x.Country.CountryCode == countryCode && x.Date.Year == year).GroupBy(x => x.Date.Month)
                    .ToList();
            }
            else
            {
                try
                {
                    var item = await _holidayService.GetHolidaysForYear(year, countryCode, "public_holiday");

                    responce = _mapper.Map<List<Entities.Holiday>>(item).GroupBy(x => x.Date.Month).ToList();
                    _ctx.Holidays.AddRange((IEnumerable<Holiday>)responce);
                    await _ctx.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Ok(responce);
        }

        [HttpGet("GetAllCountries")]
        public async Task<IActionResult> GetSupportedCountries()
        {
            List<Country> responce = null;
            if (_ctx.Countries.Any())
            {
                responce = _ctx.Countries.Where(x => !string.IsNullOrEmpty(x.FullName)).ToList();
            }
            else
            {
                var item = await _holidayService.GetSupportedCountries();
                responce = _mapper.Map<List<Entities.Country>>(item);
                _ctx.Countries.AddRange(responce);
                await _ctx.SaveChangesAsync();
            }

            return Ok(responce);
        }

        [HttpGet("GetPublicHoliday/{day:int}/{month:int}/{year:int}")]
        public async Task<IActionResult> GetPublicHoliday(int day, int month, int year)
        {
            List<Country> responce = null;

            try
            {
                var item = await _holidayService.GetPublicHoliday(day, month, year);
                responce = _mapper.Map<List<Entities.Country>>(item);
                _ctx.Countries.AddRange(responce);
                await _ctx.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            //var obj = item.ToList();

            return Ok(responce);


        }
    }
}
