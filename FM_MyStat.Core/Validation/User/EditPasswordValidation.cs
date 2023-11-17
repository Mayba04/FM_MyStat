using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.User
{
    public class EditPasswordValidation : AbstractValidator<EditUserPasswordDTO>
    {
        public EditPasswordValidation()
        {
            RuleFor(r => r.OldPassword).NotEmpty().MinimumLength(6);
            RuleFor(r => r.NewPassword).NotEmpty().MinimumLength(6);
            RuleFor(r => r.ConfirmPassword).NotEmpty().MinimumLength(6).Equal(r => r.NewPassword);
        }
    }
}
