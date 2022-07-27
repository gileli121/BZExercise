using Newtonsoft.Json;

namespace BzScanner.VirusTotalModels
{
    public class ScanResult
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
