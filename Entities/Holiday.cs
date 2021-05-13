using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryHolidays_API.Entities
{
    public class Holiday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Date Date { get; set; }
        public HolidayType Type { get; set; }
        public Country Country { get; set; }
        //[ForeignKey("")]
        //public int CountryId { get; set; }
        public string Name { get; set; }

    }
}
