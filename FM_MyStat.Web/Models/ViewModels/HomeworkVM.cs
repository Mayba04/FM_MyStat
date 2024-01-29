using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using FM_MyStat.Core.DTOs.SubjectsDTO;

namespace FM_MyStat.Web.Models.ViewModels
{
    public class HomeworkVM
    {
        public List<HomeworkDoneDTO> homeworkDoneDTOs { get; set; }
        public List<HomeworkDTO> homeworkDTOs { get; set; }
    }
}
