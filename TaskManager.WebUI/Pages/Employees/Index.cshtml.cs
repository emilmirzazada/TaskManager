using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Employees.Queries.GetEmployeeDetail;
using TaskManager.Application.Employees.Queries.GetEmployeeList;
using TaskManager.Application.Organizations.Queries.GetOrganizationByEmployeeId;

namespace TaskManager.WebUI.Employees
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public IndexModel(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public IEnumerable<EmployeeDto> Employees { get; private set; }

        public async Task OnGetAsync()
        {
            var organizationId = (await _mediator.Send(new GetEmployeeByAppUserIdQuery { AppUserId = _currentUserService.UserId })).OrganizationId;
            Employees = (await _mediator.Send(new GetEmployeeListQuery { OrganizationId=organizationId })).Employees;
        }
    }
}
