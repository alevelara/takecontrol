using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Takecontrol.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingNumberOfCourts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfCourts",
                table: "clubs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfCourts",
                table: "clubs");
        }
    }
}
