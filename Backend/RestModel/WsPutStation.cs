using Backend.Model;
using System.ComponentModel.DataAnnotations;

namespace Backend.RestModel
{
    public class WsPutStation
    {
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        public string City { get; set; }
        public void FIllPutUser(OrderStation user)
        {
            if (Address != "" && Address != null)
            {
                user.Address = Address;
            }
            if (Username != "" && Username != null)
            {
                user.Username = Username;
            }
            if (City != "" && City != null)
            {
                user.City.Name = City;
            }
        }
    }
}
