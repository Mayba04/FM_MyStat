using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Homeworks
{
    public class Homework : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public IEnumerable<HomeworkDone> HomeworksDone { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public string? PathFile { get; set; }
    }
}
