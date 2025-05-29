using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCodeCodeFirstLib.Migrations
{
    /// <inheritdoc />
    public partial class student_course_tbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_enrollments_tbl_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tbl_courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_enrollments_tbl_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tbl_students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_enrollments_CourseId",
                table: "tbl_enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_enrollments_StudentId",
                table: "tbl_enrollments",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_enrollments");

            migrationBuilder.DropTable(
                name: "tbl_courses");

            migrationBuilder.DropTable(
                name: "tbl_students");
        }
    }
}
