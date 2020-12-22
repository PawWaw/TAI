using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("Deliverer")]
    public partial class Deliverer
    {
        public Deliverer()
        {
            DelivererRates = new HashSet<DelivererRate>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public long Id { get; set; }
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
        [Required]
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [Column("firstName")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Column("lastName")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Column("dragonCoinBalance")]
        public double DragonCoinBalance { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Deliverers")]
        public virtual City City { get; set; }
        [InverseProperty(nameof(DelivererRate.Deliverer))]
        public virtual ICollection<DelivererRate> DelivererRates { get; set; }
        [InverseProperty(nameof(Order.Deliverer))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
