using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportSystem.Migrations
{
    /// <inheritdoc />
    public partial class managefk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Flights_FlightId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FlightId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "PilotFlight",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PilotUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PilotFlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PilotFlight_AspNetUsers_PilotUserId",
                        column: x => x.PilotUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PilotFlight_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PilotFlight_FlightId",
                table: "PilotFlight",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_PilotFlight_PilotUserId",
                table: "PilotFlight",
                column: "PilotUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PilotFlight");

            migrationBuilder.AddColumn<Guid>(
                name: "FlightId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FlightId",
                table: "AspNetUsers",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Flights_FlightId",
                table: "AspNetUsers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
