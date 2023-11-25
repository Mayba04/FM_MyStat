using FluentValidation;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.Homework
{
    public class CreateHomeworkValidation : AbstractValidator<HomeworkDTO>
    {
        public CreateHomeworkValidation() 
        {
            RuleFor(r => r.Title).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.Description).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.LessonId).NotEmpty();
            RuleFor(r => r.GroupId).NotEmpty();
        }
    }
}
