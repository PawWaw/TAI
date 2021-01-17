using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            FoodIngredients = new HashSet<FoodIngredient>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FoodIngredient> FoodIngredients { get; set; }
    }
}
