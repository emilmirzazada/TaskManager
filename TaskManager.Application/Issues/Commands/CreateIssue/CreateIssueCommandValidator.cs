using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueCommandValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(i => i.Description)
                .MaximumLength(256);
            
            RuleFor(i => i.ReporterId)
                .NotEmpty();
            RuleFor(i => i.Priority)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
