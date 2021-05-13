using Microsoft.EntityFrameworkCore.Migrations;

namespace CountryHolidays_API.Migrations
{
    public partial class Added_CountryFullname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Countries",
                newName: "FullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Countries",
                newName: "Name");
        }
    }
}
