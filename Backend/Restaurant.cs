using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            OrderStations = new HashSet<OrderStation>();
        }

        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string Name { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual ICollection<OrderStation> OrderStations { get; set; }
    }
}
