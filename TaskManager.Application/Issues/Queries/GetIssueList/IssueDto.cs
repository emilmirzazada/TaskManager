using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Common.Mappings;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Issues.Queries.GetIssueList
{
    public class IssueDto : IMapFrom<Issue>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<IssueAssignee> IssueAssignees { get; private set; } = new HashSet<IssueAssignee>();

        public long? ReporterId { get; set; }

        public string ReporterName { get; set; }

        public string StatusName { get; set; }

        public string PriorityName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueDto>()
                .ForMember(dto => dto.Id, exp => exp.MapFrom(i => i.IssueId))
                .ForMember(dto => dto.StatusName, exp => exp.MapFrom(i => i.Status.ToString()))
                .ForMember(dto => dto.PriorityName, exp => exp.MapFrom(i => i.Priority.ToString()))
                .ForMember(dto => dto.ReporterName, exp => exp.MapFrom(i => i.Reporter.FullName))
                .ForMember(dto => dto.IssueAssignees, exp => exp.MapFrom(i => i.IssueAssignees));
        }
    }
}
