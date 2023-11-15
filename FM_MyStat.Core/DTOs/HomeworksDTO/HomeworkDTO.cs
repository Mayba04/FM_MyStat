using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.HomeworksDTO
{
    public class HomeworkDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int GroupId { get; set; }
        public int LessonId { get; set; }
        public string? PathFile { get; set; }
    }
}
