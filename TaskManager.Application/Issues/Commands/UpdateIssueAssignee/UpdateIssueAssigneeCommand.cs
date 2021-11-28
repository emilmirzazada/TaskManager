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

namespace TaskManager.Application.Issues.Commands.UpdateIssueAssignee
{
    public class UpdateIssueAssigneeCommand : IRequest
    {
        public long IssueId { get; set; }

        public List<int> AssignedEmployeeIds { get; set; }

        public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueAssigneeCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateIssueCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateIssueAssigneeCommand request, CancellationToken cancellationToken)
            {
                var issueAssignees = _context.IssueAssignees.Where(x => x.IssueId == request.IssueId);

                _context.IssueAssignees.RemoveRange(issueAssignees);
                await _context.SaveChangesAsync(cancellationToken);

                foreach (var assigneId in request.AssignedEmployeeIds)
                {
                    var newEntity = new IssueAssignee
                    {
                        IssueId = request.IssueId,
                        AssigneeId = assigneId
                    };

                    _context.IssueAssignees.Add(newEntity);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}
