using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FM_MyStat.Core.Entities.Homeworks;

namespace FM_MyStat.Core.DTOs.GrouopsDTO
{
    public class EditGroupDTO
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
    }
}
