using Newtonsoft.Json;

namespace Backend.RestModel
{
    public class WsStatistics
    {
        [JsonProperty(PropertyName = "Current orders")]
        public int CurrentOrders { get; set; }
        [JsonProperty(PropertyName = "Client rate")]
        public double ClientRate { get; set; }
        [JsonProperty(PropertyName = "Total delivery")]
        public int TotalDelivery { get; set; }
        [JsonProperty(PropertyName = "Max Daily Orders")]
        public int MaxDailyOrders { get; set; }
    }
}
