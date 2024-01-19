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
            var adminUserId1 = Guid.NewGuid().ToString();
            var adminUser1 = new AppUser
            {
                Id = adminUserId1,
                FirstName = "John",
                LastName = "Connor",
                SurName = "Johnovych",
                UserName = "admin1@email.com",
                NormalizedUserName = "ADMIN1@EMAIL.COM",
                Email = "admin1@email.com",
                NormalizedEmail = "ADMIN1@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                AdministratorId = 2,
            };
            Administrator administrator1 = new Administrator
            {
                Id = 2,
                AppUserId = adminUserId1
            };
            adminUser1.PasswordHash = passwordHasher.HashPassword(adminUser1, "Qwerty-1");
            modelBuilder.Entity<Administrator>().HasData(administrator1);
            modelBuilder.Entity<AppUser>().HasData(adminUser1);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId1
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
            var teacherUserId1 = Guid.NewGuid().ToString();
            var teacherUser1 = new AppUser
            {
                Id = teacherUserId1,
                FirstName = "Serhiy",
                LastName = "Stadnyk",
                SurName = "Viacheslavovich",
                UserName = "teacher1@email.com",
                NormalizedUserName = "TEACHER1@EMAIL.COM",
                Email = "teacher1@email.com",
                NormalizedEmail = "TEACHER1@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                TeacherId = 2,
            };
            Teacher teacher1 = new Teacher
            {
                Id = 2,
                AppUserId = teacherUserId1
            };
            teacherUser1.PasswordHash = passwordHasher.HashPassword(teacherUser1, "Qwerty-1");
            modelBuilder.Entity<Teacher>().HasData(teacher1);
            modelBuilder.Entity<AppUser>().HasData(teacherUser1);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = teacherRoleId,
                UserId = teacherUserId1
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
            var studentUserId1 = Guid.NewGuid().ToString();
            var studentUser1 = new AppUser
            {
                Id = studentUserId1,
                FirstName = "Dima",
                LastName = "Shostak",
                SurName = "Oleksiyovich",
                UserName = "student1@email.com",
                NormalizedUserName = "STUDENT1@EMAIL.COM",
                Email = "student1@email.com",
                NormalizedEmail = "STUDENT1@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                StudentId = 2,
            };
            Student student1 = new Student
            {
                Id = 2,
                GroupId = 1,
                AppUserId = studentUserId1
            };
            studentUser1.PasswordHash = passwordHasher.HashPassword(studentUser1, "Qwerty-1");
            modelBuilder.Entity<Student>().HasData(student1);
            modelBuilder.Entity<AppUser>().HasData(studentUser1);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = studentRoleId,
                UserId = studentUserId1
            });
            var studentUserId2 = Guid.NewGuid().ToString();
            var studentUser2 = new AppUser
            {
                Id = studentUserId2,
                FirstName = "Yurii",
                LastName = "Bortnik",
                SurName = "Andriyovich",
                UserName = "student2@email.com",
                NormalizedUserName = "STUDENT2@EMAIL.COM",
                Email = "student2@email.com",
                NormalizedEmail = "STUDENT2@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                StudentId = 3,
            };
            Student student2 = new Student
            {
                Id = 3,
                GroupId = 2,
                AppUserId = studentUserId2
            };
            studentUser2.PasswordHash = passwordHasher.HashPassword(studentUser2, "Qwerty-1");
            modelBuilder.Entity<Student>().HasData(student2);
            modelBuilder.Entity<AppUser>().HasData(studentUser2);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = studentRoleId,
                UserId = studentUserId2
            });
            var studentUserId3 = Guid.NewGuid().ToString();
            var studentUser3 = new AppUser
            {
                Id = studentUserId3,
                FirstName = "Pavlo",
                LastName = "Mayba",
                SurName = "Ivanovich",
                UserName = "student3@email.com",
                NormalizedUserName = "STUDENT3@EMAIL.COM",
                Email = "student3@email.com",
                NormalizedEmail = "STUDENT3@EMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                StudentId = 4,
            };
            Student student3 = new Student
            {
                Id = 4,
                GroupId = 2,
                AppUserId = studentUserId3
            };
            studentUser3.PasswordHash = passwordHasher.HashPassword(studentUser3, "Qwerty-1");
            modelBuilder.Entity<Student>().HasData(student3);
            modelBuilder.Entity<AppUser>().HasData(studentUser3);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = studentRoleId,
                UserId = studentUserId3
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
            Group group1 = new Group
            {
                Id = 2,
                Name = "Bad Boys",
                TeacherId = 2
            };
            modelBuilder.Entity<Group>().HasData(group1);
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
