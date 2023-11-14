using FM_MyStat.Core.Entities.Homeworks;
using FM_MyStat.Core.Entities.Lessons;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Users
{
    public class Student : AppUser
    {
        public int Rating { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public IEnumerable<LessonMark> LessonMarks { get; set; }
        public IEnumerable<HomeworkDone> HomeworksDone{ get; set; }
    }
}
