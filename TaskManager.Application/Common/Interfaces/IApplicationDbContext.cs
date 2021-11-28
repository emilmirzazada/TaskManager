using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; set; }

        DbSet<Issue> Issues { get; set; }
        public DbSet<IssueAssignee> IssueAssignees { get; set; }
        DbSet<Organization> Organizations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
