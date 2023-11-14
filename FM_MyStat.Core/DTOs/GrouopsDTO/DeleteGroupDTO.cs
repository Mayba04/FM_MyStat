using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM_MyStat.Core.Entities.Homeworks;

namespace FM_MyStat.Core.DTOs.GrouopsDTO
{
    public class DeleteGroupDTO
    {
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Homework> Homeworks { get; set; }
    }
}
