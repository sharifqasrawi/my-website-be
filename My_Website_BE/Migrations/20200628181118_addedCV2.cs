using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Website_BE.Migrations
{
    public partial class addedCV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetLanguageDocuments_Languages_LanguageId",
                table: "GetLanguageDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetLanguageDocuments",
                table: "GetLanguageDocuments");

            migrationBuilder.RenameTable(
                name: "GetLanguageDocuments",
                newName: "LanguageDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_GetLanguageDocuments_LanguageId",
                table: "LanguageDocuments",
                newName: "IX_LanguageDocuments_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageDocuments",
                table: "LanguageDocuments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageDocuments_Languages_LanguageId",
                table: "LanguageDocuments",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageDocuments_Languages_LanguageId",
                table: "LanguageDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageDocuments",
                table: "LanguageDocuments");

            migrationBuilder.RenameTable(
                name: "LanguageDocuments",
                newName: "GetLanguageDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageDocuments_LanguageId",
                table: "GetLanguageDocuments",
                newName: "IX_GetLanguageDocuments_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetLanguageDocuments",
                table: "GetLanguageDocuments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GetLanguageDocuments_Languages_LanguageId",
                table: "GetLanguageDocuments",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
