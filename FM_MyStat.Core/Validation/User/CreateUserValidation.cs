using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.User
{
    public class CreateUserValidation : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidation()
        {
            RuleFor(r => r.FirstName).MinimumLength(2).NotEmpty().MaximumLength(12);
            RuleFor(r => r.LastName).MinimumLength(2).NotEmpty().MaximumLength(12);
            RuleFor(r => r.SurName).MinimumLength(2).NotEmpty().MaximumLength(12);
            RuleFor(r => r.Email).NotEmpty().WithMessage("Filed must not be empty")
                  .EmailAddress().WithMessage("Invalid email format.");
        }


       
    }
}
