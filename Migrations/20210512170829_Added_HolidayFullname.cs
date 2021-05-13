using Microsoft.EntityFrameworkCore.Migrations;

namespace CountryHolidays_API.Migrations
{
    public partial class Added_HolidayFullname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HolidayName",
                table: "Countries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayName",
                table: "Countries");
        }
    }
}
