using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Models;
using TaskManager.Application.Employees.Queries.GetEmployeeDetail;
using TaskManager.Application.Issues.Commands.CreateIssue;
using TaskManager.Application.Issues.Commands.CreateIssueAssignee;
using TaskManager.Application.Issues.Queries.GetIssueDetail;

namespace TaskManager.WebUI.Issues
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailService _emailService;

        public CreateModel(IMediator mediator, ICurrentUserService currentUserService, IEmailService emailService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
            _emailService = emailService;
        }

        public IssueDetailVm IssueDetail { get; set; }

        public async Task OnGetAsync()
        {
            var currentEmployee = await _mediator.Send(new GetEmployeeByAppUserIdQuery { AppUserId = _currentUserService.UserId });
            IssueDetail = await _mediator.Send(new GetEmptyIssueQuery { OrganizationId = currentEmployee.OrganizationId });
        }

        public async Task<IActionResult> OnPostAsync(CreateIssueCommand command)
        {
            var currentEmployee = await _mediator.Send(new GetEmployeeByAppUserIdQuery { AppUserId = _currentUserService.UserId });

            foreach (var assigneId in command.AssignedEmployeeIds)
            {
                var assigneEmployee = await _mediator.Send(new GetEmployeeDetailQuery { EmployeeId = assigneId });
                await _emailService.SendAsync(new EmailRequest
                {
                    Subject = "Task Manager",
                    Body = $"New task assigned to you by {currentEmployee.Email}",
                    To = assigneEmployee.Email
                });
            }

            command.OrganizationId = currentEmployee.OrganizationId;
            command.ReporterId = currentEmployee.Id;
            var result = await _mediator.Send(command);

            await _mediator.Send(new CreateIssueAssigneeCommand {IssueId=result,AssignedEmployeeIds=command.AssignedEmployeeIds });

            if (result > 0)
            {
                return RedirectToPage("./Detailed", new { id = result });
            }
            return BadRequest(result);
        }
    }
}
