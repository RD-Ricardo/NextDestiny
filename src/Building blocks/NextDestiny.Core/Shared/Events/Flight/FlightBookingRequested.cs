using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightBookingRequested
    {
        public Guid OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
