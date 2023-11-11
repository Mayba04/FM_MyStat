using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Infrastructure.Initializers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Utilities.IO.Pem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Infrastructure.Context
{
    internal class AppDBContext : IdentityDbContext
    {
        public AppDBContext() : base() { }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; } 
        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           ;
            modelBuilder.Entity<Student>()
             .HasOne(s => s.Group)
             .WithMany(g => g.Students)
             .HasForeignKey(s => s.GroupId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Administrator>(E => { E.ToTable("Administrators"); });
            modelBuilder.Entity<Teacher>(E => { E.ToTable("Teachers"); });
            modelBuilder.Entity<Student>(E => { E.ToTable("Students"); });
            modelBuilder.SeedAdministrator();
            modelBuilder.SeedTeacher();
            modelBuilder.SeedStudent();
        }
    }
}
