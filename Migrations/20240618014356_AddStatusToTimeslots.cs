using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAppointmentApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToTimeslots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STATUS",
                table: "AVAILABLE_TIMESLOTS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STATUS",
                table: "AVAILABLE_TIMESLOTS");
        }
    }
}
