using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCoreAngular.Migrations
{
    public partial class _14_id_angular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Id",
                table: "AspNetUsers",
                newName: "IdAngular");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdAngular",
                table: "AspNetUsers",
                newName: "_Id");
        }
    }
}
