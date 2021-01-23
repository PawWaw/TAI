using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.RestModel.ForDeliverer
{
    public class WsExtendedDateWsOrderResponse : WsEndDateWsOrderResponse
    {
        private string status;

        public string StartDate { get; set; }
        public string Status { get { return status; } set { status = value.Trim(); } }
    }
}
