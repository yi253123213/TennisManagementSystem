using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisFinalGrp339.Data.Migrations
{
    /// <inheritdoc />
    public partial class Enroll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Enrollment",
                newName: "EnrolledOn");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schedule",
                type: "nchar(200)",
                fixedLength: true,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(200)",
                oldFixedLength: true,
                oldMaxLength: 200);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnrolledOn",
                table: "Enrollment",
                newName: "EnrollmentDate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schedule",
                type: "nchar(200)",
                fixedLength: true,
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nchar(200)",
                oldFixedLength: true,
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
