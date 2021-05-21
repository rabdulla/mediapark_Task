using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CountryHolidays_API.Models
{
    public class HolidayName
    {
        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
