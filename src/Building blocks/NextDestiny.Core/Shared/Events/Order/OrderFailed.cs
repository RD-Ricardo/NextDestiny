using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDestiny.Core.Shared.Events.Order
{
    public class OrderFailed
    {
        public OrderFailed(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }

        public Guid OrderId { get; set; }
        public string Reason { get; set; }
    }
}
