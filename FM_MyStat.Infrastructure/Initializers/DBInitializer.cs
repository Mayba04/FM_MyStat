using FM_MyStat.Core.Entities;
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
    internal static class DBInitializer
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

            modelBuilder.Entity<Administrator>().HasData(new Administrator
            {
                Id = adminUserId,
                FirstName = "John",
                LastName = "Connor",
                SurName = "Johnovych",
                UserName = "admi@gmail.com",
                Email = "admi@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }

        public static void SeedTeacher(this ModelBuilder modelBuilder)
        {
            var teacherId = Guid.NewGuid().ToString();
            var teacherRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = teacherRoleId,
                Name = "Teacher",
                NormalizedName = "TEACHER"
            });


            modelBuilder.Entity<Teacher>().HasData(new Teacher
            {
                Id = teacherId,
                FirstName = "John",
                LastName = "Doe",
                SurName = "Johnovych",
                UserName = "teacher@gmail.com",
                Email = "teacher@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = teacherId,
                RoleId = teacherRoleId
            });

            modelBuilder.Entity<Group>().HasData(new Group
            {
                Id = 1,
                Name = "PD116",
                TeacherId = teacherId
            });
        }

        public static void SeedStudent(this ModelBuilder modelBuilder)
        {
            var studentId = Guid.NewGuid().ToString();
            var studentRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = studentRoleId,
                Name = "Student",
                NormalizedName = "STUDENT"
            });

            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = studentId,
                FirstName = "John",
                LastName = "Wick",
                SurName = "Johnovych",
                UserName = "student@gmail.com",
                Email = "student@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
                GroupId = 1,
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = studentId,
                RoleId = studentRoleId
            });
        }

        

        
    }
}
