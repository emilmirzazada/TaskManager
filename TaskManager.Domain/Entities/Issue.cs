using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Enumerations;

namespace TaskManager.Domain.Entities
{
    public class Issue
    {
        public long IssueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IssueStatus Status { get; set; }

        public PriorityLevel Priority { get; set; }
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public IEnumerable<IssueAssignee> IssueAssignees { get; private set; } = new HashSet<IssueAssignee>();

        public long? ReporterId { get; set; }

        public Employee Reporter { get; set; }
    }
}
