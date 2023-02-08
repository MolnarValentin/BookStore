using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KönyvtárAlkalmazás.Migrations
{
    public partial class databaseFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Név",
                table: "Felhasználók",
                newName: "Vezetéknév");

            migrationBuilder.AddColumn<string>(
                name: "Felhasználónév",
                table: "Felhasználók",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Keresztnév",
                table: "Felhasználók",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Felhasználónév",
                table: "Felhasználók");

            migrationBuilder.DropColumn(
                name: "Keresztnév",
                table: "Felhasználók");

            migrationBuilder.RenameColumn(
                name: "Vezetéknév",
                table: "Felhasználók",
                newName: "Név");
        }
    }
}
