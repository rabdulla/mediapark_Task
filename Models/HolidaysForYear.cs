using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays_API.Models
{
    public class HolidaysForYear
    {
        public Date Date { get; set; }
        public HolidayName HolidayName {get; set; }
        public string HolidayType { get; set; }

    }
}
