using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.QueryDrivers
{
    public interface IRequestHandlerBase
    {

    }
    public interface IRequestHandler<TRequest, TResponse> : IRequestHandlerBase 
        where TResponse : class 
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> SendRequestAsync(TRequest request);
        TResponse SendRequest(TRequest request);
    }
}
