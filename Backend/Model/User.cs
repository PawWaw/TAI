using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            FoodRates = new HashSet<FoodRate>();
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

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Users")]
        public virtual City City { get; set; }
        [InverseProperty(nameof(FoodRate.User))]
        public virtual ICollection<FoodRate> FoodRates { get; set; }
        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
