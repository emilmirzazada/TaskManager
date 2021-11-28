﻿using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Employees.Commands.UpdateEmployee;
using TaskManager.Domain.Entities;
using TaskManager.UnitTests.Application.Common;
using Xunit;

namespace TaskManager.UnitTests.Application.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandTests : CommandTestBase
    {
        [Theory]
        [InlineData(1, "1111", "1")]
        [InlineData(2, "2222", "22")]
        [InlineData(3, "", null)]
        [InlineData(3, null, null)]
        public async Task Handle_GivenValidId_ShouldUpdatePersistedEmployee(long id, string fullname, string shortname)
        {
            var command = new UpdateEmployeeCommand
            {
                Id = id,
                FullName = fullname,
                ShortName = shortname
            };

            var handler = new UpdateEmployeeCommand.UpdateEmployeeCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Employees.Find(command.Id);

            entity.ShouldNotBeNull();
            entity.FullName.ShouldBe(command.FullName);
            entity.ShortName.ShouldBe(command.ShortName);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            var command = new UpdateEmployeeCommand
            {
                Id = 99,
                FullName = "ModifiedFullName"
            };

            var sut = new UpdateEmployeeCommand.UpdateEmployeeCommandHandler(Context);

            var exception = await Should.ThrowAsync<NotFoundException>(() =>
                sut.Handle(command, CancellationToken.None));

            Assert.Equal($"Entity \"{nameof(Employee)}\" ({command.Id}) was not found.", exception.Message);
        }
    }
}
