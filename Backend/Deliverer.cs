using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class Deliverer
    {
        public Deliverer()
        {
            DelivererRates = new HashSet<DelivererRate>();
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public long CityId { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double DragonCoinBalance { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<DelivererRate> DelivererRates { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
