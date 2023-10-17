using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Takecontrol.Matches.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCancelledDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelledDescription",
                table: "matches",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelledDescription",
                table: "matches");
        }
    }
}
