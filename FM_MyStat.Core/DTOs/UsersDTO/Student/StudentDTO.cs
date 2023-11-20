using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.Student
{
    public class StudentDTO : UserDTO
    {
        public int Rating { get; set; }
        public int? GroupId { get; set; }
        public string Group { get; set; }
    }
}
