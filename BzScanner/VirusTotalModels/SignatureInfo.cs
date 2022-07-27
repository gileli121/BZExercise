using Newtonsoft.Json;

namespace BzScanner.VirusTotalModels
{
    public class SignatureInfo
    {
        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("original name")]
        public string OriginalName { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("file version")]
        public string FileVersion { get; set; }
    }


}
