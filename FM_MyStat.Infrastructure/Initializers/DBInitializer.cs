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
    public static class DBInitializer
    {
        public static void SeedAdministrator(this ModelBuilder modelBuilder)
        {
            /*Administrator admin = new Administrator()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Connor",
                SurName = "Johnovych",
                UserName = "admi@gmail.com",
                Email = "admi@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true
            };
            IdentityRole role = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);
            modelBuilder.Entity<Administrator>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = admin.Id,
            });*/
        }

        public static void SeedTeacher(this ModelBuilder modelBuilder)
        {
            /*IdentityRole role = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Teacher",
                NormalizedName = "TEACHER"
            };

            Teacher teacher = new Teacher()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Doe",
                SurName = "Johnovych",
                UserName = "teacher@gmail.com",
                Email = "teacher@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
            };

            modelBuilder.Entity<Teacher>().HasData(teacher);
            modelBuilder.Entity<IdentityRole>().HasData(role);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = teacher.Id,
                RoleId = role.Id
            });*/
        }

        public static void SeedStudent(this ModelBuilder modelBuilder)
        {
            /*IdentityRole role = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Student",
                NormalizedName = "STUDENT"
            };
            Student student = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Wick",
                SurName = "Johnovych",
                UserName = "student@gmail.com",
                Email = "student@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);
            modelBuilder.Entity<Student>().HasData(student);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = student.Id,
                RoleId = role.Id
            });*/
        }
    }
}
