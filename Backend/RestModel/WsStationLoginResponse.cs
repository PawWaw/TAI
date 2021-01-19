namespace Backend.RestModel
{
    public class WsStationLoginResponse
    {
        private string username;
        private string address;
        private string city;
        public string Token { get; internal set; }
        public string Username { get { return username; } internal set { username = value.Trim(); } }
        public string Address { get { return address; } internal set { address = value.Trim(); } }
        public string City { get { return city; } internal set { city = value.Trim(); } }
    }
}
