using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("City")]
    public partial class City
    {
        public City()
        {
            Deliverers = new HashSet<Deliverer>();
            OrderStations = new HashSet<OrderStation>();
            Owners = new HashSet<Owner>();
            Users = new HashSet<User>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(Deliverer.City))]
        public virtual ICollection<Deliverer> Deliverers { get; set; }
        [InverseProperty(nameof(OrderStation.City))]
        public virtual ICollection<OrderStation> OrderStations { get; set; }
        [InverseProperty(nameof(Owner.City))]
        public virtual ICollection<Owner> Owners { get; set; }
        [InverseProperty(nameof(User.City))]
        public virtual ICollection<User> Users { get; set; }
    }
}
