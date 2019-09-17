using Kalev.Framework.Cqrs.EventSourcing.CommandDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.CommandHandlers
{
    public class UpdatePersonInfoCommandHandler : ICommandHandler<UpdatePersonInfoCommand>
    {
        public async Task HandleAsync(UpdatePersonInfoCommand command)
        {
            Assert.True(command != null, "Person info not updated");

            Debug.WriteLine($"{command.GetType().Name} Person info updated");
        }
    }
}
