using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookAddIn1
{
    public static class Extensions
    {

        public static string ToMd5(this byte[] inputData)
        {
            //convert byte array to stream
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            stream.Write(inputData, 0, inputData.Length);

            //important: get back to start of stream
            stream.Seek(0, System.IO.SeekOrigin.Begin);

            //get a string value for the MD5 hash.
            using (var md5Instance = System.Security.Cryptography.MD5.Create())
            {
                var hashResult = md5Instance.ComputeHash(stream);

                //***I did some formatting here, you may not want to remove the dashes, or use lower case depending on your application
                return BitConverter.ToString(hashResult).Replace("-", "").ToLowerInvariant();
            }
        }

    }
}
