using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enumerations;
using TaskManager.Persistence;

namespace TaskManager.UnitTests.Application.Common
{
    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var operationalStoreOptions = Options.Create(
                new OperationalStoreOptions
                {
                    DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
                    PersistedGrants = new TableConfiguration("PersistedGrants")
                });

            var dateTimeMock = new Mock<IDateTimeService>();
            dateTimeMock.Setup(m => m.Now)
                    .Returns(new DateTime(3001, 1, 1));

            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(m => m.UserId)
                    .Returns("00000000-0000-0000-0000-000000000000");

            var context = new ApplicationDbContext(
                options, operationalStoreOptions,
                currentUserServiceMock.Object, dateTimeMock.Object);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(ApplicationDbContext context)
        {
            context.Organizations.AddRange(
                new Organization { Id = 1, Name = "Organization_1", Phonenumber = "0511111111", Address = "Baku" },
                new Organization { Id = 2, Name = "Organization_2", Phonenumber = "0511111112", Address = "London" },
                new Organization { Id = 3, Name = "Organization_3", Phonenumber = "0511111113", Address = "Barcelona" }
            );

            

            context.Employees.AddRange(
                new Employee { EmployeeId = 1, FullName = "FIO_1", ShortName = "sn1", AppUserId = Guid.NewGuid().ToString(), OrganizationId = 1 },
                new Employee { EmployeeId = 2, FullName = "FIO_2", ShortName = "sn2", AppUserId = Guid.NewGuid().ToString(), OrganizationId = 2 },
                new Employee { EmployeeId = 3, FullName = "FIO_3", ShortName = "sn3", AppUserId = Guid.NewGuid().ToString(), OrganizationId = 3 }
            );

            context.Issues.AddRange(
                new Issue { IssueId = 1, Name = "Name_1", Description = "Description_1", ReporterId = 1, Status = IssueStatus.New, OrganizationId = 1 },
                new Issue { IssueId = 2, Name = "Name_2", Description = "Description_2", ReporterId = 1, Status = IssueStatus.New, OrganizationId = 3 },
                new Issue { IssueId = 3, Name = "Name_3", Description = "Description_3", ReporterId = 2, Status = IssueStatus.New, OrganizationId = 2 },
                new Issue { IssueId = 4, Name = "Name_4", Description = "Description_4", ReporterId = 2, Status = IssueStatus.New, OrganizationId = 1 },
                new Issue { IssueId = 5, Name = "Name_5", Description = "Description_5", ReporterId = 3, Status = IssueStatus.New, OrganizationId = 2 }
            );

            context.IssueAssignees.AddRange(
                new IssueAssignee { IssueId = 1, AssigneeId = 2 },
                new IssueAssignee { IssueId = 1, AssigneeId = 3 },
                new IssueAssignee { IssueId = 2, AssigneeId = 3 },
                new IssueAssignee { IssueId = 4, AssigneeId = 2 }
            );

            context.SaveChanges();
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
