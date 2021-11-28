using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Identity.Commands.DeleteAppUser
{
    public class DeleteAppUserCommand : IRequest
    {
        public string AppUserId { get; set; }

        public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand>
        {
            private readonly IIdentityService _identityService;

            public DeleteAppUserCommandHandler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
            {
                if (!await _identityService.IsUserExistAsync(request.AppUserId))
                {
                    await _identityService.DeleteUserAsync(request.AppUserId);
                }
                return Unit.Value;
            }
        }
    }
}
