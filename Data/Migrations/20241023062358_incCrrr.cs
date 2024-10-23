using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisFinalGrp339.Data.Migrations
{
    /// <inheritdoc />
    public partial class incCrrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.CoachId);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nchar(400)", fixedLength: true, maxLength: 400, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Schedule");
        }
    }
}
