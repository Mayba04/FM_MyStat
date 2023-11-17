using FM_MyStat.Core.DTOs.UsersDTO.User;

namespace FM_MyStat.Web.Models.ViewModels
{
    public class UpdateProfileTeacherVM
    {
        public EditUserDTO TeacherInfo { get; set; }
        public EditUserPasswordDTO UpdatePassword { get; set; }
    }
}
