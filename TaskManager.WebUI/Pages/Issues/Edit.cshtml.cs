using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Employees.Queries.GetEmployeeDetail;
using TaskManager.Application.Issues.Commands.DeleteIssue;
using TaskManager.Application.Issues.Commands.UpdateIssue;
using TaskManager.Application.Issues.Commands.UpdateIssueAssignee;
using TaskManager.Application.Issues.Queries.GetIssueDetail;

namespace TaskManager.WebUI.Issues
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public EditModel(IMediator mediator,ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public IssueDetailVm IssueDetail { get; private set; }

        public async Task OnGetAsync(long id)
        {
            IssueDetail = await _mediator.Send(new GetIssueDetailForEditQuery { IssueId = id });
        }

        public async Task<IActionResult> OnPostAsync(long id, UpdateIssueCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var currentEmployee = await _mediator.Send(new GetEmployeeByAppUserIdQuery { AppUserId = _currentUserService.UserId });
            command.ReporterId = currentEmployee.Id;

            await _mediator.Send(command);

            await _mediator.Send(new UpdateIssueAssigneeCommand {IssueId=id,AssignedEmployeeIds=command.AssignedEmployeeIds });

            return RedirectToPage("./Detailed", new { id = command.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            await _mediator.Send(new DeleteIssueCommand { Id = id });

            return RedirectToPage("./Index");
        }
    }
}
