using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Employees.Queries.GetEmployeeDetail;
using TaskManager.Application.Identity.Queries.GetAppUserDetail;

namespace TaskManager.WebUI.Employees
{
    [Authorize(Roles = "Admin")]
    public class DetailedModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailedModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public EmployeeDetailVm Employee { get; private set; }

        public async Task OnGetAsync(long id)
        {
            Employee = await _mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id });
            Employee.Email = await _mediator.Send(new GetAppUserDetailQuery { AppUserId = Employee.AppUserId });
        }
    }
}
