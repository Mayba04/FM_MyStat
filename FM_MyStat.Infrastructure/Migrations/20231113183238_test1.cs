using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FM_MyStat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "413fd769-c662-4ad4-a011-a0e26174e60f");

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
                keyValue: "413fd769-c662-4ad4-a011-a0e26174e60f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad77cdc9-f309-4d3d-95e1-a1f10345b2e9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a5754295-5841-4f8f-acaa-69fd6982f596", null, "Teacher", "TEACHER" },
                    { "a9004de0-bf1c-44ec-8037-ee093e1c1d93", null, "Administrator", "ADMINISTRATOR" },
                    { "c5ec05d4-bb62-40cc-a16f-beb3cb654b4b", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "56dd1af0-bb02-48f3-b3d6-e35c1a77370c", 0, "21151968-31ec-4ed5-b096-95b435e7da92", "teacher@gmail.com", true, false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "c9932f16-e283-478e-b1ea-0aeb02852252", false, "teacher@gmail.com" },
                    { "7ae40ab5-32f5-43bc-b2cf-b9b86e3cae6e", 0, "29d41443-2b36-4214-8f1f-8cd6f26b6500", "admi@gmail.com", true, false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "49e8f04e-2252-4b03-b3db-23e497cd8592", false, "admi@gmail.com" },
                    { "b4656dde-6245-4a41-a9d4-ffcabdd36bc8", 0, "c80d3900-73e7-463b-81d8-ef3d425d4b86", "student@gmail.com", true, false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "76b1b52f-9e37-4ed0-b258-d2c5634c8033", false, "student@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                column: "TeacherId",
                value: "56dd1af0-bb02-48f3-b3d6-e35c1a77370c");

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "FirstName", "LastName", "SurName" },
                values: new object[] { "7ae40ab5-32f5-43bc-b2cf-b9b86e3cae6e", "John", "Connor", "Johnovych" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a5754295-5841-4f8f-acaa-69fd6982f596", "56dd1af0-bb02-48f3-b3d6-e35c1a77370c" },
                    { "a9004de0-bf1c-44ec-8037-ee093e1c1d93", "7ae40ab5-32f5-43bc-b2cf-b9b86e3cae6e" },
                    { "c5ec05d4-bb62-40cc-a16f-beb3cb654b4b", "b4656dde-6245-4a41-a9d4-ffcabdd36bc8" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "GroupId", "LastName", "Rating", "SurName" },
                values: new object[] { "b4656dde-6245-4a41-a9d4-ffcabdd36bc8", "John", 1, "Wick", 0, "Johnovych" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "FirstName", "LastName", "SurName" },
                values: new object[] { "56dd1af0-bb02-48f3-b3d6-e35c1a77370c", "John", "Doe", "Johnovych" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: "7ae40ab5-32f5-43bc-b2cf-b9b86e3cae6e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a5754295-5841-4f8f-acaa-69fd6982f596", "56dd1af0-bb02-48f3-b3d6-e35c1a77370c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a9004de0-bf1c-44ec-8037-ee093e1c1d93", "7ae40ab5-32f5-43bc-b2cf-b9b86e3cae6e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c5ec05d4-bb62-40cc-a16f-beb3cb654b4b", "b4656dde-6245-4a41-a9d4-ffcabdd36bc8" });

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "b4656dde-6245-4a41-a9d4-ffcabdd36bc8");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "56dd1af0-bb02-48f3-b3d6-e35c1a77370c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5754295-5841-4f8f-acaa-69fd6982f596");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9004de0-bf1c-44ec-8037-ee093e1c1d93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5ec05d4-bb62-40cc-a16f-beb3cb654b4b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56dd1af0-bb02-48f3-b3d6-e35c1a77370c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ae40ab5-32f5-43bc-b2cf-b9b86e3cae6e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b4656dde-6245-4a41-a9d4-ffcabdd36bc8");

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

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                column: "TeacherId",
                value: "413fd769-c662-4ad4-a011-a0e26174e60f");

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
                table: "Students",
                columns: new[] { "Id", "FirstName", "GroupId", "LastName", "Rating", "SurName" },
                values: new object[] { "2fd5c0b1-c3da-44d4-9b1b-cd349e55151c", "John", 1, "Wick", 0, "Johnovych" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "FirstName", "LastName", "SurName" },
                values: new object[] { "413fd769-c662-4ad4-a011-a0e26174e60f", "John", "Doe", "Johnovych" });
        }
    }
}
