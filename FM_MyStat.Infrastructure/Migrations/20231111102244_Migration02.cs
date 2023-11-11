using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FM_MyStat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4fc73391-d6d3-43cd-9ed4-84e72a34ef3f", null, "Teacher", "TEACHER" },
                    { "9ac18314-0496-46af-929a-3ceddfc229b7", null, "Student", "STUDENT" },
                    { "f0dfcfa0-91b1-4ade-b5ed-8a4c81ef0b97", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2fd5c0b1-c3da-44d4-9b1b-cd349e55151c", 0, "d3c6b3ca-2024-4c11-b55d-e5f3a3a61288", "student@gmail.com", true, false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "1b5ef88f-7dbb-43b1-98de-7d0405976128", false, "student@gmail.com" },
                    { "413fd769-c662-4ad4-a011-a0e26174e60f", 0, "a97dcc5c-49c3-4e79-9745-e03c3607f0bf", "teacher@gmail.com", true, false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "3093ac9b-1f42-4b85-8d72-72e49ba99e9e", false, "teacher@gmail.com" },
                    { "ad77cdc9-f309-4d3d-95e1-a1f10345b2e9", 0, "1386ffea-bc04-4117-8071-27eeef119112", "admi@gmail.com", true, false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "6e9ab62a-3db1-48d8-a9b9-35c75418141e", false, "admi@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "FirstName", "LastName", "SurName" },
                values: new object[] { "ad77cdc9-f309-4d3d-95e1-a1f10345b2e9", "John", "Connor", "Johnovych" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9ac18314-0496-46af-929a-3ceddfc229b7", "2fd5c0b1-c3da-44d4-9b1b-cd349e55151c" },
                    { "4fc73391-d6d3-43cd-9ed4-84e72a34ef3f", "413fd769-c662-4ad4-a011-a0e26174e60f" },
                    { "f0dfcfa0-91b1-4ade-b5ed-8a4c81ef0b97", "ad77cdc9-f309-4d3d-95e1-a1f10345b2e9" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "FirstName", "LastName", "SurName" },
                values: new object[] { "413fd769-c662-4ad4-a011-a0e26174e60f", "John", "Doe", "Johnovych" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "TeacherId" },
                values: new object[] { 1, "PD116", "413fd769-c662-4ad4-a011-a0e26174e60f" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "GroupId", "LastName", "Rating", "SurName" },
                values: new object[] { "2fd5c0b1-c3da-44d4-9b1b-cd349e55151c", "John", 1, "Wick", 0, "Johnovych" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: "ad77cdc9-f309-4d3d-95e1-a1f10345b2e9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9ac18314-0496-46af-929a-3ceddfc229b7", "2fd5c0b1-c3da-44d4-9b1b-cd349e55151c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4fc73391-d6d3-43cd-9ed4-84e72a34ef3f", "413fd769-c662-4ad4-a011-a0e26174e60f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f0dfcfa0-91b1-4ade-b5ed-8a4c81ef0b97", "ad77cdc9-f309-4d3d-95e1-a1f10345b2e9" });

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "2fd5c0b1-c3da-44d4-9b1b-cd349e55151c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fc73391-d6d3-43cd-9ed4-84e72a34ef3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ac18314-0496-46af-929a-3ceddfc229b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0dfcfa0-91b1-4ade-b5ed-8a4c81ef0b97");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2fd5c0b1-c3da-44d4-9b1b-cd349e55151c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad77cdc9-f309-4d3d-95e1-a1f10345b2e9");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "413fd769-c662-4ad4-a011-a0e26174e60f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "413fd769-c662-4ad4-a011-a0e26174e60f");
        }
    }
}
