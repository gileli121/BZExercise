using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BzScanner.Extensions
{
    static class RequestExtensions
    {
        public static void TrowIfError(this IRestResponse response, HttpStatusCode successCode = HttpStatusCode.OK)
        {
            if (response.StatusCode != successCode)
                throw new Exception(
                    $"There was an error in the request, got {response.StatusCode} status code (expected for {successCode}). The full response was: {response.Content}");
        }
    }
}