using Business.Requests.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.Validation.FluentValidation.User
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().MinimumLength(2).MaximumLength(15);
            RuleFor(u => u.LastName).NotEmpty().MinimumLength(2).MaximumLength(15);
            RuleFor(u => u.Email).EmailAddress();
        }


    }
}
