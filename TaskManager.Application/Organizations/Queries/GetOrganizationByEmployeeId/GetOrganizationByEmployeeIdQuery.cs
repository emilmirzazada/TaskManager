using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Organizations.Queries.GetOrganizationByEmployeeId
{
    public class GetOrganizationByEmployeeIdQuery : IRequest<Organization>
    {
        public long EmployeeId { get; set; }

        public class GetOrganizationByEmployeeIdQueryHandler : IRequestHandler<GetOrganizationByEmployeeIdQuery, Organization>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetOrganizationByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Organization> Handle(GetOrganizationByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Organizations
                    .FirstOrDefaultAsync(p => p.Id == request.EmployeeId, cancellationToken);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Employee), request.EmployeeId);
                }

                return vm;
            }
        }
    }
}
