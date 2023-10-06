using MpkCzestochowaDownloader.Data.Departures;
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
            var lines = new List<Line?>()
            {
                GetRandomLine(TransportType.Bus),
                GetRandomLine(TransportType.BusNight),
                GetRandomLine(TransportType.BusSuburban),
                GetRandomLine(TransportType.Tram)
            };

            bool anyTimeTable = false;
            bool anyVariant = false;

            foreach (var line in lines)
            {
                Assert.IsNotNull(line);

                if (line != null)
                {
                    var downloader = new LineDetailsDownloader();
                    var request = new LineDetailsRequestModel(line.Value);

                    var response = downloader.DownloadData(request);

                    Assert.IsFalse(response.HasErrors);
                    Assert.IsTrue(response.HasData);

                    Assert.IsTrue(!string.IsNullOrWhiteSpace(response.LineDetails.DirectionFrom));
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(response.LineDetails.DirectionTo));
                    Assert.IsTrue(response.LineDetails.Directions.Any());
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(response.LineDetails.Value));

                    foreach (var direction in response.LineDetails.Directions)
                    {
                        Assert.IsTrue(direction.Stops.Any());

                        foreach (var stop in direction.Stops)
                        {
                            Assert.IsTrue(!string.IsNullOrEmpty(stop.Name));
                            Assert.IsTrue(!string.IsNullOrEmpty(stop.URL));
                        }
                    }

                    var dates = response.LineDetails.Dates;
                    var routeVariants = response.LineDetails.RouteVariants;

                    if (dates.Any())
                    {
                        Assert.IsNotNull(response.LineDetails.Date);
                        Assert.IsNotNull(response.LineDetails.Date.Date);
                        Assert.IsTrue(!string.IsNullOrEmpty(response.LineDetails.Date.Title));

                        anyTimeTable = true;
                    }

                    if (routeVariants.Any())
                    {
                        Assert.IsNotNull(response.LineDetails.RouteVariant);
                        Assert.IsTrue(!string.IsNullOrEmpty(response.LineDetails.RouteVariant.Name));
                        Assert.IsNotNull(response.LineDetails.RouteVariant.Variant);

                        anyVariant = true;
                    }
                }

                Assert.IsTrue(anyTimeTable);
                Assert.IsTrue(anyVariant);
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Line stop departures download and serialization test. </summary>
        [Test]
        public void LineDeparturesDownloadTest()
        {
            var lineDetails = GetRandomLineDetails(TransportType.Tram);

            Assert.IsNotNull(lineDetails);

            if (lineDetails != null)
            {
                var random = new Random();
                int randomIndex = random.Next(lineDetails.Directions[0].Stops.Count);
                var lineStop = lineDetails.Directions[0].Stops[randomIndex];

                var downloader = new LineDeparturesDownloader();
                var request = new LineDeparturesRequestModel(lineStop.URL);
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

        //  --------------------------------------------------------------------------------
        /// <summary> Get random line details from specific transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Random selected line details. </returns>
        private LineDetails? GetRandomLineDetails(TransportType transportType)
        {
            var line = GetRandomLine(transportType);

            if (line == null)
                return null;

            var downloader = new LineDetailsDownloader();
            var request = new LineDetailsRequestModel(line.Value);
            var response = downloader.DownloadData(request);

            return response.HasData ? response.LineDetails : null;
        }

        #endregion UTILITY METHODS

    }
}
