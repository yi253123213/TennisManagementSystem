using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbFirstEF.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachIdToSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
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
                    table.PrimaryKey("PK_Coaches", x => x.CoachId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
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
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "CoachId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CoachId",
                table: "Schedules",
                column: "CoachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Coaches");
        }
    }
}
