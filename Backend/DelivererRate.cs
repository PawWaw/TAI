using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class DelivererRate
    {
        public long Id { get; set; }
        public long DelivererId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public long UserId { get; set; }

        public virtual Deliverer Deliverer { get; set; }
    }
}
