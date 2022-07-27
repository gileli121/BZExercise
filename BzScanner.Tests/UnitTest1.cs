using System;
using BzScanner.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BzScanner.Tests
{
    [TestClass]
    public class UnitTest1
    {
        BZScanner scanner;

        [TestInitialize]
        public void Init()
        {
            var apiKey = Environment.GetEnvironmentVariable("VirusTotalAPI");
            scanner = new BZScanner(apiKey);
        }

        [TestMethod]
        public void TestUnknownFile()
        {
            var scanResult = scanner.CheckFile("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Assert.AreEqual(ScanResultType.Unknown, scanResult);
        }


        [TestMethod]
        public void TestGoodFile()
        {
            var scanResult = scanner.CheckFile("6bddc0ffc4cc38dce444144e2b697eb2");
            Assert.AreEqual(ScanResultType.Legitimate, scanResult);
        }


        [TestMethod]
        public void TestBadFile()
        {
            var scanResult = scanner.CheckFile("a196c6b8ffcb97ffb276d04f354696e2391311db3841ae16c8c9f56f36a38e92");
            Assert.AreEqual(ScanResultType.Malicious,scanResult);
        }


    }
}
