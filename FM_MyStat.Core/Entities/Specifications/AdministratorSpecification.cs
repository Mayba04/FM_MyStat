using Ardalis.Specification;
using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
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
    public class AdministratorSpecification : AbstractValidator<Administrator>
    {
        public class GetByAppUserId : Specification<Administrator>
        {
            public GetByAppUserId(string Id)
            {
                Query.Where(a => a.AppUserId == Id);
            }
        }
    }
}
