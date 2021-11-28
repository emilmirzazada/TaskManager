using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Common.Mappings;
using TaskManager.Application.Common.Models;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enumerations;

namespace TaskManager.Application.Employees.Queries.GetEmployeeDetail
{
    public class EmployeeDetailVm : IMapFrom<Employee>
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public string AppUserId { get; set; }

        [Display(Name = "ShortName")]
        [Required(ErrorMessage = "ShortName is required")]
        public string ShortName { get; set; }

        [Display(Name = "FullName")]
        [Required(ErrorMessage = "FullName is required")]
        [MinLength(5, ErrorMessage = "Min length should be 5 symbols")]
        [MaxLength(64, ErrorMessage = "Max length should be 64 symbols")]
        public string FullName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Email is required")]
        [MinLength(5, ErrorMessage = "Min length should be 5 symbols")]
        [MaxLength(64, ErrorMessage = "Max length should be 64 symbols")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Min length should be 5 symbols")]
        [MaxLength(64, ErrorMessage = "Max length should be 64 symbols")]
        public string Password { get; set; }

        [Display(Name = "Select Role")]
        public UserRole Role { get; set; }

        public string RoleName { get; set; }

        public IList<FrameDto> UserRoles =
            Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(r => new FrameDto { Value = (int)r, Name = r.ToString() })
            .ToList();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDetailVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(e => e.EmployeeId));
        }
    }
}
