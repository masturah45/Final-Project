using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Final_Project.Migrations
{
    public partial class counsel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuesId",
                table: "TherapistIssues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IssuesId",
                table: "TherapistIssues",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
