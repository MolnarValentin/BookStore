using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KönyvtárAlkalmazás.Migrations
{
    public partial class fix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Állapot",
                table: "Könyvek");

            migrationBuilder.AddColumn<bool>(
                name: "Elérhető",
                table: "Könyvek",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elérhető",
                table: "Könyvek");

            migrationBuilder.AddColumn<int>(
                name: "Állapot",
                table: "Könyvek",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
