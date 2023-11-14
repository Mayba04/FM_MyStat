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
                values: new object[] { 1, "daad4c7c-1bc7-4faa-bbc8-ac3f5166054e" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb367071-08d3-4e39-8b14-4b513ef7e10d", null, "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdministratorId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentId", "SurName", "TeacherId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "daad4c7c-1bc7-4faa-bbc8-ac3f5166054e", 0, 1, "bcfd51b7-bdb1-4b22-8b10-fe194fef35a0", "AppUser", "admi@gmail.com", true, "John", "Connor", false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "bc83966c-042d-4301-932b-fca53d2f7497", null, "Johnovych", null, false, "admi@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "eb367071-08d3-4e39-8b14-4b513ef7e10d", "daad4c7c-1bc7-4faa-bbc8-ac3f5166054e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eb367071-08d3-4e39-8b14-4b513ef7e10d", "daad4c7c-1bc7-4faa-bbc8-ac3f5166054e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb367071-08d3-4e39-8b14-4b513ef7e10d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "daad4c7c-1bc7-4faa-bbc8-ac3f5166054e");

            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
