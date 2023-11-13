using FluentValidation;
using FM_MyStat.Core.DTOs.Homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation
{
    public class HomeValidation: AbstractValidator<HomeworkDTO>
    {
        public HomeValidation()
        {
            RuleFor(r => r.Title).MinimumLength(2).NotEmpty().MaximumLength(32);
            RuleFor(r => r.Description).MinimumLength(1).NotEmpty();
            RuleFor(r => r.SubjectId).NotEmpty();
            RuleFor(r => r.GroupId).NotEmpty();
        }
    }
}
