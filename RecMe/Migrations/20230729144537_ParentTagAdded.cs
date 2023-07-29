using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecMe.Migrations
{
    /// <inheritdoc />
    public partial class ParentTagAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Tag",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Tag");
        }
    }
}
