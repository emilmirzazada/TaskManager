using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Issues.Queries.GetIssueList
{
    public class GetIssueListQuery : IRequest<IssueListVm>
    {
        public long OrganizationId { get; set; }
        public class GetIssueListQueryHandler : IRequestHandler<GetIssueListQuery, IssueListVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIssueListQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueListVm> Handle(GetIssueListQuery request, CancellationToken cancellationToken)
            {
                var issues = await _context.Issues
                .Where(i => i.OrganizationId == request.OrganizationId)
                .Include(x => x.IssueAssignees)
                .ThenInclude(x => x.Assignee)
                .Include(x => x.Reporter)
                .ToListAsync();

                var vm = new IssueListVm
                {
                    Issues = issues
                };

                return vm;

            }
        }
    }
}
