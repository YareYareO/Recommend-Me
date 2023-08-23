using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecMe.Migrations
{
    /// <inheritdoc />
    public partial class ThingsHaveAUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Thing",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Thing");
        }
    }
}
