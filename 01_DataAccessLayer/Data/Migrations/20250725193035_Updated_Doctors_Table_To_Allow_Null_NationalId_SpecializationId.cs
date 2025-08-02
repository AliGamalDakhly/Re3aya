using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _01_DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Doctors_Table_To_Allow_Null_NationalId_SpecializationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f09236cd-a75a-4026-a05b-a70c40ef591b", "AQAAAAIAAYagAAAAEAMmBpA3XYAV1bCU4e+kRGpxvGOe1vFVYQ04GdQ+qdsPAqsZGGEJ6v8rJhFeJM7PeQ==", "a6fbb4e4-956f-4cd9-a92b-0b5138bf51fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06ccdf6e-f27f-4c1b-9dbd-32bcbacd4d2f", "AQAAAAIAAYagAAAAEMCE79TD0rI4t7f5GbQOkhEStM2Zl6TvAzyaWkcbo3bILYvm7KO0ailiXRfut+3TRw==", "fd5eadc7-b84b-4cfe-b057-7552826cf1a6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9b3ee0b-3204-47c0-9bc9-7a3c5c09d7a4", "AQAAAAIAAYagAAAAENFazy4Lkek/L5FUbmHktuq5GhpZRIb0hAkFu6QTmWWGY7xpZS9rDIBoemDW9OWRKQ==", "611c44f1-6a3d-46b2-9d2b-4964dbf268fd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "956139ad-3ffd-41e8-8102-93f4c3b87637", "AQAAAAIAAYagAAAAEA4C32bi7qEtGxWSMCHilpP21lLYJGid1cBrC0sFVxTLB1JLjDLtwAJwBASCf0oGBA==", "bb277107-d779-4da2-bfb9-7c1b6ff8a16e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06ec269e-fa30-47bd-bf0e-7322c541c3f1", "AQAAAAIAAYagAAAAEOPUTYBM7gWG1IfHLo+ib765KszXshspEQi3T5hh6DiKKy0w1/SaHDVPl2iQY94lZA==", "fe91ac9b-e442-4c2c-a342-e61831901b18" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20ce748f-99da-4bf8-b652-1a82ebe46443", "AQAAAAIAAYagAAAAEJohljpp/1tq41a2VWfbti5WXeBqHfLD203XIVekIC4maR73RbCXyOiDohdvUDoHIw==", "ad6d3318-3cb8-4388-963d-a43b4d45ab7d" });
        }
    }
}
