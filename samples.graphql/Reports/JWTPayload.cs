namespace StarWars.Reports
{
    public class JWTPayload
    {
        public JWTPayload(long iat, string path, string apiKey)
        {
            this.iat = iat;
            this.path = path;
            this.apiKey = apiKey;
        }

        public long iat { get; set; }
        public string path { get; set; }
        public string apiKey { get; set; }

    }
}
