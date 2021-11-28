using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Employees.Commands.DeleteEmployee;
using TaskManager.UnitTests.Application.Common;
using Xunit;

namespace TaskManager.UnitTests.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldRemovePersistedEmployee()
        {
            var command = new DeleteEmployeeCommand
            {
                Id = 1,
            };

            var handler = new DeleteEmployeeCommand.DeleteEmployeeCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Employees.Find(command.Id);

            entity.ShouldBeNull();
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteEmployeeCommand
            {
                Id = 99
            };

            var handler = new DeleteEmployeeCommand.DeleteEmployeeCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
