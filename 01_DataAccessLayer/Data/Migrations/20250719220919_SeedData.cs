using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _01_DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "9491f2ae-fcdb-4057-9fd5-d7820557dc2c", new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Utc), new DateOnly(1990, 5, 15), "ahmed@example.com", false, "Ahmed Khaled", 0, false, null, "AHMED@EXAMPLE.COM", "AHMED@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPhLhCswJK7dNZTZhbU5ekiJxw3rwjxjA9ps28oVB63K0zrPTNk5RB1dUJbDLRFyDA==", "01234567890", false, "abdf8d64-c10d-444c-af3c-b7c9553cb760", false, "ahmed@example.com" },
                    { "2", 0, "111d28f1-522d-4a1d-bea6-a92c6344c1f6", new DateTime(2025, 7, 1, 11, 0, 0, 0, DateTimeKind.Utc), new DateOnly(1985, 9, 22), "sara.ali@doc.com", false, "Dr. Sara Ali", 1, false, null, "SARA.ALI@DOC.COM", "SARA.ALI@DOC.COM", "AQAAAAIAAYagAAAAENb65JF25Bnfd6Sgzyb09gi2W/YxOOMXV9QylM05UyUhlNqTCsg66IR/yxL1ZC21Sg==", "01987654321", false, "53ce9db4-2c11-46c3-8fd3-13fdd1ed1f6c", false, "sara.ali@doc.com" },
                    { "3", 0, "a6fc7c9d-1659-4d4e-bce4-48df30aa10ad", new DateTime(2025, 7, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateOnly(1980, 3, 10), "omar@admin.com", false, "Admin Omar", 0, false, null, "OMAR@ADMIN.COM", "OMAR@ADMIN.COM", "AQAAAAIAAYagAAAAEB64NijPIi36ipAqhHDhMkBrW/gNah/pamxvSaY3LGScjlh9ts4j/utjr1AgxzsAnw==", "01102682493", false, "c8b53f4f-a382-4a5b-9e00-e753ce9a5556", false, "omar@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "Governments",
                columns: new[] { "GovernmentId", "Name" },
                values: new object[,]
                {
                    { 1, "Cairo" },
                    { 2, "Alexandria" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "CreatedAt", "Status", "TransactionId" },
                values: new object[] { 1, 500.0, new DateTime(2025, 7, 19, 11, 0, 0, 0, DateTimeKind.Utc), 1, 123456 });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "SpecializationId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Heart care", "Cardiology" },
                    { 2, "Child care", "Pediatrics" }
                });

            migrationBuilder.InsertData(
                table: "SystemInfos",
                columns: new[] { "SystemInfoId", "Balance", "Email", "Name", "PhoneNumber" },
                values: new object[] { 1, 10000.0, "support@rea3ya.com", "Rea3ya", "+20212345678" });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "TimeSlotId", "DayOfWeek", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 7, 21, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 7, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 3, new DateTime(2025, 7, 22, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 7, 22, 14, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "AppUserId", "SystemId", "SystemInfoId" },
                values: new object[] { 1, "3", 1, null });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "GovernmentId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Cairo" },
                    { 2, 2, "Alexandria" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "AboutMe", "AppUserId", "Balance", "ExpYears", "Fees", "NationalId", "RatingValue", "Service", "SpecializationId", "Status" },
                values: new object[] { 1, "Experienced cardiologist with 10 years in heart care.", "2", 1500.0, 10, 500.0, "12345678901234", 4.8f, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "AppUserId" },
                values: new object[] { 1, "1" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "CityId", "DetailedAddress", "DoctorId", "Location" },
                values: new object[] { 1, 1, "5th Settlement", 1, "Cairo" });

            migrationBuilder.InsertData(
                table: "DoctorTimeSlots",
                columns: new[] { "DoctorTimeSlotId", "DoctorId", "IsAvailable", "TimeSlotId" },
                values: new object[,]
                {
                    { 1, 1, true, 1 },
                    { 2, 1, true, 2 }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "DoctorId", "DocumentType", "FilePath", "IsVerified", "UploadedAt" },
                values: new object[,]
                {
                    { 1, 1, 2, "cert_sara.pdf", true, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, 4, "id_sara.pdf", true, new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingId", "Comment", "DoctorId", "PatientId", "RatingValue" },
                values: new object[] { 1, "Great service!", 1, 1, 4.8f });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CreatedAt", "DoctorTimeSlotId", "Notes", "PatientId", "PaymentId", "Status" },
                values: new object[] { 1, new DateTime(2025, 7, 19, 11, 0, 0, 0, DateTimeKind.Utc), 1, "Initial visit", 1, 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DoctorTimeSlots",
                keyColumn: "DoctorTimeSlotId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "RatingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SystemInfos",
                keyColumn: "SystemInfoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorTimeSlots",
                keyColumn: "DoctorTimeSlotId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Governments",
                keyColumn: "GovernmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeSlots",
                keyColumn: "TimeSlotId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Governments",
                keyColumn: "GovernmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeSlots",
                keyColumn: "TimeSlotId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 1);
        }
    }
}
