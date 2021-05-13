using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CountryHolidays_API.Models;

namespace CountryHolidays_API.Entities
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(25)]
        public string CountryCode { get; set; }
        public Date FromDate { get; set; }
        public Date ToDate { get; set; }
        public string HolidayName { get; set; }

    }
}
