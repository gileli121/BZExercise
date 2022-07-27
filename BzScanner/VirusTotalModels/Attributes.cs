using System.Collections.Generic;
using Newtonsoft.Json;

namespace BzScanner.VirusTotalModels
{
    public class Attributes
    {
        [JsonProperty("type_description")]
        public string TypeDescription { get; set; }

        [JsonProperty("tlsh")]
        public string Tlsh { get; set; }

        [JsonProperty("vhash")]
        public string Vhash { get; set; }

        // [JsonProperty("trid")]
        // public List<Trid> Trid { get; set; }

        [JsonProperty("creation_date")]
        public int CreationDate { get; set; }

        [JsonProperty("names")]
        public List<string> Names { get; set; }

        [JsonProperty("signature_info")]
        public SignatureInfo SignatureInfo { get; set; }

        [JsonProperty("last_modification_date")]
        public int LastModificationDate { get; set; }

        [JsonProperty("type_tag")]
        public string TypeTag { get; set; }

        [JsonProperty("times_submitted")]
        public int TimesSubmitted { get; set; }

        // [JsonProperty("total_votes")]
        // public TotalVotes TotalVotes { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("type_extension")]
        public string TypeExtension { get; set; }

        [JsonProperty("authentihash")]
        public string Authentihash { get; set; }

        [JsonProperty("last_submission_date")]
        public int LastSubmissionDate { get; set; }

        // [JsonProperty("sigma_analysis_results")]
        // public List<SigmaAnalysisResult> SigmaAnalysisResults { get; set; }

        [JsonProperty("meaningful_name")]
        public string MeaningfulName { get; set; }

        // [JsonProperty("sigma_analysis_summary")]
        // public SigmaAnalysisSummary SigmaAnalysisSummary { get; set; }
        //
        // [JsonProperty("sandbox_verdicts")]
        // public SandboxVerdicts SandboxVerdicts { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("last_analysis_date")]
        public int LastAnalysisDate { get; set; }

        [JsonProperty("unique_sources")]
        public int UniqueSources { get; set; }

        [JsonProperty("first_submission_date")]
        public int FirstSubmissionDate { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("ssdeep")]
        public string Ssdeep { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        // [JsonProperty("pe_info")]
        // public PeInfo PeInfo { get; set; }

        [JsonProperty("magic")]
        public string Magic { get; set; }

        [JsonProperty("last_analysis_stats")]
        public LastAnalysisStats LastAnalysisStats { get; set; }

        // [JsonProperty("last_analysis_results")]
        // public LastAnalysisResults LastAnalysisResults { get; set; }

        [JsonProperty("reputation")]
        public int Reputation { get; set; }

        [JsonProperty("first_seen_itw_date")]
        public int FirstSeenItwDate { get; set; }

        // [JsonProperty("sigma_analysis_stats")]
        // public SigmaAnalysisStats SigmaAnalysisStats { get; set; }
    }

}
