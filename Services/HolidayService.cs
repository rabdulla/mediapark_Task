using CountryHolidays_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CountryHolidays_API.Services
{
    public interface IHolidayService
    {
        Task<List<IGrouping<int, Holiday>>> GetHolidaysForYear(int year, string country, string holidayType);
        Task<List<Country>> GetSupportedCountries();
        Task<List<Country>> GetPublicHoliday(int day, int month, int year);

    }
    public class HolidayService : IHolidayService
    {
        private readonly HttpClient _httpClient;

        public HolidayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IGrouping<int, Holiday>>> GetHolidaysForYear(int year, string country,
            string holidayType)
        {
            var url = new Uri($"{_httpClient.BaseAddress}getHolidaysForYear&year={year}&country={country}&holidayType={holidayType}");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<List<Holiday>>(content);

            var groupByMonth= obj.GroupBy(x => x.Date.Month).ToList();

            return groupByMonth;
        }

        public async Task<List<Country>> GetSupportedCountries()
        {
            var url = new Uri($"{_httpClient.BaseAddress}getSupportedCountries");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<List<Country>>(content);

            return obj;
        }

        public async Task<List<Country>> GetPublicHoliday(int day, int month, int year)
        {
            List<Country> obj = null;
            var url = new Uri($"{_httpClient.BaseAddress}whereIsPublicHoliday&date={day}-{month}-{year}");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<List<Country>>(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return obj;

        }
    }
}
