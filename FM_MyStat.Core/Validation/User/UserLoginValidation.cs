using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.User
{
    public class UserLoginValidation : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidation()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage("Filed must not be empty")
               .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(r => r.Password).NotEmpty().WithMessage("Filed must not be empty")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
