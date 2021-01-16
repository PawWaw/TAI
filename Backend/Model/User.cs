using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Backend.RestModel;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("User")]
    public partial class User
    {
        [NotMapped]
        private string password;
        public User()
        {
            FoodRates = new HashSet<FoodRate>();
            Orders = new HashSet<Order>();
        }
        public void fillProperties(WsUser user)
        {
            this.Address = user.Address;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.password = user.Password;
            this.Username = user.Username;
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
        public string Password { get { return password; } set { password = GenerateHash(value); } }
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

        public String GenerateHash(String Input)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Input);
            System.Security.Cryptography.SHA256Managed hashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = hashString.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }
        private String ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);

            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }
    }
}
