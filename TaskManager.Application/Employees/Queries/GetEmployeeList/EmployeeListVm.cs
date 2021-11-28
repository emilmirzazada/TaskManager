using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Employees.Queries.GetEmployeeList
{
    public class EmployeeListVm
    {
        public IList<EmployeeDto> Employees { get; set; }
    }
}
