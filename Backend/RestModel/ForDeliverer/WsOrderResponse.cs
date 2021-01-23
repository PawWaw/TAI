using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel.ForDeliverer
{
    public class WsOrderResponse
    {
        public long Id { get; set; }
        [JsonProperty("restaurant")]
        public WsAddress RestaurantAddress { get; set; }
        [JsonProperty("client")]
        public WsAddress ClientAddress { get; set; }
    }
}
