using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Takecontrol.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingOpeningTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "ClosureDate",
                table: "clubs",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "OpenDate",
                table: "clubs",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosureDate",
                table: "clubs");

            migrationBuilder.DropColumn(
                name: "OpenDate",
                table: "clubs");
        }
    }
}
