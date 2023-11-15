using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using System.ComponentModel.DataAnnotations;

namespace FM_MyStat.Core.DTOs.GrouopsDTO
{
    public class GroupDTO
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
    }
}
