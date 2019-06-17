using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Web.Data.Migrations
{
    public partial class specifydatetimezone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Bugs",
                newName: "CreatedUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedUtc",
                table: "Bugs",
                newName: "Created");
        }
    }
}
