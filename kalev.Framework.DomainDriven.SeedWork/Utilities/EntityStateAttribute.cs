using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EntityStateAttribute : Attribute
    {

    }
}
