using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Data.Static;
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

        //  --------------------------------------------------------------------------------
        /// <summary> Line details download and serialization test. </summary>
        [Test]
        public void LineDetailsDonwloadTest()
        {
            var line = GetRandomLine(TransportType.Bus);

            Assert.IsNotNull(line);

            if (line != null)
            {
                var downloader = new LineDetailsDownloader();
                var request = new LineDetailsRequestModel("1");

                var response = downloader.DownloadData(request);

                Assert.IsFalse(response.HasErrors);
                Assert.IsTrue(response.HasData);
            }
        }

        #endregion TEST METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get random line from specific transpot type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Random selected line. </returns>
        private Line? GetRandomLine(TransportType transportType)
        {
            var downloader = new LinesDownloader();
            var request = new LinesRequestModel();

            var response = downloader.DownloadData(request);

            if (response.HasData && response.Lines.ContainsKey(transportType))
            {
                var random = new Random();
                int randomIndex = random.Next(response.Lines[transportType].Count);
                return response.Lines[transportType][randomIndex];
            }

            return null;
        }

        #endregion UTILITY METHODS

    }
}
