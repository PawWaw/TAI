using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Backend.RestModel;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Backend.Model
{
    [Table("OrderStation")]
    public partial class OrderStation
    {
        private string password;

        public OrderStation()
        {
            Orders = new HashSet<Order>();
        }
        public void FillProperties(WsStation user)
        {
            this.Address = user.Address;
            this.password = user.Password;
            this.Username = user.Username;
        }
        [Key]
        public long Id { get; set; }
        [Column("resteurantId")]
        public long RestaurantId { get; set; }
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
        [JsonIgnore]
        [Column("password")]
        public string Password { get { return password; } set { password = GenerateHash(value); } }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("OrderStations")]
        public virtual City City { get; set; }
        [Column("OrderStations")]
        [ForeignKey(nameof(RestaurantId))]
        [InverseProperty(nameof(Model.Restaurant.OrderStation))]
        public virtual Restaurant Restaurant { get; set; }
        [InverseProperty(nameof(Order.OrderStation))]
        public virtual ICollection<Order> Orders { get; set; }

        internal void InsertHashedPassword(string password)
        {
            this.password = password;
        }
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
