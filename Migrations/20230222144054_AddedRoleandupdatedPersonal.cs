using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3._1Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleandupdatedPersonal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Befattning",
                table: "Personals");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEmployed",
                table: "Personals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "Personals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Personals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personals_RoleID",
                table: "Personals",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Personals_Roles_RoleID",
                table: "Personals",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personals_Roles_RoleID",
                table: "Personals");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Personals_RoleID",
                table: "Personals");

            migrationBuilder.DropColumn(
                name: "DateEmployed",
                table: "Personals");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Personals");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Personals");

            migrationBuilder.AddColumn<string>(
                name: "Befattning",
                table: "Personals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
