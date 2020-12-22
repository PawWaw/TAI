using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("Ingredient")]
    public partial class Ingredient
    {
        public Ingredient()
        {
            FoodIngredients = new HashSet<FoodIngredient>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(FoodIngredient.Ingredient))]
        public virtual ICollection<FoodIngredient> FoodIngredients { get; set; }
    }
}
