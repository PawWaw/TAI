﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel
{
    public class WsOwnerLoginResponse : WsLoginResponse
    {
        private string restaurantName;
        [JsonProperty("restaurant")]
        public string RestaurantName { get { return restaurantName; } set { restaurantName = value.Trim(); } }
    }
}
