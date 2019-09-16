using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.EventDrivers
{
    public interface IEvent
    {
        Guid Guid { get; }
    }
}
