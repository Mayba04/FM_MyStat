using FM_MyStat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.Homework
{
    public class HomeworkDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
    }
}
