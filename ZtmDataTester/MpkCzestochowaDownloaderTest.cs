using MpkCzestochowaDownloader.Data.Arrives;
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
        [Test, Order(1)]
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
        [Test, Order(2)]
        public void LineDetailsDonwloadTest()
        {
            var lines = new List<Line?>()
            {
                GetRandomLine(TransportType.Bus),
                GetRandomLine(TransportType.BusNight),
                GetRandomLine(TransportType.BusSuburban),
                GetRandomLine(TransportType.Tram)
            };

            bool anyAttributeLoaded = false;
            bool anyRouteVariantLoaded = false;

            foreach (var line in lines)
            {
                Assert.IsNotNull(line);

                var downloader = new LineDetailsDownloader();
                var request = new LineDetailsRequestModel(line.Value);
                var response = downloader.DownloadData(request);

                Assert.IsFalse(response.HasErrors);
                Assert.IsTrue(response.HasData);
                Assert.IsNotNull(response.LineDetails);

                var lineDetails = response.LineDetails;

                Assert.IsTrue(lineDetails.Dates.Any());
                Assert.IsTrue(!string.IsNullOrEmpty(lineDetails.DirectionFrom));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDetails.DirectionTo));
                Assert.IsTrue(lineDetails.Directions.Any());
                Assert.IsTrue(!string.IsNullOrEmpty(lineDetails.LineId));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDetails.Value));

                if (lineDetails.Attributes.Any())
                    anyAttributeLoaded = true;

                if (lineDetails.Dates.Any())
                {
                    var anyDateSelected = false;

                    foreach (var date in lineDetails.Dates)
                    {
                        Assert.IsNotNull(date.Date);
                        Assert.IsTrue(!string.IsNullOrEmpty(date.Title));

                        anyDateSelected = date.Selected ? true : anyDateSelected;
                    }

                    Assert.IsTrue(anyDateSelected);
                }

                if (lineDetails.Directions.Any())
                {
                    foreach (var direction in lineDetails.Directions)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(direction.Name));
                        Assert.IsTrue(direction.Stops.Any());

                        var anyStopAttributeLoaded = false;

                        foreach (var stop in direction.Stops)
                        {
                            Assert.IsTrue(!string.IsNullOrEmpty(stop.Name));
                            Assert.IsTrue(!string.IsNullOrEmpty(stop.URL));

                            if (stop.Attributes.Any())
                                anyStopAttributeLoaded = true;
                        }

                        Assert.IsTrue(anyStopAttributeLoaded);
                    }
                }

                if (lineDetails.RouteVariants.Any())
                {
                    anyRouteVariantLoaded = true;
                    var anyRouteVariantSelected = false;

                    foreach (var routeVariant in lineDetails.RouteVariants)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(routeVariant.Name));
                        Assert.IsNotNull(routeVariant.Variant);

                        anyRouteVariantSelected = routeVariant.Selected ? true : anyRouteVariantSelected;
                    }

                    Assert.IsTrue(anyRouteVariantSelected);
                }
            }

            Assert.IsTrue(anyAttributeLoaded);
            Assert.IsTrue(anyRouteVariantLoaded);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Line stop departures download and serialization test. </summary>
        [Test, Order(3)]
        public void LineDeparturesDownloadTest()
        {
            var lineDetailsList = new List<LineDetails?>()
            {
                GetRandomLineDetails(TransportType.Bus),
                GetRandomLineDetails(TransportType.BusSuburban),
                GetRandomLineDetails(TransportType.BusNight),
                GetRandomLineDetails(TransportType.Tram)
            };

            var anyDepartureAttribute = false;
            var anyOtherLineAttributeLoaded = false;
            var anyOtherLineLoaded = false;
            var anyStopAttribute = false;

            foreach (var lineDetails in lineDetailsList)
            {
                var lineStop = GetRandomLineStop(lineDetails);

                Assert.IsNotNull(lineStop);
                Assert.IsTrue(!string.IsNullOrEmpty(lineStop.URL));

                var downloader = new LineDeparturesDownloader();
                var request = new LineDeparturesRequestModel(lineStop.URL);
                var response = downloader.DownloadData(request);

                Assert.IsFalse(response.HasErrors);
                Assert.IsTrue(response.HasData);
                Assert.IsNotNull(response.LineDepartures);

                var lineDepartures = response.LineDepartures;

                Assert.IsTrue(lineDepartures.Dates.Any());
                Assert.IsTrue(lineDepartures.Departures.Any());
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.DirectionId));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.DirectionName));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.ImageURL));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.LineId));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.RouteVariantId));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.StopId));
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.StopName));
                Assert.IsTrue(lineDepartures.Stops.Any());
                Assert.IsTrue(!string.IsNullOrEmpty(lineDepartures.Value));

                var anyDateSelected = false;

                foreach (var date in lineDepartures.Dates)
                {
                    Assert.IsNotNull(date.Date);
                    Assert.IsTrue(!string.IsNullOrEmpty(date.Title));

                    anyDateSelected = date.Selected ? true : anyDateSelected;
                }

                Assert.IsTrue(anyDateSelected);

                foreach (var departure in lineDepartures.Departures)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(departure.DataRoute));
                    Assert.IsTrue(!string.IsNullOrEmpty(departure.DataTrip));
                    Assert.IsNotNull(departure.Time);

                    if (departure.Attributes.Any())
                        anyDepartureAttribute = true;
                }

                if (lineDepartures.OtherLines.Any())
                {
                    anyOtherLineLoaded = true;

                    foreach (var otherLine in lineDepartures.OtherLines)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(otherLine.URL));
                        Assert.IsTrue(!string.IsNullOrEmpty(otherLine.Value));

                        if (otherLine.Attributes.Any())
                            anyOtherLineAttributeLoaded = true;
                    }
                }

                foreach (var stop in lineDepartures.Stops)
                {
                    Assert.IsTrue(!string.IsNullOrEmpty(stop.Name));
                    Assert.IsTrue(!string.IsNullOrEmpty(stop.URL));

                    if (stop.Attributes.Any())
                        anyStopAttribute = true;
                }
            }

            Assert.IsTrue(anyDepartureAttribute);
            Assert.IsTrue(anyOtherLineAttributeLoaded);
            Assert.IsTrue(anyOtherLineLoaded);
            Assert.IsTrue(anyStopAttribute);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Line arrivals download and serialization test. </summary>
        [Test, Order(4)]
        public void LineArrivalsDownloadTest()
        {
            var lineDetailsList = new List<LineDetails?>()
            {
                GetRandomLineDetails(TransportType.Bus),
                GetRandomLineDetails(TransportType.BusSuburban),
                GetRandomLineDetails(TransportType.BusNight),
                GetRandomLineDetails(TransportType.Tram)
            };

            foreach (var lineDetails in lineDetailsList)
            {
                var date = (lineDetails?.Dates.FirstOrDefault(d => d.Selected)?.Date ?? DateTime.Now)
                    .ToString("yyyy-MM-dd");

                var departure = GetLineFirstDeparture(lineDetails);

                Assert.IsTrue(!string.IsNullOrEmpty(date));
                Assert.IsNotNull(departure);
                Assert.IsNotNull(departure.DataTrip);
                Assert.IsNotNull(departure.Time);

                var downloader = new LineArrivalsDownloader();
                var request = new LineArrivalsRequestModel(
                    departure.DataTrip,
                    departure.Time.Value.ToString("HH:mm:ss"),
                    date);

                var response = downloader.DownloadData(request);

                Assert.IsFalse(response.HasErrors);
                Assert.IsTrue(response.HasData);
                Assert.IsNotNull(response.LineArrivals);

                var lineArrivals = response.LineArrivals;
                int lineCounter = -1;

                Assert.IsNotNull(lineArrivals.TripTime);
                Assert.IsTrue(lineArrivals.Arrivals.Any());

                foreach (var arrival in lineArrivals.Arrivals)
                {
                    Assert.IsTrue(lineCounter < arrival.Id);
                    Assert.IsTrue(!string.IsNullOrEmpty(arrival.StopName));
                    Assert.IsNotNull(arrival.Time);

                    arrival.Id = lineCounter;
                }
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

        //  --------------------------------------------------------------------------------
        /// <summary> Get random line stop from line details. </summary>
        /// <param name="lineDetails"> Line details data object. </param>
        /// <returns> Line stop data object. </returns>
        private LineStop? GetRandomLineStop(LineDetails? lineDetails)
        {
            if (lineDetails?.Directions.Any() ?? false)
            {
                var random = new Random();
                var randomDirectionIndex = random.Next(lineDetails.Directions.Count);
                var direction = lineDetails.Directions[randomDirectionIndex];

                if (direction.Stops.Any())
                {
                    var randomStopIndex = random.Next(direction.Stops.Count);
                    return direction.Stops[randomStopIndex];
                }
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        private Departure? GetLineFirstDeparture(LineDetails? lineDetails)
        {
            if (lineDetails?.Directions.Any() ?? false)
            {
                var direction = lineDetails.Directions.First();

                if (direction.Stops.Any())
                {
                    var lineStop = direction.Stops.First();

                    var downloader = new LineDeparturesDownloader();
                    var request = new LineDeparturesRequestModel(lineStop.URL);
                    var response = downloader.DownloadData(request);

                    if (response.HasData && !response.HasErrors)
                    {
                        var lineDepartures = response.LineDepartures;

                        return lineDepartures.Departures.FirstOrDefault();
                    }
                }
            }

            return null;
        }

        #endregion UTILITY METHODS

    }
}
