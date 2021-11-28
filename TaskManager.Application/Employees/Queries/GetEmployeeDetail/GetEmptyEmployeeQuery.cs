using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmptyEmployeeQuery : IRequest<EmployeeDetailVm>
    {
        public class GetEmptyEmployeeQueryHandler : IRequestHandler<GetEmptyEmployeeQuery, EmployeeDetailVm>
        {
            public async Task<EmployeeDetailVm> Handle(GetEmptyEmployeeQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => new EmployeeDetailVm());
            }
        }
    }
}
