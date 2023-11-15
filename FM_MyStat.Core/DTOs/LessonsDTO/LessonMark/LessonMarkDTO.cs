using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.LessonsDTO.LessonMark
{
    public class LessonMarkDTO
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public int LessonId { get; set; }
        public int StudentId { get; set; }
    }
}
