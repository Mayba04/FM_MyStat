using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Users
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(64)]
        public string SurName { get; set; } = string.Empty;
        [Required, MaxLength(64)]
        public string LastName { get; set; } = string.Empty;
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int? AdministratorId { get; set; }
        public Administrator? Administrator { get; set; }
    }
}
