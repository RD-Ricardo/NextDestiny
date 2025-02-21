using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelBookingRequested
    {
        public Guid OrderId { get; }
        public HotelBookingRequested(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
