using Newtonsoft.Json;

namespace CountryHolidays_API.Models
{
    public class HolidayName
    {
        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
