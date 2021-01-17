using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class User
    {
        public User()
        {
            FoodRates = new HashSet<FoodRate>();
            FoodUsers = new HashSet<FoodUser>();
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

        public virtual City City { get; set; }
        public virtual ICollection<FoodRate> FoodRates { get; set; }
        public virtual ICollection<FoodUser> FoodUsers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
