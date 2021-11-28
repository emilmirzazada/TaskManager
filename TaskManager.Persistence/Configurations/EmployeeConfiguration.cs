using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(t => t.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.OrganizationId);
        }
    }
}
