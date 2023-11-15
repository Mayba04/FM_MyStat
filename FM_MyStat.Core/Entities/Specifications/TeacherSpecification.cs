using Ardalis.Specification;
using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.Student;
using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FM_MyStat.Core.Entities.Specifications
{
    public class TeacherSpecification: AbstractValidator<TeacherDTO>
    {
        public class GetById : Specification<Teacher>
        {
            public GetById(int Id)
            {
                Query.Where(x => x.Id == Id);
            }
        }
    }
}
