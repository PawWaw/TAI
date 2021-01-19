using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsStation : WsPassword
    {
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string City { get; set; }
    }
}
