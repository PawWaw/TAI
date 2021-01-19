using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsStation : WsPassword
    {
        private string address;
        private string username;
        private string city;
        [Required]
        [StringLength(100)]
        public string Address { get { return address; } set { address = value.Trim(); } }
        [Required]
        [StringLength(50)]
        public string Username { get { return username; } set { username = value.Trim(); } }

        [Required]
        public string City { get { return city; } set { city = value.Trim(); } }
    }
}
