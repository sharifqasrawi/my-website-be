using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Website_BE.Migrations
{
    public partial class addedCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emails = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    LinkedInUrl = table.Column<string>(nullable: true),
                    GitHubUrl = table.Column<string>(nullable: true),
                    FacebookUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CVFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_EN = table.Column<string>(nullable: true),
                    Title_FR = table.Column<string>(nullable: true),
                    Specialization_EN = table.Column<string>(nullable: true),
                    Specialization_FR = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    GraduateDate = table.Column<DateTime>(nullable: true),
                    YearsCount = table.Column<int>(nullable: false),
                    Mention_EN = table.Column<string>(nullable: true),
                    Mention_FR = table.Column<string>(nullable: true),
                    Establishment_EN = table.Column<string>(nullable: true),
                    Establishment_FR = table.Column<string>(nullable: true),
                    Country_EN = table.Column<string>(nullable: true),
                    Country_FR = table.Column<string>(nullable: true),
                    City_EN = table.Column<string>(nullable: true),
                    City_FR = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_EN = table.Column<string>(nullable: true),
                    Title_FR = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Country_EN = table.Column<string>(nullable: true),
                    Country_FR = table.Column<string>(nullable: true),
                    City_EN = table.Column<string>(nullable: true),
                    City_FR = table.Column<string>(nullable: true),
                    Responisbilites_EN = table.Column<string>(nullable: true),
                    Responisbilites_FR = table.Column<string>(nullable: true),
                    Accomplishments_EN = table.Column<string>(nullable: true),
                    Accomplishments_FR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true),
                    LevelRead = table.Column<int>(nullable: false),
                    LevelWrite = table.Column<int>(nullable: false),
                    LevelSpeak = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Title_EN = table.Column<string>(nullable: true),
                    Title_FR = table.Column<string>(nullable: true),
                    About_EN = table.Column<string>(nullable: true),
                    About_FR = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Establishment = table.Column<string>(nullable: true),
                    Country_EN = table.Column<string>(nullable: true),
                    Country_FR = table.Column<string>(nullable: true),
                    City_EN = table.Column<string>(nullable: true),
                    City_FR = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    CourseUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true),
                    Description_EN = table.Column<string>(nullable: true),
                    Description_FR = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    EducationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationDocuments_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true),
                    Description_EN = table.Column<string>(nullable: true),
                    Description_FR = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    ExperienceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceDocuments_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GetLanguageDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true),
                    Description_EN = table.Column<string>(nullable: true),
                    Description_FR = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetLanguageDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetLanguageDocuments_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_SkillCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "SkillCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourseDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_EN = table.Column<string>(nullable: true),
                    Name_FR = table.Column<string>(nullable: true),
                    Description_EN = table.Column<string>(nullable: true),
                    Description_FR = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourseDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingCourseDocuments_TrainingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_GetLanguageDocuments_LanguageId",
                table: "GetLanguageDocuments",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CategoryId",
                table: "Skills",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCourseDocuments_CourseId",
                table: "TrainingCourseDocuments",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropTable(
                name: "CVFiles");

            migrationBuilder.DropTable(
                name: "EducationDocuments");

            migrationBuilder.DropTable(
                name: "ExperienceDocuments");

            migrationBuilder.DropTable(
                name: "GetLanguageDocuments");

            migrationBuilder.DropTable(
                name: "PersonalInfo");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "TrainingCourseDocuments");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "SkillCategories");

            migrationBuilder.DropTable(
                name: "TrainingCourses");
        }
    }
}
