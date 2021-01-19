﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsUser : WsPassword
    {
        private string adress;
        private string username;
        private string email;
        private string firstName;
        private string lastName;
        private string city;

        [Required]
        [StringLength(100)]
        public string Address { get {return adress; } set {adress = value.Trim(); } }
        [Required]
        [StringLength(50)]
        public string Username { get {return username; } set {username = value.Trim(); } }
        [Required]
        [StringLength(50)]
        public string Email { get { return email; } set {email = value.Trim(); } }
        [Required]
        [StringLength(50)]
        public string FirstName { get { return firstName; } set { firstName = value.Trim(); } }
        [Required]
        [StringLength(50)]
        public string LastName { get { return lastName; } set { lastName = value.Trim(); } }
        [Required]
        public string City { get { return city; } set {city = value.Trim(); } }
    }
}
