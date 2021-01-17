using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class OrderStation
    {
        public OrderStation()
        {
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public long ResteurantId { get; set; }
        public long CityId { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual City City { get; set; }
        public virtual Restaurant Resteurant { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
