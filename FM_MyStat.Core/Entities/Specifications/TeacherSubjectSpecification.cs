using Ardalis.Specification;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Specifications
{
    public class TeacherSubjectSpecification : AbstractValidator<TeacherSubject>
    {
        public class GetByTeacherId : Specification<TeacherSubject>
        {
            public GetByTeacherId(int Id)
            {
                Query.Where(a => a.TeacherId == Id);
            }
        }
        public class GetBySubjectId : Specification<TeacherSubject>
        {
            public GetBySubjectId(int Id)
            {
                Query.Where(a => a.SubjectId == Id);
            }
        }
    }
}
