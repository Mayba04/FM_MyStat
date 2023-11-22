using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = FM_MyStat.Core.Entities.Group;

namespace FM_MyStat.Infrastructure.Initializers
{
    public static class DBInitializer
    {
        public static void SeedAdministrator(this ModelBuilder modelBuilder)
        {
            var adminUserId = Guid.NewGuid().ToString();
            var adminRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });

            var passwordHasher = new PasswordHasher<AppUser>();

            var adminUser = new AppUser
            {
                Id = adminUserId,
                FirstName = "John",
                LastName = "Connor",
                SurName = "Johnovych",
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                AdministratorId = 1,
            };

            var hashedPassword = passwordHasher.HashPassword(adminUser, "Qwerty-1");

            adminUser.PasswordHash = hashedPassword;

            modelBuilder.Entity<AppUser>().HasData(adminUser);

            modelBuilder.Entity<Administrator>().HasData(new Administrator
            {
                Id=1,
                AppUserId = adminUserId
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }

        

        public static void SeedTeacher(this ModelBuilder modelBuilder)
        {
            var teacherUserId = Guid.NewGuid().ToString();
            var teacherRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = teacherRoleId,
                Name = "Teacher",
                NormalizedName = "TEACHER"
            });

            var passwordHasher = new PasswordHasher<AppUser>();

            var adminUser = new AppUser
            {
                Id = teacherUserId,
                FirstName = "John",
                LastName = "Connor",
                SurName = "Johnovych",
                UserName = "teacher@email.com",
                NormalizedUserName = "TEACHER@EMAIL.COM",
                Email = "teacher@email.com",
                NormalizedEmail = "TEACHER@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                TeacherId = 1,
            };

            var hashedPassword = passwordHasher.HashPassword(adminUser, "Qwerty-1");

            adminUser.PasswordHash = hashedPassword;

            modelBuilder.Entity<AppUser>().HasData(adminUser);

            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = 1,
                AppUserId = teacherUserId
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = teacherRoleId,
                UserId = teacherUserId
            });
        }
        public static void SeedLesson(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>().HasData(new Lesson
            {
                Id = 1,
                Name = "C# beginning",
                Description = "First steps into c# today",
                Start = DateTime.UtcNow.AddHours(1),
                End = DateTime.UtcNow.AddHours(3),
                TeacherId = 1,
                GroupId = 1,
                SubjectId = 1
            });
        }
        public static void SeedSubject(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasData(new Subject
            {
                Id = 1,
                Name = "C#"
            });
        }

        public static void SeedStudent(this ModelBuilder modelBuilder)
        {
            var studentUserId = Guid.NewGuid().ToString();
            var studentRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = studentRoleId,
                Name = "Student",
                NormalizedName = "STUDENT"
            });

            var passwordHasher = new PasswordHasher<AppUser>();

            var adminUser = new AppUser
            {
                Id = studentUserId,
                FirstName = "John",
                LastName = "Connor",
                SurName = "Johnovych",
                UserName = "student@email.com",
                NormalizedUserName = "STUDENT@EMAIL.COM",
                Email = "student@email.com",
                NormalizedEmail = "STUDENT@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                StudentId = 1,
            };

            var hashedPassword = passwordHasher.HashPassword(adminUser, "Qwerty-1");

            adminUser.PasswordHash = hashedPassword;

            modelBuilder.Entity<AppUser>().HasData(adminUser);

            modelBuilder.Entity<Group>().HasData(new Group 
            { 
                Id = 1,
                Name = "suicide terrorists",
                TeacherId = 1
            });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = 1,
                GroupId = 1,
                AppUserId = studentUserId
            });    

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = studentRoleId,
                UserId = studentUserId
            });
        }
    }
}
