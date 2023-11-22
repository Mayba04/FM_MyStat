using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.User
{
    public class ResetPasswordValidation : AbstractValidator<PasswordRecoveryDTO>
    {
        public ResetPasswordValidation()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage("Filed must not be empty")
               .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(r => r.Token).NotEmpty().WithMessage("A password recovery error occurred");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Filed must not be empty")
                 .MinimumLength(6).WithMessage("Password must be at least 6 characters");
            RuleFor(r => r.ConfirmPassword).NotEmpty().WithMessage("Filed must not be empty").
                MinimumLength(6).WithMessage("Password must be at least 6 characters").
                Equal(p => p.Password).WithMessage("The verification password is incorrect");
        }
    }
}
