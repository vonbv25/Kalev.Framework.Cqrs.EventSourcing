using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kalev.Framework.DomainDriven.SeedWork.Domain
{
    public delegate Task ExternalEventHandlers(EventStream eventStream);
}
