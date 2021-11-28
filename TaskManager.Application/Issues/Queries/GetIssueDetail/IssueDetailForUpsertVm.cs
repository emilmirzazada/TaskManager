using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Common.Models;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Issues.Queries.GetIssueDetail
{
    public class IssueDetailForUpsertVm:IssueDetailVm
    {
        public IList<FrameDto> Employees { get; set; }

    }
}
