using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _01_DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Doctors_Table_To_Allow_Null_NationalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ddcc743-0809-4faf-936a-b410550f9b7f", "AQAAAAIAAYagAAAAEOvDkuggRiUd9Xdo0Duj/enwiAk5AhB8Ksz8rR1iOEWN2bCh8VfGgsmTvRzGEyoxnA==", "1745186c-0e59-4796-af84-a4162219cf06" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53905215-6116-4c99-9d4f-8ac6d5b60350", "AQAAAAIAAYagAAAAENtOLscE6QaTjI3Op7q4E4dnYxbkouIBJRNoR8zUbEfwohsOxm8ayhbMNet19EIf+A==", "4b2c44fb-7b6f-464a-bae6-811683b4491c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aea05986-3038-4000-aa2c-d5e18348ef44", "AQAAAAIAAYagAAAAEMJZFV6JYG/C5oEGx57mt6WC/pQz61MTAUvHE4YxAtJ6dUWezgG10QWrI6IOHVfXHg==", "351d124b-b500-467f-b221-0e46170a68c8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
