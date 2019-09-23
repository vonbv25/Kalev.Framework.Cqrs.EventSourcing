using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.RequestDrivers
{
    public interface IRequest<out TResponse>
    {
        Guid Guid { get; }
    }
}
