using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KönyvtárAlkalmazás.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Felhasználók",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Név = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jelszó = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Felhasználók", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Könyvek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cím = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Író = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kiadó = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Állapot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Könyvek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Előkölcsönzések",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KezdetiDátum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LejáratiDátum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KölcsönzőEmailCíme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KölcsönzőTelefonszáma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KönyvId = table.Column<int>(type: "int", nullable: false),
                    AzonosítóKód = table.Column<int>(type: "int", nullable: false),
                    FelhasználóId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Előkölcsönzések", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Előkölcsönzések_Felhasználók_FelhasználóId",
                        column: x => x.FelhasználóId,
                        principalTable: "Felhasználók",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Előkölcsönzések_Könyvek_KönyvId",
                        column: x => x.KönyvId,
                        principalTable: "Könyvek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kölcsönzések",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LejáratiDátum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KölcsönzőEmailCíme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KölcsönzőTelefonszáma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KönyvId = table.Column<int>(type: "int", nullable: false),
                    FelhasználóId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kölcsönzések", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kölcsönzések_Felhasználók_FelhasználóId",
                        column: x => x.FelhasználóId,
                        principalTable: "Felhasználók",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kölcsönzések_Könyvek_KönyvId",
                        column: x => x.KönyvId,
                        principalTable: "Könyvek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Előkölcsönzések_FelhasználóId",
                table: "Előkölcsönzések",
                column: "FelhasználóId");

            migrationBuilder.CreateIndex(
                name: "IX_Előkölcsönzések_KönyvId",
                table: "Előkölcsönzések",
                column: "KönyvId");

            migrationBuilder.CreateIndex(
                name: "IX_Kölcsönzések_FelhasználóId",
                table: "Kölcsönzések",
                column: "FelhasználóId");

            migrationBuilder.CreateIndex(
                name: "IX_Kölcsönzések_KönyvId",
                table: "Kölcsönzések",
                column: "KönyvId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Előkölcsönzések");

            migrationBuilder.DropTable(
                name: "Kölcsönzések");

            migrationBuilder.DropTable(
                name: "Felhasználók");

            migrationBuilder.DropTable(
                name: "Könyvek");
        }
    }
}
