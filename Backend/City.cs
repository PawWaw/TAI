using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class City
    {
        public City()
        {
            Deliverers = new HashSet<Deliverer>();
            OrderStations = new HashSet<OrderStation>();
            Owners = new HashSet<Owner>();
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Deliverer> Deliverers { get; set; }
        public virtual ICollection<OrderStation> OrderStations { get; set; }
        public virtual ICollection<Owner> Owners { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
