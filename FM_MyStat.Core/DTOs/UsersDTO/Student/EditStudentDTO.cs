using FM_MyStat.Core.DTOs.UsersDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.Student
{
    public class EditStudentDTO : EditUserDTO
    {
        public int Rating { get; set; }
        public int? GroupId { get; set; }
    }
}
