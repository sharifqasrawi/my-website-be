using Microsoft.EntityFrameworkCore.Migrations;

namespace My_Website_BE.Migrations
{
    public partial class addedCV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDocuments_Educations_EducationId",
                table: "EducationDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceDocuments_Experiences_ExperienceId",
                table: "ExperienceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageDocuments_Languages_LanguageId",
                table: "LanguageDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingCourseDocuments_TrainingCourses_CourseId",
                table: "TrainingCourseDocuments");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TrainingCourseDocuments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "LanguageDocuments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentlyWorking",
                table: "Experiences",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExperienceId",
                table: "ExperienceDocuments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "EducationDocuments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDocuments_Educations_EducationId",
                table: "EducationDocuments",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceDocuments_Experiences_ExperienceId",
                table: "ExperienceDocuments",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageDocuments_Languages_LanguageId",
                table: "LanguageDocuments",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingCourseDocuments_TrainingCourses_CourseId",
                table: "TrainingCourseDocuments",
                column: "CourseId",
                principalTable: "TrainingCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDocuments_Educations_EducationId",
                table: "EducationDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceDocuments_Experiences_ExperienceId",
                table: "ExperienceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageDocuments_Languages_LanguageId",
                table: "LanguageDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingCourseDocuments_TrainingCourses_CourseId",
                table: "TrainingCourseDocuments");

            migrationBuilder.DropColumn(
                name: "IsCurrentlyWorking",
                table: "Experiences");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TrainingCourseDocuments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "LanguageDocuments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ExperienceId",
                table: "ExperienceDocuments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "EducationDocuments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDocuments_Educations_EducationId",
                table: "EducationDocuments",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceDocuments_Experiences_ExperienceId",
                table: "ExperienceDocuments",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageDocuments_Languages_LanguageId",
                table: "LanguageDocuments",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingCourseDocuments_TrainingCourses_CourseId",
                table: "TrainingCourseDocuments",
                column: "CourseId",
                principalTable: "TrainingCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
