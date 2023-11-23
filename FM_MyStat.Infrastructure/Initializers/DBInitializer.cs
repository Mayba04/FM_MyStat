using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
            var passwordHasher = new PasswordHasher<AppUser>();
            var adminUserId = Guid.NewGuid().ToString();
            var adminRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });
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
            Administrator administrator = new Administrator
            {
                Id = 1,
                AppUserId = adminUserId
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Qwerty-1");
            modelBuilder.Entity<Administrator>().HasData(administrator);
            modelBuilder.Entity<AppUser>().HasData(adminUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }
        public static void SeedTeacher(this ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var teacherUserId = Guid.NewGuid().ToString();
            var teacherRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = teacherRoleId,
                Name = "Teacher",
                NormalizedName = "TEACHER"
            });
            var teacherUser = new AppUser
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
            Teacher teacher = new Teacher
            {
                Id = 1,
                AppUserId = teacherUserId
            };
            teacherUser.PasswordHash = passwordHasher.HashPassword(teacherUser, "Qwerty-1");
            modelBuilder.Entity<Teacher>().HasData(teacher);
            modelBuilder.Entity<AppUser>().HasData(teacherUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = teacherRoleId,
                UserId = teacherUserId
            });
        }
        public static void SeedStudent(this ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var studentUserId = Guid.NewGuid().ToString();
            var studentRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = studentRoleId,
                Name = "Student",
                NormalizedName = "STUDENT"
            });
            var studentUser = new AppUser
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
            Student student = new Student
            {
                Id = 1,
                GroupId = 1,
                AppUserId = studentUserId
            };
            studentUser.PasswordHash = passwordHasher.HashPassword(studentUser, "Qwerty-1");
            modelBuilder.Entity<Student>().HasData(student);
            modelBuilder.Entity<AppUser>().HasData(studentUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = studentRoleId,
                UserId = studentUserId
            });
        }
        public static void SeedSubject(this ModelBuilder modelBuilder)
        {
            Subject subject = new Subject
            {
                Id = 1,
                Name = "C#"
            };
            modelBuilder.Entity<Subject>().HasData(subject);
        }
        public static void SeedLesson(this ModelBuilder modelBuilder)
        {
            Lesson lesson = new Lesson
            {
                Id = 1,
                Name = "C# beginning",
                Description = "First steps into c# today",
                Start = DateTime.UtcNow.AddHours(1),
                End = DateTime.UtcNow.AddHours(3),
                TeacherId = 1,
                GroupId = 1,
                SubjectId = 1
            };
            modelBuilder.Entity<Lesson>().HasData(lesson);
        }
        public static void SeedGroup(this ModelBuilder modelBuilder)
        {
            Group group = new Group
            {
                Id = 1,
                Name = "suicide terrorists",
                TeacherId = 1
            };
            modelBuilder.Entity<Group>().HasData(group);
        }
        public static void SeedTeacherSubject(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherSubject>().HasData(new TeacherSubject()
            {
                Id = 1,
                SubjectId = 1,
                TeacherId = 1
            });
        }
    }
}
