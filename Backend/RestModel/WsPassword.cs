using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.RestModel
{
    public class WsPassword
    {
        private string password;
        [Required]
        public string Password { get {return password; } set {password = GenerateHash(value); } }
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
