using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailVm>
    {
        public long EmployeeId { get; set; }

        public class GetEmployeeDetailQueryHandler : IRequestHandler<GetEmployeeDetailQuery, EmployeeDetailVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IIdentityService _identityService;

            public GetEmployeeDetailQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
            {
                _context = context;
                _mapper = mapper;
                _identityService = identityService;
            }

            public async Task<EmployeeDetailVm> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Employees
                    .ProjectTo<EmployeeDetailVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.Id == request.EmployeeId, cancellationToken);

                vm.RoleName = await _identityService.GetUserRoleAsync(vm.AppUserId);

                vm.Email = await _identityService.GetUserEmailAsync(vm.AppUserId);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Employee), request.EmployeeId);
                }

                return vm;
            }
        }
    }
}
