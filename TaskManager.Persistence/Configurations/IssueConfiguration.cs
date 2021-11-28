using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entities;
using System.Threading.Tasks;
namespace TaskManager.Persistence.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Property(t => t.Name)
               .HasMaxLength(64)
               .IsRequired();
            builder.Property(t => t.Description)
               .HasMaxLength(256);
            builder.HasOne(b => b.Reporter)
                .WithMany(d => d.ReporteredIssues)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.OrganizationId);
        }
    }
}
