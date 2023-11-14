using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Lessons
{
    public class Lesson : IEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(128)]
        public string Name { get; set; }
        [Required, MaxLength(1024)]
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int? HomeworkId { get; set; }
        public Homework? Homework { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public IEnumerable<LessonMark> LessonMarks { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
