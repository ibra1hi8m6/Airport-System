using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportSystem.Migrations
{
    /// <inheritdoc />
    public partial class addDoctor1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Flights",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DoctorId",
                table: "Flights",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_AspNetUsers_DoctorId",
                table: "Flights",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_AspNetUsers_DoctorId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DoctorId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Flights");
        }
    }
}
