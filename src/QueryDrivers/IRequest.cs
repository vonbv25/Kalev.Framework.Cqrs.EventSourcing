using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.QueryDrivers
{
    public interface IRequest<out TResponse> where TResponse : class
    {
        Guid Guid { get; }
    }
}
