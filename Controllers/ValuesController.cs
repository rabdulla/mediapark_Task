using CountryHolidays_API.Entities;
using CountryHolidays_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateTime = System.DateTime;
using DayOfWeek = CountryHolidays_API.Entities.DayOfWeek;

namespace CountryHolidays_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IHolidayService _holidayService;
        private readonly CountryHolidaysContext _ctx;

        public ValuesController(ILogger<ValuesController> logger, IHolidayService holidayService, CountryHolidaysContext ctx)
        {
            _logger = logger;
            _holidayService = holidayService;
            _ctx = ctx;
        }

        [HttpGet("GetHolidaysForYear")]
        public async Task<IActionResult> GetHolidaysForYear(int year, string countryCode)
        {
            var response = new List<HolidaysForYear>();

            if (_ctx.HolidaysForYear.Any(x => x.CountryCode == countryCode && x.Date.Year == year))
            {
                response = _ctx.HolidaysForYear.Where(x => x.CountryCode == countryCode && x.Date.Year == year)
                    .ToList();
            }

            else
            {
                var holidayList = await _holidayService.GetHolidaysForYear(year, countryCode, "public_holiday");

                foreach (var item in holidayList)
                {
                    response.Add(new HolidaysForYear
                    {
                        Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day),
                        HolidayName = string.Join(", ", item.HolidayName.Select(x => x.Text)),
                        HolidayType = item.HolidayType,
                        CountryCode = countryCode,
                        DayOfWeek = (DayOfWeek)item.Date.DayOfWeek

                    });
                }

                _ctx.HolidaysForYear.AddRange(response);
                await _ctx.SaveChangesAsync();
            }

            var resultGroupedByMonth = response.GroupBy(x => x.Date.Month)
                .Select(x => new { Month = x.Key, Holiday = x.ToList() })
                .ToList();
            return Ok(resultGroupedByMonth);
        }

        [HttpGet("MaximumNumberOfFreeDays")]
        public async Task<IActionResult> MaximumNumberOfFreeDays(int year, string countryCode)
        {
            var response = new List<HolidaysForYear>();

            var holidayList = await _holidayService.GetHolidaysForYear(year, countryCode, "public_holiday");

            foreach (var item in holidayList)
            {
                response.Add(new HolidaysForYear
                {
                    Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day),
                    HolidayName = string.Join(", ", item.HolidayName.Select(x => x.Text)),
                    HolidayType = item.HolidayType,
                    CountryCode = item.CountryCode,
                    DayOfWeek = (DayOfWeek)item.Date.DayOfWeek

                });
            }

            var countFreeDays = 0;
            var maxFreeDays = 0;
            var rangesOfFreeDays = new DateTime();
            var expectedDay = new DateTime();
            var previousDay = new DateTime();

            foreach (var item in response)
            {

                var currentDay = item.Date;

                if (expectedDay.Equals(item.Date))
                {
                    if (item.DayOfWeek == (DayOfWeek)5)
                    {
                        countFreeDays += 3;
                        expectedDay.AddDays(3);
                        previousDay = currentDay;

                        continue;
                    }
                    countFreeDays += 1;
                    expectedDay.AddDays(1);
                    continue;
                }

                if (countFreeDays > 0)
                {
                    if (countFreeDays > maxFreeDays)
                    {
                        maxFreeDays = countFreeDays;
                        rangesOfFreeDays = previousDay;
                    }
                    countFreeDays = 0;
                    expectedDay = new DateTime();
                }

                if (item.DayOfWeek == (DayOfWeek)2 || item.DayOfWeek == (DayOfWeek)3 || item.DayOfWeek == (DayOfWeek)4)
                {
                    countFreeDays += 1;
                    expectedDay = currentDay.AddDays(1);
                    previousDay = currentDay;
                }

                else if (item.DayOfWeek == (DayOfWeek)1)
                {
                    countFreeDays += 3;
                    expectedDay = currentDay.AddDays(1);
                    previousDay = currentDay;

                }

                else if (item.DayOfWeek == (DayOfWeek)5)
                {
                    countFreeDays += 3;
                    expectedDay = currentDay.AddDays(3);
                    previousDay = currentDay;

                }
            }

            if (countFreeDays > maxFreeDays)
            {
                maxFreeDays = countFreeDays;
                rangesOfFreeDays = previousDay;
            }

            var result = $"{rangesOfFreeDays.ToString("MM/dd/yyyy")} - Number of free days: {maxFreeDays}";

            return Ok(result);
        }

        [HttpGet("GetAllCountries")]
        public async Task<IActionResult> GetSupportedCountries()
        {
            var response = new List<Country>();

            if (_ctx.Countries.Any())
            {
                response = _ctx.Countries.Where(x => !string.IsNullOrEmpty(x.FullName)).ToList();
            }
            else
            {
                var listCountries = await _holidayService.GetSupportedCountries();
                foreach (var item in listCountries)
                {
                    response.Add(new Country
                    {
                        FullName = item.FullName,
                        CountryCode = item.CountryCode,
                        HolidayType = string.Join(", ", item.HolidayTypes),
                        FromDate = new DateTime(item.FromDate.Year, item.FromDate.Month, item.FromDate.Day),
                        ToDate = new DateTime(2999, item.ToDate.Month, item.ToDate.Day)
                    });

                }

                _ctx.Countries.AddRange(response);
                await _ctx.SaveChangesAsync();
            }

            return Ok(response);
        }

        [HttpGet("isPublicHoliday")]
        public async Task<IActionResult> GetPublicHoliday(int day, int month, int year, string countryCode)
        {
            var response = new Holiday();

            if (_ctx.Holidays.Any(x => x.CountryCode == countryCode && x.DateTime.Equals(new DateTime(year, month, day))))
            {
                response = _ctx.Holidays.SingleOrDefault(x =>
                    x.CountryCode == countryCode && x.DateTime.Equals(new DateTime(year, month, day)));
            }

            else
            {
                var item = await _holidayService.GetPublicHoliday(day, month, year, countryCode);

                response.IsPublicHoliday = item.IsPublicHoliday;
                response.DateTime = new DateTime(year, month, day);
                response.CountryCode = countryCode;

                _ctx.Holidays.AddRange(response);
                await _ctx.SaveChangesAsync();
            }

            var obj = response?.IsPublicHoliday;

            return Ok(obj);

        }
    }
}
