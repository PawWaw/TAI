using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("Restaurant")]
    public partial class Restaurant
    {
        public Restaurant()
        {
            Foods = new HashSet<Food>();
            OrderStation = new OrderStation();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("ownerId")]
        public long OwnerId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey(nameof(OwnerId))]
        [InverseProperty("Restaurant")]
        public virtual Owner Owner { get; set; }
        [InverseProperty(nameof(Model.OrderStation.Restaurant))]
        public virtual OrderStation OrderStation { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
