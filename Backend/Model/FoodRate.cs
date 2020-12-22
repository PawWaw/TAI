using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("FoodRate")]
    public partial class FoodRate
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("userId")]
        public long UserId { get; set; }
        [Column("foodId")]
        public long FoodId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("value")]
        public double Value { get; set; }

        [ForeignKey(nameof(FoodId))]
        [InverseProperty("FoodRates")]
        public virtual Food Food { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("FoodRates")]
        public virtual User User { get; set; }
    }
}
