using System;
using System.Collections.Generic;
using System.Text;

namespace Kalev.Framework.Cqrs.EventSourcing.Utilities
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DataPropertyAttribute : Attribute
    {
    }
}
