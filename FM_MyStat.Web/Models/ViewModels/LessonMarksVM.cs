using FM_MyStat.Core.DTOs.LessonsDTO.LessonMark;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using FM_MyStat.Core.DTOs.UsersDTO.Student;

namespace FM_MyStat.Web.Models.ViewModels
{
    public class LessonMarksVM
    {
        public List<StudentDTO> students {  get; set; }
        public LessonMarkDTO mark { get; set; }
        public LessonDTO lesson { get; set; }
    }
}
