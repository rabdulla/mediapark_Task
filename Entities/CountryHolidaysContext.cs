using Microsoft.EntityFrameworkCore;

namespace CountryHolidays_API.Entities
{
    public class CountryHolidaysContext : DbContext
    {
        public CountryHolidaysContext(DbContextOptions<CountryHolidaysContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Date> FromToDate { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
    }
}
