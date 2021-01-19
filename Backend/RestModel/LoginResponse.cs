using System.ComponentModel.DataAnnotations;

namespace Backend.RestModel
{
    public class LoginResponse
    {
        private string adress;
        private string username;
        private string email;
        private string firstName;
        private string lastName;
        private string city;
        public string Token { get; set; }
        [StringLength(100)]
        public string Address { get { return adress; } set { adress = value.Trim(); } }
        [StringLength(50)]
        public string Username { get { return username; } set { username = value.Trim(); } }
        [StringLength(50)]
        public string Email { get { return email; } set { email = value.Trim(); } }
        [StringLength(50)]
        public string FirstName { get { return firstName; } set { firstName = value.Trim(); } }
        [StringLength(50)]
        public string LastName { get { return lastName; } set { lastName = value.Trim(); } }
        public string City { get { return city; } set { city = value.Trim(); } }
    }
}
