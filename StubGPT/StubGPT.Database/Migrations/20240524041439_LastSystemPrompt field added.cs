using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StubGPT.Database.Migrations
{
    /// <inheritdoc />
    public partial class LastSystemPromptfieldadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastSystemPrompt",
                table: "UserConfiguration",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSystemPrompt",
                table: "UserConfiguration");
        }
    }
}
