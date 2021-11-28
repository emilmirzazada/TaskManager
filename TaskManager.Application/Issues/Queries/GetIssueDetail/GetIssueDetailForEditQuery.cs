using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Application.Common.Models;

namespace TaskManager.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailForEditQuery : IRequest<IssueDetailVm>
    {
        public long IssueId { get; set; }

        public class GetIssueDetailForEditQueryHandler : IRequestHandler<GetIssueDetailForEditQuery, IssueDetailVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIssueDetailForEditQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueDetailVm> Handle(GetIssueDetailForEditQuery request, CancellationToken cancellationToken)
            {

                var employees = await _context.Employees
                    .Select(e => new FrameDto { Value = e.EmployeeId, Name = e.FullName })
                    .ToListAsync(cancellationToken);


                var vm = new IssueDetailVm
                {
                    Employees = employees
                };

                vm.Issue= await _context.Issues
                    .Include(x => x.IssueAssignees)
                    .ThenInclude(x => x.Assignee)
                    .Include(x => x.Reporter)
                    .FirstOrDefaultAsync(p => p.IssueId == request.IssueId, cancellationToken);

                return vm;
            }
        }
    }
}
