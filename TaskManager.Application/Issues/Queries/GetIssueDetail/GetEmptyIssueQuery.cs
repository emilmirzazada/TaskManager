using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Models;

namespace TaskManager.Application.Issues.Queries.GetIssueDetail
{
    public class GetEmptyIssueQuery : IRequest<IssueDetailVm>
    {
        public long OrganizationId { get; set; }
        public class GetEmptyIssueQueryHandler : IRequestHandler<GetEmptyIssueQuery, IssueDetailVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEmptyIssueQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueDetailVm> Handle(GetEmptyIssueQuery request, CancellationToken cancellationToken)
            {
                var employees = await _context.Employees
                    .Where(x=>x.OrganizationId==request.OrganizationId)
                    .Select(e => new FrameDto { Value = e.EmployeeId, Name = e.FullName })
                    .ToListAsync(cancellationToken);

                var vm = new IssueDetailVm
                {
                    Employees = employees,
                };

                return vm;
            }
        }
    }
}
