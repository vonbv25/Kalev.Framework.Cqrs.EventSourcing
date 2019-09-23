using Kalev.Framework.Cqrs.EventSourcing.DataTypes;
using Kalev.Framework.Cqrs.EventSourcing.RequestDrivers;
using Kalev.Framework.Cqrs.EventSourcing.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.Pipelines
{
    public delegate TResponse RequestHandlerDelegate<TResponse>();

    public interface IPipelineHandlerBehavior <TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
}
