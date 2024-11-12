using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportSystem.Migrations
{
    /// <inheritdoc />
    public partial class addplane : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "number_of_seats",
                table: "Planes",
                newName: "seats_Economy");

            migrationBuilder.AddColumn<int>(
                name: "seats_Budiness",
                table: "Planes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seats_Budiness",
                table: "Planes");

            migrationBuilder.RenameColumn(
                name: "seats_Economy",
                table: "Planes",
                newName: "number_of_seats");
        }
    }
}
