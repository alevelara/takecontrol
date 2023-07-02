using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Takecontrol.Matches.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsAvailableToReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "reservations");
        }
    }
}
