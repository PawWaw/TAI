using Backend.RestModel;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class LoginRequest : WsPassword
    {
        [Required]
        public string Username { get; set; }
    }
}
