using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Events
{
    public class ProductCreatedRejected : IRejectedEvent
    {
        public string Reason { get; set; }
        public string Code { get; set; }
        public ICorrelationContext Context { get; set; }
    }
}
