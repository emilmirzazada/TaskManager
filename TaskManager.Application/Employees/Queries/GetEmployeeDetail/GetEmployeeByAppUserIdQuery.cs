using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Application.Common.Exceptions;

namespace TaskManager.Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeByAppUserIdQuery : IRequest<EmployeeDetailVm>
    {
        public string AppUserId { get; set; }

        public class GetEmployeeByAppUserIdQueryHandler : IRequestHandler<GetEmployeeByAppUserIdQuery, EmployeeDetailVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IIdentityService _identityService;

            public GetEmployeeByAppUserIdQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
            {
                _context = context;
                _mapper = mapper;
                _identityService = identityService;
            }

            public async Task<EmployeeDetailVm> Handle(GetEmployeeByAppUserIdQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Employees
                    .ProjectTo<EmployeeDetailVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.AppUserId == request.AppUserId, cancellationToken);

                vm.Email = await _identityService.GetUserEmailAsync(vm.AppUserId);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Employee), request.AppUserId);
                }

                return vm;
            }
        }
    }
}
