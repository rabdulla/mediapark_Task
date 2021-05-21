using CountryHolidays_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CountryHolidays_API.Services
{
    public interface IHolidayService
    {
        Task<List<Holiday>> GetHolidaysForYear(int year, string country, string holidayType);
        Task<List<Country>> GetSupportedCountries();
        Task<Holiday> GetPublicHoliday(int day, int month, int year, string countryCode);

    }
    public class HolidayService : IHolidayService
    {
        private readonly HttpClient _httpClient;

        public HolidayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<Holiday>> GetHolidaysForYear(int year, string countryCode,
            string holidayType)
        {
            var url = new Uri($"{_httpClient.BaseAddress}getHolidaysForYear&year={year}&country={countryCode}&holidayType={holidayType}");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<List<Holiday>>(content);

            return obj;
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

        public async Task<Holiday> GetPublicHoliday(int day, int month, int year, string countryCode)
        {
            Holiday obj ;
            var url = new Uri($"{_httpClient.BaseAddress}isPublicHoliday&date={day}-{month}-{year}&country={countryCode}");

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<Holiday>(content);
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
