using Newtonsoft.Json;

namespace BzScanner.VirusTotalModels
{
    public class LastAnalysisStats
    {
        [JsonProperty("harmless")]
        public int Harmless { get; set; }

        [JsonProperty("type-unsupported")]
        public int TypeUnsupported { get; set; }

        [JsonProperty("suspicious")]
        public int Suspicious { get; set; }

        [JsonProperty("confirmed-timeout")]
        public int ConfirmedTimeout { get; set; }

        [JsonProperty("timeout")]
        public int Timeout { get; set; }

        [JsonProperty("failure")]
        public int Failure { get; set; }

        [JsonProperty("malicious")]
        public int Malicious { get; set; }

        [JsonProperty("undetected")]
        public int Undetected { get; set; }
    }
}
