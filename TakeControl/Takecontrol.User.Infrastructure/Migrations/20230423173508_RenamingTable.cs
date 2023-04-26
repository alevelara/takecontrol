using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Takecontrol.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerClubs_clubs_ClubId",
                table: "PlayerClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerClubs_players_PlayerId",
                table: "PlayerClubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerClubs",
                table: "PlayerClubs");

            migrationBuilder.RenameTable(
                name: "PlayerClubs",
                newName: "players_clubs");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerClubs_PlayerId",
                table: "players_clubs",
                newName: "IX_players_clubs_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_players_clubs",
                table: "players_clubs",
                columns: new[] { "ClubId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_players_clubs_clubs_ClubId",
                table: "players_clubs",
                column: "ClubId",
                principalTable: "clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_players_clubs_players_PlayerId",
                table: "players_clubs",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_players_clubs_clubs_ClubId",
                table: "players_clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_players_clubs_players_PlayerId",
                table: "players_clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_players_clubs",
                table: "players_clubs");

            migrationBuilder.RenameTable(
                name: "players_clubs",
                newName: "PlayerClubs");

            migrationBuilder.RenameIndex(
                name: "IX_players_clubs_PlayerId",
                table: "PlayerClubs",
                newName: "IX_PlayerClubs_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerClubs",
                table: "PlayerClubs",
                columns: new[] { "ClubId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerClubs_clubs_ClubId",
                table: "PlayerClubs",
                column: "ClubId",
                principalTable: "clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerClubs_players_PlayerId",
                table: "PlayerClubs",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
