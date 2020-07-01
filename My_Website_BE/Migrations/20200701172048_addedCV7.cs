using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Website_BE.Migrations
{
    public partial class addedCV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationDocuments");

            migrationBuilder.DropTable(
                name: "ExperienceDocuments");

            migrationBuilder.DropTable(
                name: "LanguageDocuments");

            migrationBuilder.DropTable(
                name: "TrainingCourseDocuments");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true),
                    Description_EN = table.Column<string>(nullable: true),
                    Description_FR = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsDisplayed = table.Column<bool>(nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    ExperienceId = table.Column<int>(nullable: true),
                    EducationId = table.Column<int>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_TrainingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CourseId",
                table: "Documents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EducationId",
                table: "Documents",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ExperienceId",
                table: "Documents",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LanguageId",
                table: "Documents",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.CreateTable(
                name: "EducationDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationDocuments_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceId = table.Column<int>(type: "int", nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceDocuments_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageDocuments_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourseDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Description_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_FR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourseDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingCourseDocuments_TrainingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationDocuments_EducationId",
                table: "EducationDocuments",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceDocuments_ExperienceId",
                table: "ExperienceDocuments",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageDocuments_LanguageId",
                table: "LanguageDocuments",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseDocuments_CourseId",
                table: "TrainingCourseDocuments",
                column: "CourseId");
        }
    }
}
