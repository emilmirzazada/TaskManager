using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enumerations;

namespace TaskManager.Application.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommand : IRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public List<int> AssignedEmployeeIds { get; set; }
        public PriorityLevel Priority { get; set; }

        public IssueStatus Status { get; set; }

        public long ReporterId { get; set; }

        public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateIssueCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Issues.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Issue), request.Id);
                }

                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.Priority = request.Priority;
                entity.ReporterId = request.ReporterId;
                entity.Status = request.Status;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
