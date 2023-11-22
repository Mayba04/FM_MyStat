using FluentValidation;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.Homework
{
    public class CreateHomeworkValidation : AbstractValidator<CreateHomeworkDTO>
    {
        public CreateHomeworkValidation() 
        {
            RuleFor(r => r.Title).MinimumLength(2).NotEmpty().MaximumLength(32);
        }
    }
}
