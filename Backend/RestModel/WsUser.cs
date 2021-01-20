using System.ComponentModel.DataAnnotations;

namespace Backend.RestModel
{
    public class WsUser : WsStation
    {
        private string email;
        private string firstName;
        private string lastName;

        [Required]
        [StringLength(50)]
        public string Email { get { return email; } set {email = value.Trim(); } }
        [Required]
        [StringLength(50)]
        public string FirstName { get { return firstName; } set { firstName = value.Trim(); } }
        [Required]
        [StringLength(50)]
        public string LastName { get { return lastName; } set { lastName = value.Trim(); } }
    }
}
