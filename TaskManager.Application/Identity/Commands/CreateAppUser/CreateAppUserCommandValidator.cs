using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Identity.Commands.CreateAppUser
{
    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty();
            RuleFor(e => e.Password)
                .NotEmpty();
        }
    }
}
