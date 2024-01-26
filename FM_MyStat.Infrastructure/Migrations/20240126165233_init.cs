using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    AdministratorId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimePublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    PathFile = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    HomeworkId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeworkId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
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
                    { "6409a216-d91f-40ba-8b60-908d294b3e85", null, "Student", "STUDENT" },
                    { "f0758146-0d4f-4911-9f66-ec93dd5326f2", null, "Administrator", "ADMINISTRATOR" },
                    { "fe01c14c-cdac-4de9-a290-7e49d3fe5d8f", null, "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdministratorId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentId", "SurName", "TeacherId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0bce2006-9a70-4967-b707-0d03e836b6da", 0, null, "aede0707-068d-4bc8-bffc-937e597338a6", "AppUser", "student3@email.com", true, "Pavlo", "Mayba", false, null, "STUDENT3@EMAIL.COM", "STUDENT3@EMAIL.COM", "AQAAAAIAAYagAAAAENRL/BhSCdjQ0LLk1+BePS7YTSh4NbRmdSgRxrNaOWzZjljL742HI1UVh+9Rp+BRdA==", "+xx(xxx)xxx-xx-xx", true, "b78dbad2-7b13-4cda-93bc-c8dc0b84296f", 4, "Ivanovich", null, false, "student3@email.com" },
                    { "0d44cee7-2a63-44be-b856-c22676e9ae44", 0, null, "906f0711-a61d-41ca-a49d-5e133ac3ed8f", "AppUser", "teacher@email.com", true, "John", "Connor", false, null, "TEACHER@EMAIL.COM", "TEACHER@EMAIL.COM", "AQAAAAIAAYagAAAAEA4Y+n2uyqtXsiSd/Ss9GjDzrJWykLw6rqPNmO0Dpt3lND/KuRNYly8JxPoypl8hpw==", "+xx(xxx)xxx-xx-xx", true, "3d49d342-b4ad-4859-9a68-d51bc9e265d1", null, "Johnovych", 1, false, "teacher@email.com" },
                    { "1e7a0c67-e816-44ee-ba7a-506a7c4d5f25", 0, 2, "45d2d4e8-58a1-446a-b948-789f90fa6d6f", "AppUser", "admin1@email.com", true, "John", "Connor", false, null, "ADMIN1@EMAIL.COM", "ADMIN1@EMAIL.COM", "AQAAAAIAAYagAAAAEN2iOlYYapVNgCqDr5DCCJ1/oqTZiwRKHbf0LRZnzHwWXsmIOq6IjCgNYUpDGiQpog==", "+xx(xxx)xxx-xx-xx", true, "dc7b660b-13af-4748-aeef-ee84857527de", null, "Johnovych", null, false, "admin1@email.com" },
                    { "395bb86a-a0f3-42df-a4d1-efc4c648a502", 0, null, "b58f0b27-8e09-41b4-814b-3bcdbe2b2910", "AppUser", "student@email.com", true, "John", "Connor", false, null, "STUDENT@EMAIL.COM", "STUDENT@EMAIL.COM", "AQAAAAIAAYagAAAAEH6EJT4gmXibiBL3AOsDNh6t5K4hzrbivfqToPGiP+87TeS0HCgz4/UougBGyZmklg==", "+xx(xxx)xxx-xx-xx", true, "907b61d4-cd38-482a-ac65-f893e945345c", 1, "Johnovych", null, false, "student@email.com" },
                    { "6a7897b7-2e41-4e78-9ca6-5cdf06d1f175", 0, null, "2c79f5d4-6a2a-4703-b840-a78c80dbe4ab", "AppUser", "student1@email.com", true, "Dima", "Shostak", false, null, "STUDENT1@EMAIL.COM", "STUDENT1@EMAIL.COM", "AQAAAAIAAYagAAAAEC+f45mGQ+m67fXopo8MbjfVjDyinHfMI5OK+ShG/FSfAX3Y++KE9P/BkT610wCerw==", "+xx(xxx)xxx-xx-xx", true, "0b8dc280-557b-45f4-a7f4-6bfacd9f2bd5", 2, "Oleksiyovich", null, false, "student1@email.com" },
                    { "7b9a41a7-2bdd-4412-b287-0c25c8c8a35d", 0, null, "39218991-6890-43bf-aedb-8a81aa3d5e6f", "AppUser", "teacher1@email.com", true, "Serhiy", "Stadnyk", false, null, "TEACHER1@EMAIL.COM", "TEACHER1@EMAIL.COM", "AQAAAAIAAYagAAAAEO1mwfbBy9PQttjs4WZXt3DwTuYXcWikMK1MH9Ai2U2Zsc8Wb7IxZ0qItWRn8YoQ5w==", "+xx(xxx)xxx-xx-xx", true, "cfa8f62d-131e-4c92-9218-7c1a8f7c4558", null, "Viacheslavovich", 2, false, "teacher1@email.com" },
                    { "a71366a7-5ddd-4e5e-858d-0ac4bc82014d", 0, 1, "0bf7887b-9f60-4636-9a6e-3e1127529ccb", "AppUser", "admin@email.com", true, "John", "Connor", false, null, "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEAtjkd31OASuEbhCb4nEvjs7ocxih7/S9ZWhuKfge/TtGm8N1jitfKsU3iy7UvaqRg==", "+xx(xxx)xxx-xx-xx", true, "e45495a9-8d66-4e1b-8629-264c80bb9f48", null, "Johnovych", null, false, "admin@email.com" },
                    { "cc1d9934-720d-4315-a4af-180ef4f14f46", 0, null, "9678a274-a80e-4974-a84f-278ce8f3dcb3", "AppUser", "student2@email.com", true, "Yurii", "Bortnik", false, null, "STUDENT2@EMAIL.COM", "STUDENT2@EMAIL.COM", "AQAAAAIAAYagAAAAEJMv5zHVbSmhSBhU2zEndSZFFO67XkwXxbQREMSEvNlIZ0AivdnLTRUXFGZLbUqjnA==", "+xx(xxx)xxx-xx-xx", true, "1a74520a-6eeb-4d26-9469-e21a770506e0", 3, "Andriyovich", null, false, "student2@email.com" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Description", "Time", "TimePublication", "Title" },
                values: new object[,]
                {
                    { 1, "Опис до новини 1", new DateTime(2024, 1, 27, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3146), new DateTime(2024, 1, 25, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3150), "Новина 1" },
                    { 2, "Опис до новини 2", new DateTime(2024, 1, 28, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3172), new DateTime(2024, 1, 25, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3174), "Новина 2" },
                    { 3, "Опис до новини 3", new DateTime(2024, 1, 29, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3209), new DateTime(2024, 1, 25, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3211), "Новина 3" },
                    { 4, "Опис до новини 4", new DateTime(2024, 1, 30, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3225), new DateTime(2024, 1, 25, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3226), "Новина 4" },
                    { 5, "Опис до новини 5", new DateTime(2024, 1, 31, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3239), new DateTime(2024, 1, 25, 16, 52, 33, 376, DateTimeKind.Utc).AddTicks(3240), "Новина 5" }
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
                    { 1, "a71366a7-5ddd-4e5e-858d-0ac4bc82014d" },
                    { 2, "1e7a0c67-e816-44ee-ba7a-506a7c4d5f25" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6409a216-d91f-40ba-8b60-908d294b3e85", "0bce2006-9a70-4967-b707-0d03e836b6da" },
                    { "fe01c14c-cdac-4de9-a290-7e49d3fe5d8f", "0d44cee7-2a63-44be-b856-c22676e9ae44" },
                    { "f0758146-0d4f-4911-9f66-ec93dd5326f2", "1e7a0c67-e816-44ee-ba7a-506a7c4d5f25" },
                    { "6409a216-d91f-40ba-8b60-908d294b3e85", "395bb86a-a0f3-42df-a4d1-efc4c648a502" },
                    { "6409a216-d91f-40ba-8b60-908d294b3e85", "6a7897b7-2e41-4e78-9ca6-5cdf06d1f175" },
                    { "fe01c14c-cdac-4de9-a290-7e49d3fe5d8f", "7b9a41a7-2bdd-4412-b287-0c25c8c8a35d" },
                    { "f0758146-0d4f-4911-9f66-ec93dd5326f2", "a71366a7-5ddd-4e5e-858d-0ac4bc82014d" },
                    { "6409a216-d91f-40ba-8b60-908d294b3e85", "cc1d9934-720d-4315-a4af-180ef4f14f46" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "AppUserId" },
                values: new object[,]
                {
                    { 1, "0d44cee7-2a63-44be-b856-c22676e9ae44" },
                    { 2, "7b9a41a7-2bdd-4412-b287-0c25c8c8a35d" }
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
                values: new object[] { 1, "First steps into c# today", new DateTime(2024, 1, 26, 19, 52, 33, 376, DateTimeKind.Utc).AddTicks(2766), 1, null, "C# beginning", new DateTime(2024, 1, 26, 17, 52, 33, 376, DateTimeKind.Utc).AddTicks(2755), 1, 1 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AppUserId", "GroupId", "Rating" },
                values: new object[,]
                {
                    { 1, "395bb86a-a0f3-42df-a4d1-efc4c648a502", 1, 0 },
                    { 2, "6a7897b7-2e41-4e78-9ca6-5cdf06d1f175", 1, 0 },
                    { 3, "cc1d9934-720d-4315-a4af-180ef4f14f46", 2, 0 },
                    { 4, "0bce2006-9a70-4967-b707-0d03e836b6da", 2, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_AppUserId",
                table: "Administrators",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                unique: true,
                filter: "[HomeworkId] IS NOT NULL");

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
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teachers",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

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
