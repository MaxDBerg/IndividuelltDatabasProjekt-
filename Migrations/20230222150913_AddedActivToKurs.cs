using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3._1Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedActivToKurs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activ",
                table: "Kurses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activ",
                table: "Kurses");
        }
    }
}
