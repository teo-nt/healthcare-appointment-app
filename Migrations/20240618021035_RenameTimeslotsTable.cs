using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAppointmentApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameTimeslotsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AVAILABLE_TIMESLOTS");

            migrationBuilder.CreateTable(
                name: "TIMESLOTS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    START_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    END_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOCTOR_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMESLOTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TIMESLOTS_DOCTORS_DOCTOR_ID",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TIMESLOTS_DOCTOR_ID",
                table: "TIMESLOTS",
                column: "DOCTOR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TIMESLOTS");

            migrationBuilder.CreateTable(
                name: "AVAILABLE_TIMESLOTS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOCTOR_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    END_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    START_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AVAILABLE_TIMESLOTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AVAILABLE_TIMESLOTS_DOCTORS_DOCTOR_ID",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AVAILABLE_TIMESLOTS_DOCTOR_ID",
                table: "AVAILABLE_TIMESLOTS",
                column: "DOCTOR_ID");
        }
    }
}
