using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("OrderStation")]
    public partial class OrderStation
    {
        public OrderStation()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public long Id { get; set; }
        [Column("resteurantId")]
        public long ResteurantId { get; set; }
        [Column("cityId")]
        public long CityId { get; set; }
        [Required]
        [Column("address")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("OrderStations")]
        public virtual City City { get; set; }
        [ForeignKey(nameof(ResteurantId))]
        [InverseProperty(nameof(Restaurant.OrderStations))]
        public virtual Restaurant Resteurant { get; set; }
        [InverseProperty(nameof(Order.OrderStation))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
