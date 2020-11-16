using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetApi.MeetApi.Migrations
{
    public partial class NewDatabaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Dates",
                table => new
                {
                    DateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartingDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Dates", x => x.DateId); });

            migrationBuilder.CreateTable(
                "Issues",
                table => new
                {
                    IssueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Issues", x => x.IssueId); });

            migrationBuilder.CreateTable(
                "People",
                table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_People", x => x.PersonId); });

            migrationBuilder.CreateTable(
                "Reasons",
                table => new
                {
                    ReasonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Reasons", x => x.ReasonId); });

            migrationBuilder.CreateTable(
                "Meetings",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateId = table.Column<int>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    IssueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        "FK_Meetings_Dates_DateId",
                        x => x.DateId,
                        "Dates",
                        "DateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Meetings_Issues_IssueId",
                        x => x.IssueId,
                        "Issues",
                        "IssueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Meetings_People_PersonId",
                        x => x.PersonId,
                        "People",
                        "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    PersonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Login);
                    table.ForeignKey(
                        "FK_Users_People_PersonId",
                        x => x.PersonId,
                        "People",
                        "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Meetings_DateId",
                "Meetings",
                "DateId");

            migrationBuilder.CreateIndex(
                "IX_Meetings_IssueId",
                "Meetings",
                "IssueId");

            migrationBuilder.CreateIndex(
                "IX_Meetings_PersonId",
                "Meetings",
                "PersonId");

            migrationBuilder.CreateIndex(
                "IX_Users_PersonId",
                "Users",
                "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Meetings");

            migrationBuilder.DropTable(
                "Reasons");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Dates");

            migrationBuilder.DropTable(
                "Issues");

            migrationBuilder.DropTable(
                "People");
        }
    }
}