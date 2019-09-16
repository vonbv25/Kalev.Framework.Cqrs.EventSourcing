using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.QueryDrivers
{
    public interface IRequestDispatcher
    {
        TResponse DispatchRequest<TRequest, TResponse>(TRequest request)
            where TResponse : class
            where TRequest : IRequest<TResponse>;
        Task<TResponse> DispatchRequestAsync<TRequest, TResponse>(TRequest request) 
            where TResponse : class 
            where TRequest : IRequest<TResponse>;
    }
    public class RequestDispatcher : IRequestDispatcher
    {
        private readonly IRequestHandlerFactory requestHandlerFactory;
        public RequestDispatcher(IRequestHandlerFactory requestHandlerFactory)
        {
            this.requestHandlerFactory = requestHandlerFactory;
        }

        public TResponse DispatchRequest<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : class
        {
            var requestHandler = requestHandlerFactory.Resolve<TRequest, TResponse>();

            return requestHandler.SendRequest(request);
        }

        public async Task<TResponse> DispatchRequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : class
        {
            var requestHandler = requestHandlerFactory.Resolve<TRequest, TResponse>();

            return await requestHandler.SendRequestAsync(request);
        }
    }
}
