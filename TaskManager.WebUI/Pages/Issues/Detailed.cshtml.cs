using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Issues.Queries.GetIssueDetail;
using TaskManager.Domain.Entities;

namespace TaskManager.WebUI.Issues
{
    [Authorize]
    public class DetailedModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailedModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Issue Issue { get; private set; }

        public async Task OnGetAsync(long id)
        {
            Issue = await _mediator.Send(new GetIssueDetailQuery { IssueId = id });
        }
    }
}
