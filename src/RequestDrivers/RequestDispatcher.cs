using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.Cqrs.EventSourcing.RequestDrivers
{
    public interface IRequestDispatcher
    {
        Task<TResponse> Handle<TRequest, TResponse>(TRequest request) 
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

        public async Task<TResponse> Handle<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse : class
        {
            var requestHandler = requestHandlerFactory.Resolve<TRequest, TResponse>();

            return await requestHandler.Handle(request);
        }
    }
}
