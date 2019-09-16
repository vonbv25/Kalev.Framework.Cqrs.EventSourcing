using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    public class NoEventHandlersForThisEventException : Exception
    {
        public NoEventHandlersForThisEventException() : base() { }
        public NoEventHandlersForThisEventException(string message) : base(message) { }
        public NoEventHandlersForThisEventException(string message, System.Exception inner) : base(message, inner) { }

    }
}
