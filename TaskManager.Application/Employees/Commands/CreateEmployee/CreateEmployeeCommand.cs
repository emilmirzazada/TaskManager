using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enumerations;

namespace TaskManager.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<long>
    {
        public string AppUserId { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public long  OrganizationId { get; set; }

        public UserRole Role { get; set; }

        public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateEmployeeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = new Employee
                {
                    ShortName = request.ShortName,
                    FullName = request.FullName,
                    AppUserId = request.AppUserId,
                    OrganizationId=request.OrganizationId
                };

                _context.Employees.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.EmployeeId;
            }
        }
    }
}
