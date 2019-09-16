using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.QueryDrivers
{
    public interface IRequestHandlerFactory
    {
        void Register<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> requestHandler) 
            where TResponse : class 
            where TRequest : IRequest<TResponse>;
        
        IRequestHandler<TRequest, TResponse> Resolve<TRequest, TResponse>() where TResponse : class
            where TRequest : IRequest<TResponse>;

        IRequestDispatcher BuildRequestDispatcher();

    }
    public class RequestHandlerFactory : IRequestHandlerFactory
    {
        private Dictionary<Type, IRequestHandlerBase> requestHandlers;
        public RequestHandlerFactory()
        {
            requestHandlers = new Dictionary<Type, IRequestHandlerBase>();
        }
        public IRequestDispatcher BuildRequestDispatcher()
        {
            return new RequestDispatcher(this);
        }

        public void Register<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> requestHandler)
            where TRequest : IRequest<TResponse>
            where TResponse : class
        {
            var key = typeof(TRequest);

            if(requestHandlers.ContainsKey(key))
            {
                requestHandlers[key] = requestHandler;
            }
            else
            {
                requestHandlers.Add(key, requestHandler);
            }

        }

        public IRequestHandler<TRequest, TResponse> Resolve<TRequest, TResponse>()
            where TRequest : IRequest<TResponse>
            where TResponse : class
        {

            var key = typeof(TRequest);

            if(requestHandlers.Count == 0 || !requestHandlers.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Command Handler for this command : {key.Name} is not registered");
            }

            return requestHandlers[key] as IRequestHandler<TRequest, TResponse>;
        }
    }
}
