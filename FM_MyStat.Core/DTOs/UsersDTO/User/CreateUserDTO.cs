using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.User
{
    public class CreateUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        public int? AdministratorId { get; set; }
        [RegularExpression(@"^(\+?38)?[0-9]+$", ErrorMessage = "Invalid format PhoneNumber.")]
        public string PhoneNumber { get; set; }

    }
}
