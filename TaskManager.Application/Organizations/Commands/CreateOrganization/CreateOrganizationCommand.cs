using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Organizations.Commands.CreateOrganization
{
    public class CreateOrganizationCommand : IRequest<long>
    {
        public string Name { get; set; }

        public string Phonenumber { get; set; }

        public string Address { get; set; }

        public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateOrganizationCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
            {
                var entity = new Organization
                {
                    Name = request.Name,
                    Phonenumber = request.Phonenumber,
                    Address = request.Address,
                };

                _context.Organizations.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
