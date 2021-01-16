﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class LoginRequest
    {
        private string password;
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get { return password; } set { password = GenerateHash(value); } }
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
