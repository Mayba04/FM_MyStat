using Ardalis.Specification;
using FM_MyStat.Core.Entities.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FM_MyStat.Core.Entities.Specifications
{
    public class LessonsSpecification
    {
        public class GetByName : Specification<Lesson>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.Name == name);
            }
        }
        public class GetByteacherId : Specification<Lesson>
        {
            public GetByteacherId(int Id)
            {
                Query.Where(x => x.TeacherId == Id);
            }
        }
    }
}
