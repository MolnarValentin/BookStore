using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KönyvtárAlkalmazás.Migrations
{
    public partial class fixKonyv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Elérhető",
                table: "Könyvek",
                newName: "Kikölcsönözték");

            migrationBuilder.AddColumn<bool>(
                name: "Előrendelték",
                table: "Könyvek",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Előrendelték",
                table: "Könyvek");

            migrationBuilder.RenameColumn(
                name: "Kikölcsönözték",
                table: "Könyvek",
                newName: "Elérhető");
        }
    }
}
