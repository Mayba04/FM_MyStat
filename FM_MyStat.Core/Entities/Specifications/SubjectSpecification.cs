using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FM_MyStat.Core.Entities.Specifications
{
    public class SubjectSpecification
    {
        public class GetByName : Specification<Subject>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.Name == name);
            }
        }
    }
}
