using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Models;
using TaskManager.Domain.Enumerations;

namespace TaskManager.Application.Identity.Commands.AssignAppUserRole
{
    public class AssignAppUserRoleCommand : IRequest<Result>
    {
        public string AppUserId { get; set; }

        public UserRole Role { get; set; }

        public class CreateAppUserCommandHandler : IRequestHandler<AssignAppUserRoleCommand, Result>
        {
            private readonly IIdentityService _identityService;

            public CreateAppUserCommandHandler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Result> Handle(AssignAppUserRoleCommand request, CancellationToken cancellationToken)
            {
                return await _identityService.AddRoleToUserAsync(request.AppUserId, request.Role.ToString());
            }
        }
    }
}
