using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelBookingCompleted
    {
        public Guid OrderId { get; set; }

        public HotelBookingCompleted(Guid orderId)
        {
            OrderId = orderId;
        }
        public DateTime Timestamp { get; set; }
    }
}
