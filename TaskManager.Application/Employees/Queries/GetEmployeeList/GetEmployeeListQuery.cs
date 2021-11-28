﻿using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Extensions;

namespace TaskManager.Application.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<EmployeeListVm>
    {
        public long OrganizationId { get; set; }
        public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, EmployeeListVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IIdentityService _identityService;

            public GetEmployeeListQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
            {
                _context = context;
                _mapper = mapper;
                _identityService = identityService;
            }

            public async Task<EmployeeListVm> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
            {
                var employees = await _context.Employees
               .Where(e => e.OrganizationId == request.OrganizationId)
               .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.FullName)
               .ToListAsync(cancellationToken);

                employees = await employees.ProjectUserAppRoles(_identityService);

                var vm = new EmployeeListVm
                {
                    Employees = employees,
                };

                return vm;
            }
        }
    }
}
