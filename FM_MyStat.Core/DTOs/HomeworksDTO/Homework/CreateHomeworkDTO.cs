using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.HomeworksDTO.Homework
{
    public class CreateHomeworkDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
        public int LessonId { get; set; }
        public string? PathFile { get; set; }
        public IFormFileCollection File { get; set; }
    }
}
