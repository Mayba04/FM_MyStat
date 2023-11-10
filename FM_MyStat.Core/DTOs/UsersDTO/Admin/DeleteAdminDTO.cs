using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.Admin
{
    public class DeleteAdminDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
