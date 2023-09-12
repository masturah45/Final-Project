using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Final_Project.Migrations
{
    public partial class supi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Clients_ClientId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ClientId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "TherapistId",
                table: "Chats",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Chats",
                newName: "RecieverId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Therapists",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TherapistIssues",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SuperAdmins",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Issues",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Chats",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Bookings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TherapistIssues");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SuperAdmins");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Chats",
                newName: "TherapistId");

            migrationBuilder.RenameColumn(
                name: "RecieverId",
                table: "Chats",
                newName: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ClientId",
                table: "Chats",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Clients_ClientId",
                table: "Chats",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
