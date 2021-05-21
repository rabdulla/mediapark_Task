using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CountryHolidays_API.Models
{
    public class Holiday
    {
        [JsonProperty("name")]
        public List<HolidayName> HolidayName { get; set; }
        public string HolidayType { get; set; }
        public bool IsPublicHoliday { get; set; }
        public Date Date { get; set; }
        public string CountryCode { get; set; }
        [JsonProperty("dayOfWeek")]
        public DayOfWeek DayOfWeek { get; set; }

    }
}
