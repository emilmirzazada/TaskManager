using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Common.Mappings;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Employees.Queries.GetEmployeeList
{
    public class EmployeeDto : IMapFrom<Employee>
    {
        public long Id { get; set; }

        public string AppUserId { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }
        public long OrganizationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(e => e.EmployeeId));
        }
    }
}
