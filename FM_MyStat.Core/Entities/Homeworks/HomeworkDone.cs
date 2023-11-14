using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Homeworks
{
    public class HomeworkDone : IEntity
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public Homework Homework { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int? Mark { get; set; }
        [Required, MaxLength(1024)]
        public string? Description { get; set; }
        [Required, MaxLength(1024)]
        public string? FilePath { get; set; }
    }
}
