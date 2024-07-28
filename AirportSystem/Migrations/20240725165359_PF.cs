using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportSystem.Migrations
{
    /// <inheritdoc />
    public partial class PF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilotFlight_AspNetUsers_PilotUserId",
                table: "PilotFlight");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotFlight_Flights_FlightId",
                table: "PilotFlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PilotFlight",
                table: "PilotFlight");

            migrationBuilder.RenameTable(
                name: "PilotFlight",
                newName: "PilotFlights");

            migrationBuilder.RenameIndex(
                name: "IX_PilotFlight_PilotUserId",
                table: "PilotFlights",
                newName: "IX_PilotFlights_PilotUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PilotFlight_FlightId",
                table: "PilotFlights",
                newName: "IX_PilotFlights_FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PilotFlights",
                table: "PilotFlights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PilotFlights_AspNetUsers_PilotUserId",
                table: "PilotFlights",
                column: "PilotUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PilotFlights_Flights_FlightId",
                table: "PilotFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilotFlights_AspNetUsers_PilotUserId",
                table: "PilotFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotFlights_Flights_FlightId",
                table: "PilotFlights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PilotFlights",
                table: "PilotFlights");

            migrationBuilder.RenameTable(
                name: "PilotFlights",
                newName: "PilotFlight");

            migrationBuilder.RenameIndex(
                name: "IX_PilotFlights_PilotUserId",
                table: "PilotFlight",
                newName: "IX_PilotFlight_PilotUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PilotFlights_FlightId",
                table: "PilotFlight",
                newName: "IX_PilotFlight_FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PilotFlight",
                table: "PilotFlight",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PilotFlight_AspNetUsers_PilotUserId",
                table: "PilotFlight",
                column: "PilotUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PilotFlight_Flights_FlightId",
                table: "PilotFlight",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
