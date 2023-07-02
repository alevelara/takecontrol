using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Takecontrol.Matches.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingDefaultValueForIsAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "reservations",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "reservations",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
