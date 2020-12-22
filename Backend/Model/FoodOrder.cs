using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("FoodOrder")]
    public partial class FoodOrder
    {
        [Key]
        public long Id { get; set; }
        [Column("foodId")]
        public long FoodId { get; set; }
        [Column("orderId")]
        public long OrderId { get; set; }

        [ForeignKey(nameof(FoodId))]
        [InverseProperty("FoodOrders")]
        public virtual Food Food { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty("FoodOrders")]
        public virtual Order Order { get; set; }
    }
}
