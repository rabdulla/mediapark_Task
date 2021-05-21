using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CountryHolidays_API.Models
{
    public class Country
    {
        public string FullName { get; set; }
        public string CountryCode { get; set; }
        public Date FromDate { get; set; }
        public Date ToDate { get; set; }
        public List<string> HolidayTypes { get; set; }

    }
}
