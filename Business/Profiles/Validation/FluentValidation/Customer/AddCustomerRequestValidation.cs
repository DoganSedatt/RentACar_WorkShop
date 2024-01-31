using Business.Requests.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.Validation.FluentValidation.Customer
{
    public class AddCustomerRequestValidation: AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerRequestValidation()
        {
            RuleFor(c => c.UserId).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(c => c.LastName).NotEmpty().MinimumLength(2).MaximumLength(20);

        }
    }
}
