using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsDelivererLoginResponse : WsLoginResponse
    {
        public double DragonCoinBalance { get; set; }
    }
}
