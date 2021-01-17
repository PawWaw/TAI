using System;
using System.Collections.Generic;

#nullable disable

namespace Backend
{
    public partial class FoodIngredient
    {
        public long Id { get; set; }
        public long FoodId { get; set; }
        public long IngredientId { get; set; }

        public virtual Food Food { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
