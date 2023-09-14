using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Downloaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader;

namespace ZtmDataTester
{
    [TestFixture]
    public class MpkCzestochowaDownloaderTest
    {

        //  METHODS

        #region SETUP METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Ztm data downloader test setup. </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            //
        }

        #endregion SETUP METHODS

        #region TEST METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Lines download and serialization test. </summary>
        [Test]
        public void LinesDownloadTest()
        {
            var downloader = new LinesDownloader();
            var request = new LinesRequestModel();

            var response = downloader.DownloadData(request);

            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(response.HasData);
            Assert.IsTrue(response.HasMessages);

            bool anyAttributeLoaded = false;

            foreach (var transportType in response.Lines.Keys)
            {
                foreach (var line in response.Lines[transportType])
                {
                    Assert.IsTrue(line.TransportType == transportType);
                    Assert.IsTrue(!string.IsNullOrEmpty(line.URL));
                    Assert.IsTrue(!string.IsNullOrEmpty(line.Value));

                    if (line.Attributes.Any())
                        anyAttributeLoaded = true;
                }
            }

            Assert.IsTrue(anyAttributeLoaded);
        }

        #endregion TEST METHODS

    }
}
