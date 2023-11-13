using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.User.Admin
{
    public class LoginAdminValidation : AbstractValidator<LoginAdminDTO>
    {
        public LoginAdminValidation()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty().WithMessage("Filed must not be empty")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
