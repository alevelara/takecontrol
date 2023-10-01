using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Takecontrol.Matches.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CancellableMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "matches");
        }
    }
}
