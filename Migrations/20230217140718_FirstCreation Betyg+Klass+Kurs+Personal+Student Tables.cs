using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3._1Database.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreationBetygKlassKursPersonalStudentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klasses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kurses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Personals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Personnum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Befattning = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Efternamn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Personnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KlassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_Klasses_KlassID",
                        column: x => x.KlassID,
                        principalTable: "Klasses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Betygs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    PersonalID = table.Column<int>(type: "int", nullable: false),
                    KursID = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Betygbetyg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Betygs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Betygs_Kurses_KursID",
                        column: x => x.KursID,
                        principalTable: "Kurses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Betygs_Personals_PersonalID",
                        column: x => x.PersonalID,
                        principalTable: "Personals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Betygs_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Betygs_KursID",
                table: "Betygs",
                column: "KursID");

            migrationBuilder.CreateIndex(
                name: "IX_Betygs_PersonalID",
                table: "Betygs",
                column: "PersonalID");

            migrationBuilder.CreateIndex(
                name: "IX_Betygs_StudentID",
                table: "Betygs",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_KlassID",
                table: "Students",
                column: "KlassID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Betygs");

            migrationBuilder.DropTable(
                name: "Kurses");

            migrationBuilder.DropTable(
                name: "Personals");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Klasses");
        }
    }
}
