using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CountryHolidays_API.Entities
{
    public class HolidaysForYear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string HolidayName { get; set; }
        public string HolidayType { get; set; }
        public string CountryCode { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
