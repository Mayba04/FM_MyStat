using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM_MyStat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { 1, "c955347f-562e-4c6c-86f0-34d245998181" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "847b5309-a048-41e6-9e40-910b82f73231", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdministratorId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentId", "SurName", "TeacherId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c955347f-562e-4c6c-86f0-34d245998181", 0, 1, "f4a169b7-4b63-4da9-aacb-4d69ed6dd803", "AppUser", "admin@example.com", true, "John", "Connor", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKFZdHuLenxWeyE5yIG7wN3YjFPZxaXGC+GONydoi3rL1NWG30zw30W2+My7ZErvzg==", "+xx(xxx)xxx-xx-xx", true, "ccc311e1-7312-48e4-9029-5f66d6588967", null, "Johnovych", null, false, "admin@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "847b5309-a048-41e6-9e40-910b82f73231", "c955347f-562e-4c6c-86f0-34d245998181" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "847b5309-a048-41e6-9e40-910b82f73231", "c955347f-562e-4c6c-86f0-34d245998181" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "847b5309-a048-41e6-9e40-910b82f73231");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c955347f-562e-4c6c-86f0-34d245998181");

            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
