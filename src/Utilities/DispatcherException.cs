namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    public class DispatcherException : System.Exception
    {
        public DispatcherException() { }
        public DispatcherException(string message) : base(message) { }
        public DispatcherException(string message, System.Exception inner) : base(message, inner) { }
    }
}