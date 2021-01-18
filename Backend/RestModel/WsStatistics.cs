using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsStatistics
    {
        [JsonProperty(PropertyName = "Current orders")]
        public int CurrentOrders { get; set; }
        [JsonProperty(PropertyName = "Client rate")]
        public double ClientRate { get; set; }
        [JsonProperty(PropertyName = "Total delivary")]
        public int TotalDelivery { get; set; }
        [JsonProperty(PropertyName = "Max Daily Orders")]
        public int MaxDailyOrders { get; set; }
    }
}
