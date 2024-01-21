using FluentValidation;
using FM_MyStat.Core.DTOs.NewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.News
{
    public class CreateNewsValidation : AbstractValidator<CreateNewsDTO>
    {
        public CreateNewsValidation()
        {
            RuleFor(n => n.Title).NotEmpty().MinimumLength(5).MaximumLength(64);
            RuleFor(n => n.Description).NotEmpty().MinimumLength(5).MaximumLength(256);
            RuleFor(n => n.TimePublication).NotEmpty();
            RuleFor(n => n.Time).NotEmpty();
        }
    }
}
