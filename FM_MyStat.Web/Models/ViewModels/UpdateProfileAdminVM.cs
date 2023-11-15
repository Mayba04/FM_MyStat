using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using FM_MyStat.Core.DTOs.UsersDTO.User;

namespace FM_MyStat.Web.Models.ViewModels
{
    public class UpdateProfileAdminVM
    {
        public EditUserDTO AdminInfo { get; set; }
        public EditUserPasswordDTO UpdatePassword { get; set; }
    }
}
