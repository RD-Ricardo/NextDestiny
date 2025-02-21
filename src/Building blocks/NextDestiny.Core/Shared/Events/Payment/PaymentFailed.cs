using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDestiny.Core.Shared.Events.Payment
{
    public class PaymentFailed
    {
        public Guid OrderId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
