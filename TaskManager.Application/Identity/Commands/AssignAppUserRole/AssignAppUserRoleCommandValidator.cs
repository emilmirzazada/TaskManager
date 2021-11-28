using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Identity.Commands.AssignAppUserRole
{
    public class AssignAppUserRoleCommandValidator : AbstractValidator<AssignAppUserRoleCommand>
    {
        public AssignAppUserRoleCommandValidator()
        {
            RuleFor(e => e.AppUserId)
                .NotEmpty();
            RuleFor(e => e.Role)
                .NotEmpty();
        }
    }
}
