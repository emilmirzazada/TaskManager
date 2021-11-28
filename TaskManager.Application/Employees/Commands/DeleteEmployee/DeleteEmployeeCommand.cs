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

namespace TaskManager.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<string>
    {
        public long Id { get; set; }

        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, string>
        {
            private readonly IApplicationDbContext _context;

            public DeleteEmployeeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Employees.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Employee), request.Id);
                }

                _context.Employees.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.AppUserId;
            }
        }
    }
}
