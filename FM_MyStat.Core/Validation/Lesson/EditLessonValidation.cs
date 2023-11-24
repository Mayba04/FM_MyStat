using FluentValidation;
using FM_MyStat.Core.DTOs.LessonsDTO.Lessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.Lesson
{
    public class EditLessonValidation : AbstractValidator<EditLessonsDTO>
    {
        public EditLessonValidation()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.Description).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.Start).NotEmpty();
            RuleFor(r => r.End).NotEmpty();
            RuleFor(r => r.GroupId).NotEmpty();
            RuleFor(r => r.SubjectId).NotEmpty();
            RuleFor(r => r.TeacherId).NotEmpty();
        }
    }
}
