using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.HomeworksDTO
{
    public class HomeworkDoneDTO
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public int StudentId { get; set; }
        public int? Mark { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public IFormFileCollection File { get; set; }
    }
}
