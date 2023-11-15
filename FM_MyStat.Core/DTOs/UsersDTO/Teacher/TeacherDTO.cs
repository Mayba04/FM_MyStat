using FM_MyStat.Core.Entities;
using FM_MyStat.Core.Entities.Lessons;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.Teacher
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
    }
}
