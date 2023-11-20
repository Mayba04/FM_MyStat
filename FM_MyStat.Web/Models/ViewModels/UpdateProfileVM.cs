using FM_MyStat.Core.DTOs.UsersDTO.User;

namespace FM_MyStat.Web.Models.ViewModels
{
    public class UpdateProfileVM
    {
        public EditUserDTO UserInfo { get; set; }

        public EditUserPasswordDTO UserPassword { get; set; }
    }
}
