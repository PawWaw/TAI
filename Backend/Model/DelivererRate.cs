using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("DelivererRate")]
    public partial class DelivererRate
    {
        [Key]
        public long Id { get; set; }
        [Column("delivererId")]
        public long DelivererId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("value")]
        public double Value { get; set; }
        [Column("userId")]
        public long UserId { get; set; }

        [ForeignKey(nameof(DelivererId))]
        [InverseProperty("DelivererRates")]
        public virtual Deliverer Deliverer { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("DelivererRates")]
        public virtual User User { get; set; }
    }
}
