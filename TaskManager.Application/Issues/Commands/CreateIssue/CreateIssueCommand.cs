using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enumerations;

namespace TaskManager.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommand : IRequest<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public List<int> AssignedEmployeeIds { get; set; }
        public long OrganizationId { get; set; }

        public PriorityLevel Priority { get; set; }

        public long ReporterId { get; set; }

        public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateIssueCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
            {
                var entity = new Issue
                {
                    Name = request.Name,
                    Description = request.Description,
                    OrganizationId=request.OrganizationId,
                    Priority = request.Priority,
                    Status = IssueStatus.New,
                    ReporterId = request.ReporterId,
                };

                _context.Issues.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IssueId;
            }
        }
    }
}
