using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisFinalGrp339.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Member_MemberId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Schedule_ScheduleId",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Enrollment");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Enrollment",
                newName: "EnrolledOn");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_ScheduleId",
                table: "Enrollment",
                newName: "IX_Enrollment_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_MemberId",
                table: "Enrollment",
                newName: "IX_Enrollment_MemberId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CoachId",
                table: "AspNetUsers",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MemberId",
                table: "AspNetUsers",
                column: "MemberId",
                unique: true,
                filter: "[MemberId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MemberId1",
                table: "AspNetUsers",
                column: "MemberId1",
                unique: true,
                filter: "[MemberId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Coach_CoachId",
                table: "AspNetUsers",
                column: "CoachId",
                principalTable: "Coach",
                principalColumn: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Member_MemberId",
                table: "AspNetUsers",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Member_MemberId1",
                table: "AspNetUsers",
                column: "MemberId1",
                principalTable: "Member",
                principalColumn: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Member_MemberId",
                table: "Enrollment",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Schedule_ScheduleId",
                table: "Enrollment",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Coach_CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Member_MemberId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Member_MemberId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Member_MemberId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Schedule_ScheduleId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MemberId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MemberId1",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollment",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Enrollment",
                newName: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "EnrolledOn",
                table: "Enrollments",
                newName: "EnrollmentDate");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_ScheduleId",
                table: "Enrollments",
                newName: "IX_Enrollments_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollment_MemberId",
                table: "Enrollments",
                newName: "IX_Enrollments_MemberId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Schedule",
                type: "nchar(200)",
                fixedLength: true,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(200)",
                oldFixedLength: true,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Member_MemberId",
                table: "Enrollments",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Schedule_ScheduleId",
                table: "Enrollments",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
