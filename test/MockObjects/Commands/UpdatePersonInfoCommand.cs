using Kalev.Framework.Cqrs.EventSourcing.CommandDrivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Test.MockObjects.Commands
{
    public class UpdatePersonInfoCommand : ICommand
    {
        public Guid Guid => Guid.NewGuid();
    }
}
