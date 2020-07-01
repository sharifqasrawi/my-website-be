using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Website_BE.Migrations
{
    public partial class addedCV8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Documents");
        }
    }
}
