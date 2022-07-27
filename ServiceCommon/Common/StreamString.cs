using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceCommon.Common
{

    // https://stackoverflow.com/questions/26489215/how-to-send-multiple-objects-using-named-pipes/72792279#72792279
    public class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            byte[] strSizeArr = new byte[sizeof(int)];
            ioStream.Read(strSizeArr, 0, sizeof(int));
            int strSize = BitConverter.ToInt32(strSizeArr, 0);
            byte[] inBuffer = new byte[strSize];
            ioStream.Read(inBuffer, 0, strSize);
            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            byte[] strSize = BitConverter.GetBytes(outBuffer.Length);
            ioStream.Write(strSize, 0, strSize.Length);
            ioStream.Write(outBuffer, 0, outBuffer.Length);
            ioStream.Flush();
            return outBuffer.Length + 2;
        }

        public void WriteSerialized(object objectValue)
        {
            WriteString(JsonSerializer.Serialize(objectValue));
        }
    }
}
