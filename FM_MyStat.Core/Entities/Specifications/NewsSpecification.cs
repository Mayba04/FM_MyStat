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
    public class NewsSpecification
    {
        public class GetByTop3News_Fuhrer_is_not_a_good_person : Specification<News>
        {
            public GetByTop3News_Fuhrer_is_not_a_good_person()
            {
                DateTime currentDate = DateTime.UtcNow;
                Query.Where(news => news.Time > currentDate).OrderBy(news => news.Time).Take(3);
            }
        }
    }
}
