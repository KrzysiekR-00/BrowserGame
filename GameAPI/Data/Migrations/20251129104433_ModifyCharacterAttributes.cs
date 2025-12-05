using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCharacterAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributesCsv",
                table: "CharactersAttributes");

            migrationBuilder.AddColumn<int>(
                name: "AttributeId",
                table: "CharactersAttributes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttributeValue",
                table: "CharactersAttributes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "CharactersAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeValue",
                table: "CharactersAttributes");

            migrationBuilder.AddColumn<string>(
                name: "AttributesCsv",
                table: "CharactersAttributes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
