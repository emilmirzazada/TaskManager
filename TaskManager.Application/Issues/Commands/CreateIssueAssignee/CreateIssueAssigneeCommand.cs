using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Issues.Commands.CreateIssueAssignee
{
    public class CreateIssueAssigneeCommand : IRequest<Unit>
    {
        public long IssueId { get; set; }

        public List<int> AssignedEmployeeIds { get; set; }

        public class CreateIssueAssigneeCommandHandler : IRequestHandler<CreateIssueAssigneeCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public CreateIssueAssigneeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateIssueAssigneeCommand request, CancellationToken cancellationToken)
            {
                foreach (var assigneId in request.AssignedEmployeeIds)
                {
                    var entity = new IssueAssignee
                    {
                        IssueId = request.IssueId,
                        AssigneeId=assigneId
                    };

                    _context.IssueAssignees.Add(entity);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}
