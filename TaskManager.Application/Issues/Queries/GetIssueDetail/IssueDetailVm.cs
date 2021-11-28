using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskManager.Application.Common.Mappings;
using TaskManager.Application.Common.Models;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enumerations;

namespace TaskManager.Application.Issues.Queries.GetIssueDetail
{
    public class IssueDetailVm 
    {
        public Issue Issue { get; set; }
        public IList<FrameDto> Employees { get; set; }
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }


        public IList<FrameDto> IssueStatuses =
            Enum.GetValues(typeof(IssueStatus))
            .Cast<IssueStatus>()
            .Select(s => new FrameDto { Value = (int)s, Name = s.ToString() })
            .ToList();

        public IList<FrameDto> IssuePriorityLevels =
            Enum.GetValues(typeof(PriorityLevel))
            .Cast<PriorityLevel>()
            .Select(p => new FrameDto { Value = (int)p, Name = p.ToString() })
            .ToList();

    }
}