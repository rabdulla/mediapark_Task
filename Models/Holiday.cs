using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays_API.Models
{
    public class Holiday
    {
        public Date Date { get; set; }
        public List<HolidayName> Names { get; set; }
        public HolidayType Type { get; set; }

    }
}
