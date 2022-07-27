using System;
using System.Net;
using BzScanner.Enums;
using BzScanner.Extensions;
using BzScanner.VirusTotalModels;
using RestSharp;


namespace BzScanner
{
    public class BZScanner
    {
        readonly RestClient client;

        public BZScanner(string token)
        {
            client = new RestClient("https://www.virustotal.com");
            client.AddDefaultHeader("x-apikey", token);
        }

        bool IsAnalysisDateTooOld(ScanResult scanResult)
        {
            var lastScanTime = DateTimeOffset.FromUnixTimeSeconds(scanResult.Data.Attributes.LastAnalysisDate).DateTime;
            var daysPassed = (DateTime.Now - lastScanTime).TotalDays;
            return daysPassed >= 30;
        }


        bool IsInvalidAnalysisData(ScanResult scanResult)
        {
            var st = scanResult.Data.Attributes.LastAnalysisStats;
            return st.Malicious + st.ConfirmedTimeout + st.Failure + st.Harmless + st.Suspicious + st.Timeout +
                st.TypeUnsupported + st.Undetected == 0;
        }

        bool IsMaliciousResult(ScanResult scanResult)
        {
            if (scanResult.Data.Attributes.Reputation < 0)
                return true;

            var st = scanResult.Data.Attributes.LastAnalysisStats;
            var total = st.Malicious + st.ConfirmedTimeout + st.Failure + st.Harmless + st.Suspicious + st.Timeout +
                        st.TypeUnsupported + st.Undetected;

            if (total == 0)
                return true;


            var badStuff = st.Malicious + st.Failure + st.Suspicious + st.Timeout + st.ConfirmedTimeout +
                           st.TypeUnsupported;

            if (badStuff / (double)total > 0.3)
                return true;

            return false;
        }

        public ScanResultType CheckFile(string fileHash)
        {
            // Send request to VirusTotal
            var request = new RestRequest(Method.GET);
            request.Resource = $"/api/v3/files/{fileHash}";
            var response = client.Execute<ScanResult>(request);

            // Return unknown 404 status because this is a classic case of unknown file 
            if (response.StatusCode == HttpStatusCode.NotFound)
                return ScanResultType.Unknown;

            // For any other unexpected status, throw error here
            response.TrowIfError();

            // Load the scan result
            var scanResult = response.Data;

            // Decide the result and return it
            if (IsAnalysisDateTooOld(scanResult))
                return ScanResultType.Unknown;

            if (IsInvalidAnalysisData(scanResult))
                return ScanResultType.Unknown;

            if (IsMaliciousResult(scanResult))
                return ScanResultType.Malicious;

            return ScanResultType.Legitimate;
        }
    }
}