using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCharacterAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CharactersAttributes",
                newName: "CharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "CharactersAttributes",
                newName: "UserId");
        }
    }
}
