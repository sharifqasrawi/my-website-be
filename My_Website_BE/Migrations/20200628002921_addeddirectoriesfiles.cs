using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Website_BE.Migrations
{
    public partial class addeddirectoriesfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DownloadPath = table.Column<string>(nullable: true),
                    UploadPath = table.Column<string>(nullable: true),
                    OriginalFileName = table.Column<string>(nullable: true),
                    ModifiedFileName = table.Column<string>(nullable: true),
                    UploadDateTime = table.Column<DateTime>(nullable: false),
                    UploadDirectoryId = table.Column<int>(nullable: true),
                    FileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Directories_UploadDirectoryId",
                        column: x => x.UploadDirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_UploadDirectoryId",
                table: "UploadedFiles",
                column: "UploadDirectoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "Directories");
        }
    }
}
