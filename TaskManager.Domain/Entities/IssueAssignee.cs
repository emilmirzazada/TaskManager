using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class IssueAssignee
    {
        public long Id { get; set; }
        public long? IssueId { get; set; }
        public long? AssigneeId { get; set; }
        public Issue Issue { get; set; }
        public Employee Assignee { get; set; }
    }
}
