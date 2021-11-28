using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailQuery : IRequest<Issue>
    {
        public long IssueId { get; set; }

        public class GetIssueDetailQueryHandler : IRequestHandler<GetIssueDetailQuery, Issue>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIssueDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Issue> Handle(GetIssueDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Issues
                    .Include(x => x.IssueAssignees)
                    .ThenInclude(x => x.Assignee)
                    .Include(x => x.Reporter)
                    .FirstOrDefaultAsync(p => p.IssueId == request.IssueId, cancellationToken);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Issue), request.IssueId);
                }

                return vm;
            }
        }
    }
}
