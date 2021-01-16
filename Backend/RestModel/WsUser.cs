using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsUser
    {
        private string password;

        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
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
