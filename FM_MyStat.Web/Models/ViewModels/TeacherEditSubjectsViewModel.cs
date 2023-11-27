using FM_MyStat.Core.DTOs.SubjectsDTO;

namespace FM_MyStat.Web.Models.ViewModels
{
    public class TeacherEditSubjectsViewModel
    {
        public int TeacherId { get; set; }
        public List<SubjectUpdateDTO> Subjects { get; set; }
    }
}
