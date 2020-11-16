using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetApi.MeetApi.Migrations
{
    public partial class HashedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Salt",
                "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Salt",
                "Users");
        }
    }
}