using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Application.Employees.Commands.DeleteEmployee;
using TaskManager.Application.Employees.Commands.UpdateEmployee;
using TaskManager.Application.Employees.Queries.GetEmployeeDetail;
using TaskManager.Application.Identity.Commands.DeleteAppUser;

namespace TaskManager.WebUI.Employees
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public EmployeeDetailVm Employee { get; private set; }

        public async Task OnGetAsync(long id)
        {
            Employee = await _mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id });
        }

        public async Task<IActionResult> OnPostAsync(long id, UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return RedirectToPage("./Detailed", new { id = command.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            var appUserId = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
            await _mediator.Send(new DeleteAppUserCommand { AppUserId = appUserId });

            return RedirectToPage("./Index");
        }
    }
}
