using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("FoodIngredient")]
    public partial class FoodIngredient
    {
        [Key]
        public long Id { get; set; }
        [Column("foodId")]
        public long FoodId { get; set; }
        [Column("ingredientId")]
        public long IngredientId { get; set; }

        [ForeignKey(nameof(FoodId))]
        [InverseProperty("FoodIngredients")]
        public virtual Food Food { get; set; }
        [ForeignKey(nameof(IngredientId))]
        [InverseProperty("FoodIngredients")]
        public virtual Ingredient Ingredient { get; set; }
    }
}
