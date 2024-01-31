using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Tests;
using FM_MyStat.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Users
{
    public class Student : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int Rating { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public IEnumerable<LessonMark> LessonMarks { get; set; }
        public IEnumerable<HomeworkDone> HomeworksDone{ get; set; }
        public IEnumerable<TestDone> TestDones { get; set; }
    }
}
