using Ardalis.Specification;
using FM_MyStat.Core.Entities.Homeworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FM_MyStat.Core.Entities.Specifications
{
    public class HomeworkSpecification
    {
        public class GetByName : Specification<Homework>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.Title == name);
            }
        }
    }
}
