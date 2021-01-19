using Backend.Model;
using System.ComponentModel.DataAnnotations;

namespace Backend.RestModel
{
    public class WsPutUser
    {
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public string City { get; set; }
        public void FIllPutUser(User user)
        {
            if (Address != "" && Address != null)
            {
                user.Address = Address;
            }
            if (Username != "" && Username != null)
            {
                user.Username = Username;
            }
            if (Email != "" && Email != null)
            {
                user.Email = Email;
            }
            if (FirstName != "" && FirstName != null)
            {
                user.FirstName = FirstName;
            }
            if (LastName != "" && LastName != null)
            {
                user.LastName = LastName;
            }
            if (City != "" && City != null)
            {
                user.City.Name = City;
            }
        }
        public void FIllPutUser(Deliverer user)
        {
            if (Address != "" && Address != null)
            {
                user.Address = Address;
            }
            if (Username != "" && Username != null)
            {
                user.Username = Username;
            }
            if (Email != "" && Email != null)
            {
                user.Email = Email;
            }
            if (FirstName != "" && FirstName != null)
            {
                user.FirstName = FirstName;
            }
            if (LastName != "" && LastName != null)
            {
                user.LastName = LastName;
            }
            if (City != "" && City != null)
            {
                user.City.Name = City;
            }
        }
        public void FIllPutUser(Owner user)
        {
            if (Address != "" && Address != null)
            {
                user.Address = Address;
            }
            if (Username != "" && Username != null)
            {
                user.Username = Username;
            }
            if (Email != "" && Email != null)
            {
                user.Email = Email;
            }
            if (FirstName != "" && FirstName != null)
            {
                user.FirstName = FirstName;
            }
            if (LastName != "" && LastName != null)
            {
                user.LastName = LastName;
            }
            if (City != "" && City != null)
            {
                user.City.Name = City;
            }
        }
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
