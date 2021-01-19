using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsPutPassword
    {
        [JsonProperty(PropertyName = "oldPassword")]
        public WsPassword OldPassword { get; set; }
        [JsonProperty(PropertyName = "newPassword")]
        public WsPassword NewPassword { get; set; }
    }
}
