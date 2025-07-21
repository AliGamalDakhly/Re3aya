using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _01_DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeadData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "AppUserId", "SystemId", "SystemInfoId" },
                values: new object[] { 1, "3", 1, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5", 0, "07c501c3-f37a-4bb4-adf8-9b6cbdff9e4f", new DateTime(2025, 7, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateOnly(1980, 3, 10), "omar@admin.com", false, "Admin Omar", 0, false, null, "OMAR@ADMIN.COM", "OMAR@ADMIN.COM", "AQAAAAIAAYagAAAAEDJFGyozDdHxCTAX4dwBmRtqLVydroCJ/ZNXGcdKZmDmQNCqnlebtSr+aOrj9NlUbw==", "01102682493", false, "e9f91751-9e80-4177-8dd1-c5542ba4a6ea", false, "omar@admin.com" },
                    { "6", 0, "a28a1f6a-da01-425d-a696-b4e0ce74902c", new DateTime(2025, 7, 1, 11, 0, 0, 0, DateTimeKind.Utc), new DateOnly(1985, 9, 22), "sara.ali@doc.com", false, "Dr. Sara Ali", 1, false, null, "SARA.ALI@DOC.COM", "SARA.ALI@DOC.COM", "AQAAAAIAAYagAAAAEGbenWRv90bduGW7Jge8KwAeaLX+lK72m3s4iaBPwE0mR7KUu1WSwLvGzcYT3JVvoQ==", "01987654321", false, "e8eaf264-b4f3-4db3-b8e3-9bfd0d0b3105", false, "sara.ali@doc.com" },
                    { "7", 0, "ec4fdfdf-67e4-4b38-b2d8-8b21fe1f2b4b", new DateTime(2025, 7, 1, 10, 0, 0, 0, DateTimeKind.Utc), new DateOnly(1990, 5, 15), "ahmed@example.com", false, "Ahmed Khaled", 0, false, null, "AHMED@EXAMPLE.COM", "AHMED@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMZc/NMZ2hYNpT5thIRLa9W6LqgPYlp/gkYdRJil8cD1q+D2bbHKzF3rH2YwwYWc1w==", "01234567890", false, "7aad23ff-a8d5-492d-838c-69f00cc820ed", false, "ahmed@example.com" }
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
                table: "Patients",
                columns: new[] { "PatientId", "AppUserId" },
                values: new object[] { 1, "1" });

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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7");

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
                table: "Specializations",
                keyColumn: "SpecializationId",
                keyValue: 1);
        }
    }
}
