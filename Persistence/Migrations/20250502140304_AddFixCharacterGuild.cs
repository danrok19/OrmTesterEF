using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFixCharacterGuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_guild_guild_id",
                table: "characters");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_guild_guild_id",
                table: "characters",
                column: "guild_id",
                principalTable: "guild",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_guild_guild_id",
                table: "characters");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_guild_guild_id",
                table: "characters",
                column: "guild_id",
                principalTable: "guild",
                principalColumn: "id");
        }
    }
}
