using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
    {
        public UpdateIssueCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(e => e.Description)
                .MaximumLength(256);
            RuleFor(e => e.ReporterId)
                .NotEmpty();
            RuleFor(e => e.Priority)
                .NotEmpty()
                .IsInEnum();
            RuleFor(e => e.Status)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
