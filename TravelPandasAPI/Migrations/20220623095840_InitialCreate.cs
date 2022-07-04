using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPandasAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Driver_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Career_Stage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auth0_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDriver = table.Column<bool>(type: "bit", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Radius = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutboundTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InboundTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    BookingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Bookings_BookingId1",
                        column: x => x.BookingId1,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BookingId",
                table: "Users",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BookingId1",
                table: "Users",
                column: "BookingId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
