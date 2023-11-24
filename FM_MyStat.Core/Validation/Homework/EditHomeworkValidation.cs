using FluentValidation;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.Homework
{
    public class EditHomeworkValidation : AbstractValidator<EditHomeworkDTO>
    {
        public EditHomeworkValidation()
        {
            RuleFor(r => r.Title).MinimumLength(2).NotEmpty().MaximumLength(32);
            RuleFor(r => r.Description).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.GroupId).NotEmpty();
            RuleFor(r => r.LessonId).NotEmpty();
        }
    }
}
