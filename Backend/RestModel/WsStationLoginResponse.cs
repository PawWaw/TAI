using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsStationLoginResponse
    {
        public string Token { get; internal set; }
        public string Username { get; internal set; }
        public string Address { get; internal set; }
        public string City { get; internal set; }
    }
}
