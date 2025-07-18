using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _01_DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Moved_IsAvailable_From_TimeSlot_To_DoctorTimeSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "TimeSlots");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "DoctorTimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "DoctorTimeSlots");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "TimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
