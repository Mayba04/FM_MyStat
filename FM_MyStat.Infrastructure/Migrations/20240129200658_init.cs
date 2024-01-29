using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FM_MyStat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    SurName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    StudentId = table.Column<int>(type: "integer", nullable: true),
                    TeacherId = table.Column<int>(type: "integer", nullable: true),
                    AdministratorId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TimePublication = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeachersSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachersSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersSubjects_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    PathFile = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homeworks_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserId = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    HomeworkId = table.Column<int>(type: "integer", nullable: true),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworksDone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HomeworkId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    Mark = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworksDone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworksDone_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworksDone_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mark = table.Column<int>(type: "integer", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonMarks_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fcdfeeb-dc34-415d-8246-0f9d890da3e3", null, "Administrator", "ADMINISTRATOR" },
                    { "36743540-e55f-417e-b3ee-f089a6380188", null, "Student", "STUDENT" },
                    { "a034bece-26a7-44a3-8124-60e75ba192bc", null, "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdministratorId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentId", "SurName", "TeacherId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "05e414a5-3d8f-4355-a754-f0ac9d4bb7c9", 0, 1, "f81c73f5-e6ed-43b1-8cb9-cc9a80e23d03", "AppUser", "admin@email.com", true, "John", "Connor", false, null, "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAELl27w3F/A6iKDAK+a4AiuTgNCMZ+M1OqZUlAPSZZ8NDe0Jme5UV3s/vD3W+/i9mFg==", "+xx(xxx)xxx-xx-xx", true, "c2e7cd5d-1ec7-4ec1-ba91-c5e64e92b24a", null, "Johnovych", null, false, "admin@email.com" },
                    { "11fa4a25-941a-4314-9c46-3ae02258ff30", 0, null, "ffaf1b41-fea3-4652-9ad5-e6df3924eac0", "AppUser", "student2@email.com", true, "Yurii", "Bortnik", false, null, "STUDENT2@EMAIL.COM", "STUDENT2@EMAIL.COM", "AQAAAAIAAYagAAAAEJB18rmPiUGsmNoskLGeURQeaGbR3q3FHdPlnkQsQtPggpt40Ny+7VsF+iZ9Vz+X8A==", "+xx(xxx)xxx-xx-xx", true, "c48980e3-e4e2-4e6a-bcd9-6895a88e6977", 3, "Andriyovich", null, false, "student2@email.com" },
                    { "31f2c536-54f9-40b8-92b7-beae31f4fa58", 0, null, "65af5198-8927-4007-adcd-aed7466eaa95", "AppUser", "teacher1@email.com", true, "Serhiy", "Stadnyk", false, null, "TEACHER1@EMAIL.COM", "TEACHER1@EMAIL.COM", "AQAAAAIAAYagAAAAEKOLoE5exh3KrxGkByFq2JOTV7Q/HkX9ouLpAyNMfksAnsSq03g27L6+BtXCz5TNUg==", "+xx(xxx)xxx-xx-xx", true, "5c8d38c3-644e-44f1-9e79-fb52008bbb8a", null, "Viacheslavovich", 2, false, "teacher1@email.com" },
                    { "5a284c53-4767-4c01-9f03-2e946cc6bccc", 0, null, "4e026511-fb06-4795-b83b-e71eeb3c410b", "AppUser", "student1@email.com", true, "Dima", "Shostak", false, null, "STUDENT1@EMAIL.COM", "STUDENT1@EMAIL.COM", "AQAAAAIAAYagAAAAEKkZjq4pEK4IQOZnSVZT2A+LC453bAr4E4MRLMEQ/GI3eJLHJZ/IkDE6kyxL9eYPkQ==", "+xx(xxx)xxx-xx-xx", true, "20e7ff2e-56da-4ea9-b23c-2c6a86807529", 2, "Oleksiyovich", null, false, "student1@email.com" },
                    { "5a37ab74-ff46-419a-b82a-c9149d3df041", 0, null, "9d5cf4bd-cec2-4393-81c2-e513b862a568", "AppUser", "student3@email.com", true, "Pavlo", "Mayba", false, null, "STUDENT3@EMAIL.COM", "STUDENT3@EMAIL.COM", "AQAAAAIAAYagAAAAEKehBeXDN2IcBXinazQVBmkvFQ8tbS18/qlCmciERdZXtJfkJT1uslMBX4oEmGir7w==", "+xx(xxx)xxx-xx-xx", true, "c6297e8f-e124-438d-9cb8-4304cf32de92", 4, "Ivanovich", null, false, "student3@email.com" },
                    { "69405185-98c6-4270-ab70-727f5bed536a", 0, null, "1957d1e6-0657-4d04-96c3-af6f03292092", "AppUser", "teacher@email.com", true, "John", "Connor", false, null, "TEACHER@EMAIL.COM", "TEACHER@EMAIL.COM", "AQAAAAIAAYagAAAAEK4hfw7UIxvxLf5XUlK0xg7tvxbK05HPW8H34mCwhSi/LjJYGByfO4qMsdBVOk5XbQ==", "+xx(xxx)xxx-xx-xx", true, "c7449e17-d6fb-460e-81dc-9390b64d121e", null, "Johnovych", 1, false, "teacher@email.com" },
                    { "69aa7955-c335-4fb3-929b-5852de237864", 0, 2, "885fc30d-5064-4eac-8ea6-84db7ce8cf7d", "AppUser", "admin1@email.com", true, "John", "Connor", false, null, "ADMIN1@EMAIL.COM", "ADMIN1@EMAIL.COM", "AQAAAAIAAYagAAAAEBjdql0WUzh4MR9zOr7ACne9G66huVDbzrfkPUBfUC36KDkfvEkGZKWh0iBErlIJdg==", "+xx(xxx)xxx-xx-xx", true, "a67236fc-87a3-42de-a441-275012f9cec9", null, "Johnovych", null, false, "admin1@email.com" },
                    { "cb2316a6-7ca4-4dc1-aa75-d6e826b04aa0", 0, null, "8fe93dc6-a2bf-4efa-8ac3-2c7ac79663f0", "AppUser", "student@email.com", true, "John", "Connor", false, null, "STUDENT@EMAIL.COM", "STUDENT@EMAIL.COM", "AQAAAAIAAYagAAAAEFlC04SA88jjUSjUBMGRf+BFHizQjmV62hz+Asf1AhTiXPdcpdc5S7m6vFC35DzuQg==", "+xx(xxx)xxx-xx-xx", true, "8a0f647c-ea29-4e6f-8c4e-6561ae37dccc", 1, "Johnovych", null, false, "student@email.com" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Description", "Time", "TimePublication", "Title" },
                values: new object[,]
                {
                    { 1, "Опис до новини 1", new DateTime(2024, 1, 30, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9837), new DateTime(2024, 1, 28, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9839), "Новина 1" },
                    { 2, "Опис до новини 2", new DateTime(2024, 1, 31, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9856), new DateTime(2024, 1, 28, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9857), "Новина 2" },
                    { 3, "Опис до новини 3", new DateTime(2024, 2, 1, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9900), new DateTime(2024, 1, 28, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9901), "Новина 3" },
                    { 4, "Опис до новини 4", new DateTime(2024, 2, 2, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9913), new DateTime(2024, 1, 28, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9914), "Новина 4" },
                    { 5, "Опис до новини 5", new DateTime(2024, 2, 3, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9924), new DateTime(2024, 1, 28, 20, 6, 58, 67, DateTimeKind.Utc).AddTicks(9924), "Новина 5" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "C#" });

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "AppUserId" },
                values: new object[,]
                {
                    { 1, "05e414a5-3d8f-4355-a754-f0ac9d4bb7c9" },
                    { 2, "69aa7955-c335-4fb3-929b-5852de237864" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0fcdfeeb-dc34-415d-8246-0f9d890da3e3", "05e414a5-3d8f-4355-a754-f0ac9d4bb7c9" },
                    { "36743540-e55f-417e-b3ee-f089a6380188", "11fa4a25-941a-4314-9c46-3ae02258ff30" },
                    { "a034bece-26a7-44a3-8124-60e75ba192bc", "31f2c536-54f9-40b8-92b7-beae31f4fa58" },
                    { "36743540-e55f-417e-b3ee-f089a6380188", "5a284c53-4767-4c01-9f03-2e946cc6bccc" },
                    { "36743540-e55f-417e-b3ee-f089a6380188", "5a37ab74-ff46-419a-b82a-c9149d3df041" },
                    { "a034bece-26a7-44a3-8124-60e75ba192bc", "69405185-98c6-4270-ab70-727f5bed536a" },
                    { "0fcdfeeb-dc34-415d-8246-0f9d890da3e3", "69aa7955-c335-4fb3-929b-5852de237864" },
                    { "36743540-e55f-417e-b3ee-f089a6380188", "cb2316a6-7ca4-4dc1-aa75-d6e826b04aa0" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "AppUserId" },
                values: new object[,]
                {
                    { 1, "69405185-98c6-4270-ab70-727f5bed536a" },
                    { 2, "31f2c536-54f9-40b8-92b7-beae31f4fa58" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "TeacherId" },
                values: new object[,]
                {
                    { 1, "suicide terrorists", 1 },
                    { 2, "Bad Boys", 2 }
                });

            migrationBuilder.InsertData(
                table: "TeachersSubjects",
                columns: new[] { "Id", "SubjectId", "TeacherId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Description", "End", "GroupId", "HomeworkId", "Name", "Start", "SubjectId", "TeacherId" },
                values: new object[] { 1, "First steps into c# today", new DateTime(2024, 1, 29, 23, 6, 58, 67, DateTimeKind.Utc).AddTicks(9310), 1, null, "C# beginning", new DateTime(2024, 1, 29, 21, 6, 58, 67, DateTimeKind.Utc).AddTicks(9211), 1, 1 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AppUserId", "GroupId", "Rating" },
                values: new object[,]
                {
                    { 1, "cb2316a6-7ca4-4dc1-aa75-d6e826b04aa0", 1, 0 },
                    { 2, "5a284c53-4767-4c01-9f03-2e946cc6bccc", 1, 0 },
                    { 3, "11fa4a25-941a-4314-9c46-3ae02258ff30", 2, 0 },
                    { 4, "5a37ab74-ff46-419a-b82a-c9149d3df041", 2, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_AppUserId",
                table: "Administrators",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_GroupId",
                table: "Homeworks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_TeacherId",
                table: "Homeworks",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworksDone_HomeworkId",
                table: "HomeworksDone",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworksDone_StudentId",
                table: "HomeworksDone",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonMarks_LessonId",
                table: "LessonMarks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonMarks_StudentId",
                table: "LessonMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GroupId",
                table: "Lessons",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_HomeworkId",
                table: "Lessons",
                column: "HomeworkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TeacherId",
                table: "Lessons",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AppUserId",
                table: "Students",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teachers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeachersSubjects_SubjectId",
                table: "TeachersSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersSubjects_TeacherId",
                table: "TeachersSubjects",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HomeworksDone");

            migrationBuilder.DropTable(
                name: "LessonMarks");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "TeachersSubjects");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
