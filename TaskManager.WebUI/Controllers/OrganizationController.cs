using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Application.Organizations.Commands.CreateOrganization;

namespace TaskManager.WebUI.Controllers
{
    [Authorize]
    public class OrganizationController : ApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateOrganizationCommand command)
        {
            var result = await Mediator.Send(command);

            if (result > 0)
            {
                return Ok(result);
            }

            return NoContent();
        }
    }
}
