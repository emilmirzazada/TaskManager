using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Employees.Queries.GetEmployeeList;

namespace TaskManager.Application.Common.Extensions
{
    public static class EmployeeDtoExtension
    {
        public static async Task<List<EmployeeDto>> ProjectUserAppRoles(this List<EmployeeDto> employeeDtos, IIdentityService identityService)
        {
            var userRoles = await identityService.FetchUserRolesAsync();

            for (int i = 0; i < employeeDtos.Count; i++)
            {
                employeeDtos[i].RoleName = userRoles.FirstOrDefault(ur => ur.appUserId == employeeDtos[i].AppUserId).role;
            }

            return employeeDtos;
        }
    }
}
