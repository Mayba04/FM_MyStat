using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Infrastructure.Initializers;
using Microsoft.AspNetCore.Identity;
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
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext() : base() { }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; } 
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkDone> HomeworksDone { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonMark> LessonMarks { get; set; }
        public DbSet<TeacherSubject> TeachersSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Links
            modelBuilder.Entity<AppUser>()
                .HasOne(user => user.Student).WithOne(student => student.AppUser)
                .HasForeignKey<Student>(student => student.AppUserId).OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<AppUser>()
                .HasOne(user => user.Teacher).WithOne(teacher => teacher.AppUser)
                .HasForeignKey<Teacher>(teacher => teacher.AppUserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
                .HasOne(user => user.Administrator).WithOne(administrator => administrator.AppUser)
                .HasForeignKey<Administrator>(administrator => administrator.AppUserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(student => student.Group).WithMany(group => group.Students)
                .HasForeignKey(student => student.GroupId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(student => student.HomeworksDone).WithOne(homework => homework.Student)
                .HasForeignKey(homework => homework.StudentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(student => student.LessonMarks).WithOne(lessonmark => lessonmark.Student)
                .HasForeignKey(lessonmark => lessonmark.StudentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Group>()
                .HasOne(group => group.Teacher).WithMany(teacher => teacher.Groups)
                .HasForeignKey(group => group.TeacherId);

            modelBuilder.Entity<Group>()
                .HasMany(group => group.Homeworks).WithOne(homework => homework.Group)
                .HasForeignKey(homework => homework.GroupId);

            modelBuilder.Entity<Group>()
                .HasMany(group => group.Lessons).WithOne(lesson => lesson.Group)
                .HasForeignKey(lesson => lesson.GroupId);

            modelBuilder.Entity<Homework>()
                .HasMany(homework => homework.HomeworksDone).WithOne(homeworkdone => homeworkdone.Homework)
                .HasForeignKey(homeworkdone => homeworkdone.HomeworkId);

            modelBuilder.Entity<Lesson>()
                .HasMany(lesson => lesson.LessonMarks).WithOne(lessonmark => lessonmark.Lesson)
                .HasForeignKey(lessonmark => lessonmark.LessonId);

            modelBuilder.Entity<Lesson>()
                .HasOne(lesson => lesson.Teacher).WithMany(teacher => teacher.Lessons)
                .HasForeignKey(lesson => lesson.TeacherId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(lesson => lesson.Homework).WithOne(homework => homework.Lesson)
                .HasForeignKey<Lesson>(lesson => lesson.HomeworkId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lesson>()
                .HasOne(lesson => lesson.Subject).WithMany(subject => subject.Lessons)
                .HasForeignKey(lesson => lesson.SubjectId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne(teachersubject => teachersubject.Teacher).WithMany(teacher => teacher.TeachersSubjects)
                .HasForeignKey(teachersubject => teachersubject.TeacherId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne(teachersubject => teachersubject.Subject).WithMany(subject => subject.TeachersSubjects)
                .HasForeignKey(teachersubject => teachersubject.SubjectId);

            modelBuilder.SeedAdministrator();
            modelBuilder.SeedTeacher();
            modelBuilder.SeedGroup();
            modelBuilder.SeedStudent();
            modelBuilder.SeedSubject();
            modelBuilder.SeedLesson();
            modelBuilder.SeedTeacherSubject();
        }
    }
}
