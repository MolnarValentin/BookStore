using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KönyvtárAlkalmazás.Migrations
{
    public partial class fixKonyv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Előrendelték",
                table: "Könyvek",
                newName: "Előkölcsönözték");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Előkölcsönözték",
                table: "Könyvek",
                newName: "Előrendelték");
        }
    }
}
