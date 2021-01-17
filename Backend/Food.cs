using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class Food
    {
        public Food()
        {
            FoodIngredients = new HashSet<FoodIngredient>();
            FoodOrders = new HashSet<FoodOrder>();
            FoodRates = new HashSet<FoodRate>();
            FoodUsers = new HashSet<FoodUser>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<FoodIngredient> FoodIngredients { get; set; }
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
        public virtual ICollection<FoodRate> FoodRates { get; set; }
        public virtual ICollection<FoodUser> FoodUsers { get; set; }
    }
}
