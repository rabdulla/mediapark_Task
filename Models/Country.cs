using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays_API.Models
{
    public class Country
    {
        public string CountryFullName { get; set; }
        public string FullName { get; set; }
        public string CountryCode { get; set; }
        public Date FromDate { get; set; }
        public Date ToDate { get; set; }
        public Holiday Holiday { get; set; }
        public List<string> Regions { get; set; }
        public List<string> HolidayName { get; set; }

    }
}
