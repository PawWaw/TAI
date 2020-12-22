using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("Food")]
    public partial class Food
    {
        public Food()
        {
            FoodIngredients = new HashSet<FoodIngredient>();
            FoodOrders = new HashSet<FoodOrder>();
            FoodRates = new HashSet<FoodRate>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("price")]
        public double Price { get; set; }

        [InverseProperty(nameof(FoodIngredient.Food))]
        public virtual ICollection<FoodIngredient> FoodIngredients { get; set; }
        [InverseProperty(nameof(FoodOrder.Food))]
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
        [InverseProperty(nameof(FoodRate.Food))]
        public virtual ICollection<FoodRate> FoodRates { get; set; }
    }
}
