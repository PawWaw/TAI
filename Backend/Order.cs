using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class Order
    {
        public Order()
        {
            FoodOrders = new HashSet<FoodOrder>();
        }

        public long Id { get; set; }
        public long OrderStationId { get; set; }
        public long? DelivererId { get; set; }
        public long UserId { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Deliverer Deliverer { get; set; }
        public virtual OrderStation OrderStation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
    }
}
