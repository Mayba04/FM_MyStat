using FluentValidation;
using FM_MyStat.Core.DTOs.HomeworksDTO;
using FM_MyStat.Core.DTOs.HomeworksDTO.Homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.Homework
{
    public class SetMarkValidation : AbstractValidator<HomeworkDoneDTO>
    {
        public SetMarkValidation()
        {
            RuleFor(r => r.FilePath).MinimumLength(2).NotEmpty();
        }
    }
}
