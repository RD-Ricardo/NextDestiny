using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightBookingRequested
    {
        public FlightBookingRequested(Guid orderId)
        {
            OrderId = orderId;
        }
        public Guid OrderId { get; }

        public DateTime Timestamp { get; set; }
    }
}
