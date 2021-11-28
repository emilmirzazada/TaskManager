using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Employees.Commands.CreateEmployee;
using TaskManager.Application.Employees.Queries.GetEmployeeDetail;
using TaskManager.Application.Identity.Commands.AssignAppUserRole;
using TaskManager.Application.Identity.Commands.CreateAppUser;

namespace TaskManager.WebUI.Employees
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public CreateModel(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public EmployeeDetailVm Employee { get; private set; }

        public async Task OnGetAsync()
        {
            Employee = await _mediator.Send(new GetEmptyEmployeeQuery());
        }

        public async Task<IActionResult> OnPostAsync(CreateEmployeeCommand command)
        {
            var appUserId = await _mediator.Send(new CreateAppUserCommand { Email = command.Email, Password = command.Password });

            var currentEmployee = await _mediator.Send(new GetEmployeeByAppUserIdQuery { AppUserId = _currentUserService.UserId });

            if (!string.IsNullOrEmpty(appUserId))
            {
                await _mediator.Send(new AssignAppUserRoleCommand { AppUserId = appUserId, Role = command.Role });

                command.AppUserId = appUserId;
                command.OrganizationId = currentEmployee.OrganizationId;
                var result = await _mediator.Send(command);

                if (result > 0)
                {
                    return RedirectToPage("./Detailed", new { id = result });
                }
            }            
            return BadRequest();
        }
    }
}
