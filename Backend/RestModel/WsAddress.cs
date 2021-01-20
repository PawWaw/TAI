using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsAddress
    {
        private string address;
        private string city;

        public string Address { get { return address; }set{ address = value.Trim(); } }
        public string City { get { return city; } set { city = value.Trim(); } }
    }
}
