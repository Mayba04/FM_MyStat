using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.LessonsDTO.Lessons
{
    public class CreateLessonsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TeacherId { get; set; }
        public int? HomeworkId { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
    }
}
