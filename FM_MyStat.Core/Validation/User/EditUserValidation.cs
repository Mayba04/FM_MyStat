using FluentValidation;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Validation.User
{
    public class EditUserValidation: AbstractValidator<EditUserDTO>
    {
        public EditUserValidation()
        {
            RuleFor(r => r.FirstName).MinimumLength(2).NotEmpty().MaximumLength(12);
            RuleFor(r => r.LastName).MinimumLength(2).NotEmpty().MaximumLength(12);
            RuleFor(r => r.SurName).MinimumLength(2).NotEmpty().MaximumLength(20);
            RuleFor(r => r.Email).NotEmpty().WithMessage("Field email must not be empty")
              .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(r=>r.PhoneNumber).NotEmpty().MinimumLength(10).MaximumLength(15).WithMessage("Incorect format PhoneNumber");
            RuleFor(r => r.PhoneNumber)
            .NotEmpty().WithMessage("The Phone Number field must be filled.")
            .Must(PhoneNumberValidator).WithMessage("Please enter a valid Ukrainian phone number in the format +380xxxxxxxxx or 0xxxxxxxxx.");
        }

        private bool PhoneNumberValidator(string phoneNumber)
        {
            if (phoneNumber.StartsWith("+380"))
            {
                return Regex.IsMatch(phoneNumber, @"^\+380\d{9}$");
            }
            else if (phoneNumber.StartsWith("0"))
            {
                return Regex.IsMatch(phoneNumber, @"^0\d{9}$");
            }

            return false;
        }
    }
}
