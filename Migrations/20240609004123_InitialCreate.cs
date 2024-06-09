using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAppointmentApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SPECIALITIES",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SPECIALITY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPECIALITIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ROLE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    USER_STATUS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DOCTORS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LASTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    STREET_NUMBER = table.Column<long>(type: "bigint", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    APPOINTMENT_DURATION = table.Column<long>(type: "bigint", nullable: false),
                    SPECIALITY_ID = table.Column<long>(type: "bigint", nullable: true),
                    USER_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCTORS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOCTORS_SPECIALITIES_SPECIALITY_ID",
                        column: x => x.SPECIALITY_ID,
                        principalTable: "SPECIALITIES",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DOCTORS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PATIENTS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LASTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AGE = table.Column<long>(type: "bigint", nullable: false),
                    GENDER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    MEDICAL_HISTORY = table.Column<string>(type: "nvarchar(max)", maxLength: -1, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    USER_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PATIENTS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AVAILABLE_TIMESLOTS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATE = table.Column<DateOnly>(type: "date", nullable: false),
                    START_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    END_TIME = table.Column<TimeOnly>(type: "time", nullable: false),
                    DOCTOR_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
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

            migrationBuilder.CreateTable(
                name: "APPOINTMENTS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATETIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PATIENT_ID = table.Column<long>(type: "bigint", nullable: true),
                    DOCTOR_ID = table.Column<long>(type: "bigint", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPOINTMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_DOCTORS_DOCTOR_ID",
                        column: x => x.DOCTOR_ID,
                        principalTable: "DOCTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APPOINTMENTS_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_DOCTOR_ID",
                table: "APPOINTMENTS",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APPOINTMENTS_PATIENT_ID",
                table: "APPOINTMENTS",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AVAILABLE_TIMESLOTS_DOCTOR_ID",
                table: "AVAILABLE_TIMESLOTS",
                column: "DOCTOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_D_LASTNAME",
                table: "DOCTORS",
                column: "LASTNAME");

            migrationBuilder.CreateIndex(
                name: "IX_DOCTORS_SPECIALITY_ID",
                table: "DOCTORS",
                column: "SPECIALITY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCTORS_USER_ID",
                table: "DOCTORS",
                column: "USER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_P_LASTNAME",
                table: "PATIENTS",
                column: "LASTNAME");

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_USER_ID",
                table: "PATIENTS",
                column: "USER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PHONE_NUMBER",
                table: "PATIENTS",
                column: "PHONE_NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_USERNAME",
                table: "USERS",
                column: "USERNAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPOINTMENTS");

            migrationBuilder.DropTable(
                name: "AVAILABLE_TIMESLOTS");

            migrationBuilder.DropTable(
                name: "PATIENTS");

            migrationBuilder.DropTable(
                name: "DOCTORS");

            migrationBuilder.DropTable(
                name: "SPECIALITIES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
