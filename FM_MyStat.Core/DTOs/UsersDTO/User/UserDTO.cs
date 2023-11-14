using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.User
{
    public class UserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        public int? AdministratorId { get; set; }
    }
}
