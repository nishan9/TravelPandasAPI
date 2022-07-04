using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPandasAPI.Migrations
{
    public partial class addressfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Town",
                table: "Users",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "Town");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
