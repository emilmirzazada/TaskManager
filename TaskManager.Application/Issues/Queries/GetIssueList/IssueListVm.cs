using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Issues.Queries.GetIssueList
{
    public class IssueListVm
    {
        public IList<Issue> Issues { get; set; }
    }
}
