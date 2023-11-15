using FM_MyStat.Core.DTOs.UsersDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.Student
{
    public class CreateStudentDTO : CreateUserDTO
    {
        public int Rating { get; set; } = 0;
        public int? GroupId { get; set; }
    }
}
