using FM_MyStat.Core.DTOs.UsersDTO.User;

namespace FM_MyStat.Web.Models.ViewModels
{
    public class UpdateProfileStudentVM
    {
        public EditUserDTO StudentInfo { get; set; }
        public EditUserPasswordDTO UpdatePassword { get; set; }
    }
}
