using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Issues.Queries.GetIssueList;
using TaskManager.Application.Employees.Queries.GetEmployeeDetail;
using TaskManager.Domain.Entities;

namespace TaskManager.WebUI.Issues
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private IssueListVm _issueVm;

        public IndexModel(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public Dictionary<string, IEnumerable<Issue>> IssueCatalog { get; private set; }

        public async Task OnGetAsync()
        {
            var currentEmployee = await _mediator.Send(new GetEmployeeByAppUserIdQuery { AppUserId = _currentUserService.UserId });

            _issueVm = await _mediator.Send(new GetIssueListQuery { OrganizationId=currentEmployee.OrganizationId});

            var assignedIssues = _issueVm.Issues.Where(i => i.IssueAssignees.Any(x=>x.AssigneeId == currentEmployee.Id));

            /*var assignedIssues2 = _issueVm.Issues.Where(i => i.AssigneeId.Value == currentEmployee.Id);*/
            var reportedIssues = _issueVm.Issues.Where(i => i.ReporterId.Value == currentEmployee.Id);
            var otherIssues = _issueVm.Issues.Except(assignedIssues).Except(reportedIssues);

            IssueCatalog = new Dictionary<string, IEnumerable<Issue>>
            {
                {"Task assigned to me:", assignedIssues },
                {"Task reported by me:", reportedIssues },
                {"Other tasks:", otherIssues },
            };
        }
    }
}
