using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Website_BE.Migrations
{
    public partial class addedCV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ContactInfo");

            migrationBuilder.AddColumn<string>(
                name: "City_EN",
                table: "ContactInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City_FR",
                table: "ContactInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_EN",
                table: "ContactInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country_FR",
                table: "ContactInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City_EN",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "City_FR",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "Country_EN",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "Country_FR",
                table: "ContactInfo");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ContactInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ContactInfo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
