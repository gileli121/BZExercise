using Newtonsoft.Json;

namespace BzScanner.VirusTotalModels
{
    public class Data
    {
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        // [JsonProperty("links")]
        // public Links Links { get; set; }
    }
}
