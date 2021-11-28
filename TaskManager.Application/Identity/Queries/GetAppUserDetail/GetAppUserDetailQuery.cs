using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Identity.Queries.GetAppUserDetail
{
    public class GetAppUserDetailQuery : IRequest<string>
    {
        public string AppUserId { get; set; }

        public class GetUserAppDetailQueryHandler : IRequestHandler<GetAppUserDetailQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IIdentityService _identityService;

            public GetUserAppDetailQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
            {
                _context = context;
                _mapper = mapper;
                _identityService = identityService;
            }

            public async Task<string> Handle(GetAppUserDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _identityService.GetUserEmailAsync(request.AppUserId);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Employee), request.AppUserId);
                }

                return vm;
            }
        }
    }
}
