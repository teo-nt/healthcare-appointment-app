using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAppointmentApp.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintsPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_PHONE_NUMBER",
                table: "PATIENTS",
                newName: "UQ_P_PHONE_NUMBER");

            migrationBuilder.CreateIndex(
                name: "UQ_D_PHONE_NUMBER",
                table: "DOCTORS",
                column: "PHONE_NUMBER",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_D_PHONE_NUMBER",
                table: "DOCTORS");

            migrationBuilder.RenameIndex(
                name: "UQ_P_PHONE_NUMBER",
                table: "PATIENTS",
                newName: "UQ_PHONE_NUMBER");
        }
    }
}
