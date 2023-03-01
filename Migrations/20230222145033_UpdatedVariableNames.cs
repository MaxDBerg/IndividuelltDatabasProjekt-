using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3._1Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVariableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Personnummer",
                table: "Students",
                newName: "PersonNumber");

            migrationBuilder.RenameColumn(
                name: "Namn",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Efternamn",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Personnum",
                table: "Personals",
                newName: "PersonNumber");

            migrationBuilder.RenameColumn(
                name: "Namn",
                table: "Personals",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Namn",
                table: "Kurses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Namn",
                table: "Klasses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Datum",
                table: "Betygs",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Personals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Personals");

            migrationBuilder.RenameColumn(
                name: "PersonNumber",
                table: "Students",
                newName: "Personnummer");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "Namn");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "Efternamn");

            migrationBuilder.RenameColumn(
                name: "PersonNumber",
                table: "Personals",
                newName: "Personnum");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Personals",
                newName: "Namn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Kurses",
                newName: "Namn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Klasses",
                newName: "Namn");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Betygs",
                newName: "Datum");
        }
    }
}
