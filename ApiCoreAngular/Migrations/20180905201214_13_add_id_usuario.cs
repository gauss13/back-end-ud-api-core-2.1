using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCoreAngular.Migrations
{
    public partial class _13_add_id_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_Id",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_Id",
                table: "AspNetUsers");
        }
    }
}
