namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    public class PublisherException : System.Exception
    {
        public PublisherException() { }
        public PublisherException(string message) : base(message) { }
        public PublisherException(string message, System.Exception inner) : base(message, inner) { }
    }
}