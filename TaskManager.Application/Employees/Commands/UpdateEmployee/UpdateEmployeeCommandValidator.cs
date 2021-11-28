using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(64);
            RuleFor(e => e.Password)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(e => e.FullName)
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}
