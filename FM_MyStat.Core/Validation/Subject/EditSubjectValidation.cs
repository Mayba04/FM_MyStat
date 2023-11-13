using FluentValidation;
using FM_MyStat.Core.DTOs.SubjectsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.Subject
{
    public class EditSubjectValidation : AbstractValidator<EditSubjectDTO>
    {
        public EditSubjectValidation()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(64).MinimumLength(2);
        }
    }
}
