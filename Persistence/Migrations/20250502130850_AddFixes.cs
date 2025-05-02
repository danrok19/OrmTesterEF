using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_account_details_account_details_id",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "level_progression",
                table: "characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_users_account_details_account_details_id",
                table: "users",
                column: "account_details_id",
                principalTable: "account_details",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_account_details_account_details_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "level_progression",
                table: "characters");

            migrationBuilder.AddForeignKey(
                name: "FK_users_account_details_account_details_id",
                table: "users",
                column: "account_details_id",
                principalTable: "account_details",
                principalColumn: "id");
        }
    }
}
