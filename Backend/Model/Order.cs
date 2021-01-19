using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            FoodOrders = new HashSet<FoodOrder>();
        }

        [Key]
        public long Id { get; set; }
        [Column("orderStationId")]
        public long OrderStationId { get; set; }
        [Column("delivererId")]
        public long? DelivererId { get; set; }
        [Column("userId")]
        public long UserId { get; set; }
        [Required]
        [Column("status")]
        [StringLength(100)]
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        [Column("endTime")]
        public DateTime? EndTime { get; set; }

        [ForeignKey(nameof(DelivererId))]
        [InverseProperty("Orders")]
        public virtual Deliverer Deliverer { get; set; }
        [ForeignKey(nameof(OrderStationId))]
        [InverseProperty("Orders")]
        public virtual OrderStation OrderStation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Orders")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(FoodOrder.Order))]
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
    }
}
