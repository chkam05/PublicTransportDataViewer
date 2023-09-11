using ZtmDataDownloader;
using ZtmDataDownloader.Data.Arrivals;
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Data.TimeTables;

namespace ZtmDataTester
{
    [TestFixture]
    public class ZtmDataDownloaderTest
    {

        //  VARIABLES

        private int _timeTablesMaxItemstTest;
        private TransportType _transportTypeTest;


        //  METHODS

        #region SETUP METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Ztm data downloader test setup. </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            _timeTablesMaxItemstTest = 3;
            _transportTypeTest = TransportType.Tram;
        }

        #endregion SETUP METHODS

        #region TEST METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Lines download and serialization test. </summary>
        [Test]
        public void LinesDownloadTest()
        {
            var lines = SimpleDownloader.DownloadLines();

            Assert.IsTrue(lines?.Any() ?? false);

            if (lines?.Any() ?? false)
            {
                Assert.IsTrue(lines.ContainsKey(TransportType.Bus));
                Assert.IsTrue(lines.ContainsKey(TransportType.BusMetropolitan));
                Assert.IsTrue(lines.ContainsKey(TransportType.Tram));
                Assert.IsTrue(lines.ContainsKey(TransportType.Trolley));

                bool anyAttributeLoaded = false;
                bool anyColorLoaded = false;

                foreach (var transportType in lines.Keys)
                {
                    foreach (var line in lines[transportType])
                    {
                        Assert.IsTrue(line.Type == transportType);
                        Assert.IsTrue(!string.IsNullOrEmpty(line.Description));
                        Assert.IsTrue(!string.IsNullOrEmpty(line.URL));
                        Assert.IsTrue(!string.IsNullOrEmpty(line.Value));

                        if (line.Attributes.Any())
                            anyAttributeLoaded = true;

                        if (!string.IsNullOrEmpty(line.Color))
                            anyColorLoaded = true;
                    }
                }

                Assert.IsTrue(anyAttributeLoaded);
                Assert.IsTrue(anyColorLoaded);
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Time tables download and serialization test. </summary>
        [Test]
        public void TimeTablesDownloadTest()
        {
            var lines = SimpleDownloader.DownloadLines();

            Assert.IsTrue(lines?.Any() ?? false);

            if (lines?.Any() ?? false)
            {
                bool anyTimeTableLoaded = false;
                int lineIndex = 0;

                foreach (var line in lines[_transportTypeTest].Where(l => l.IsPathChanged && l.IsUpdated))
                {
                    var timeTables = SimpleDownloader.DownloadTimeTables(line);

                    if (timeTables?.Any() ?? false)
                    {
                        anyTimeTableLoaded = true;
                        lineIndex++;

                        foreach (var timeTable in timeTables)
                        {
                            Assert.IsTrue(!string.IsNullOrEmpty(timeTable.ID));
                            Assert.IsTrue(!string.IsNullOrEmpty(timeTable.Value));
                            Assert.IsTrue(!string.IsNullOrEmpty(timeTable.URL));
                            Assert.IsTrue(timeTable.StartDate.HasValue || timeTable.EndDate.HasValue);
                        }

                        if (lineIndex >= _timeTablesMaxItemstTest)
                            break;
                    }
                }

                Assert.IsTrue(anyTimeTableLoaded);
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Line details download and serialization test. </summary>
        [Test]
        public void LineDetailsDonwloadTest()
        {
            var lines = SimpleDownloader.DownloadLines();
            var transportType = _transportTypeTest;

            Assert.IsTrue(lines?.Any() ?? false);

            if (lines?.Any() ?? false)
            {
                var lineTT = GetLineWithMultipleTimeTables(lines[transportType], out List<TimeTable>? timeTables);
                var line = GetLineWithoutMultipleTimeTables(lines[transportType]);

                Assert.IsNotNull(lineTT);
                Assert.IsNotNull(line);

                if (lineTT != null && (timeTables?.Any() ?? false))
                {
                    var lineTTDetails = SimpleDownloader.DownloadLineDetails(lineTT, timeTables[0].ID);
                    LineDetailsDownloadSubTest(lineTTDetails, transportType);
                }

                if (line != null)
                {
                    var lineDetails = SimpleDownloader.DownloadLineDetails(line);
                    LineDetailsDownloadSubTest(lineDetails, transportType);
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Line details test. </summary>
        /// <param name="lineDetails"> Line details for test. </param>
        /// <param name="transportType"> Transport type. </param>
        private void LineDetailsDownloadSubTest(LineDetails lineDetails, TransportType transportType)
        {
            Assert.IsNotNull(lineDetails);

            if (lineDetails != null)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(lineDetails.Description));
                Assert.IsTrue(lineDetails.HasDirections);
                Assert.IsTrue(!string.IsNullOrEmpty(lineDetails.Name));
                Assert.That(lineDetails.TransportType, Is.EqualTo(transportType));

                if (lineDetails.HasDirections)
                {
                    foreach (var lineDirection in lineDetails.Directions)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(lineDirection.Direction));
                        Assert.IsTrue(lineDirection.Cities?.Any() ?? false);
                        Assert.IsTrue(lineDirection.Stops?.Any() ?? false);

                        if (lineDirection.Cities?.Any() ?? false)
                        {
                            foreach (var city in lineDirection.Cities)
                            {
                                Assert.IsTrue(!string.IsNullOrEmpty(city.Color));
                                Assert.IsTrue(!string.IsNullOrEmpty(city.Name));
                            }
                        }

                        if (lineDirection.Stops?.Any() ?? false)
                        {
                            var anyAttributeLoaded = false;

                            foreach (var stop in lineDirection.Stops)
                            {
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.City));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.Color));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.DirectionID));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.ID));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.Name));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.Platform));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.PlatformDescription));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.TimeTableID));
                                Assert.IsTrue(!string.IsNullOrEmpty(stop.URL));

                                if (stop.Attributes?.Any() ?? false)
                                    anyAttributeLoaded = true;
                            }

                            Assert.IsTrue(anyAttributeLoaded);
                        }
                    }
                }

                if (lineDetails.HasMessages)
                {
                    foreach (var message in lineDetails.Messages)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(message.MessageText));
                        Assert.IsTrue(!string.IsNullOrEmpty(message.URL));
                    }
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Departures download and serialization test. </summary>
        [Test]
        public void DeparturesDownloadTest()
        {
            var lines = SimpleDownloader.DownloadLines();
            var transportType = _transportTypeTest;
            var departures = GetDeparturesForTest(lines, transportType);

            Assert.IsNotNull(departures?.Any() ?? false);

            if (departures?.Any() ?? false)
            {
                int departureGroupIndex = -1;

                foreach (var departureGroup in departures.Keys)
                {
                    Assert.Less(departureGroupIndex, departureGroup.Index);
                    Assert.IsTrue(!string.IsNullOrEmpty(departureGroup.Description));
                    Assert.IsTrue(departures.ContainsKey(departureGroup));

                    var departureValues = departures[departureGroup];

                    Assert.IsTrue(departureValues?.Any() ?? false);

                    if (departureValues?.Any() ?? false)
                    {
                        foreach (var departure in departureValues)
                        {
                            Assert.IsTrue(!string.IsNullOrEmpty(departure.Description));
                            Assert.IsTrue(!string.IsNullOrEmpty(departure.DirectID));
                            Assert.IsTrue(!string.IsNullOrEmpty(departure.DirectionID));
                            Assert.That(departureGroup.Index, Is.EqualTo(departure.GroupID));
                            Assert.That(departureGroup.Description, Is.EqualTo(departure.GroupDescription));
                            Assert.IsTrue(!string.IsNullOrEmpty(departure.StopID));
                            Assert.IsTrue(!string.IsNullOrEmpty(departure.URL));
                            Assert.IsTrue(!string.IsNullOrEmpty(departure.Value));
                        }
                    }

                    departureGroupIndex = departureGroup.Index;
                }
            }
        }

        //  --------------------------------------------------------------------------------
        [Test]
        public void ArrivalsDownloadTest()
        {
            var lines = SimpleDownloader.DownloadLines();
            var transportType = _transportTypeTest;
            var departureDetails = GetArrivalsForTest(lines, transportType);

            Assert.IsNotNull(departureDetails);

            if (departureDetails != null)
            {
                Assert.IsTrue(departureDetails.HasArrivalData);
                Assert.IsTrue(departureDetails.Cities?.Any() ?? false);
                Assert.IsTrue(!string.IsNullOrEmpty(departureDetails.Description));
                Assert.IsTrue(departureDetails.Informations?.Any() ?? false);

                if (departureDetails.HasArrivalData)
                {
                    foreach (var arrival in departureDetails.Arrivals)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(arrival.Color));
                        Assert.IsTrue(!string.IsNullOrEmpty(arrival.Name));
                        Assert.IsTrue(!string.IsNullOrEmpty(arrival.SpecialDistance));
                        Assert.IsTrue(!string.IsNullOrEmpty(arrival.SpecialTime));
                        Assert.IsTrue(!string.IsNullOrEmpty(arrival.Value));

                        Assert.IsTrue(arrival.Links?.Any() ?? false);

                        if (arrival.Links?.Any() ?? false)
                        {
                            foreach (var link in arrival.Links)
                            {
                                Assert.IsTrue(!string.IsNullOrEmpty(link.Title));
                                Assert.IsTrue(!string.IsNullOrEmpty(link.URL));
                            }
                        }
                    }
                }

                if (departureDetails.Cities?.Any() ?? false)
                {
                    foreach (var city in departureDetails.Cities)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(city.Color));
                        Assert.IsTrue(!string.IsNullOrEmpty(city.Name));
                    }
                }

                if (departureDetails.Informations?.Any() ?? false)
                {
                    foreach (var information in departureDetails.Informations)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(information.Key));
                        Assert.IsTrue(!string.IsNullOrEmpty(information.Value));
                    }
                }
            }
        }

        #endregion TEST METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get line with multiple time tables. </summary>
        /// <param name="lines"> Lines list. </param>
        /// <param name="timeTables"> Output of downloaded time tables. </param>
        /// <returns> Line with multiple time tables. </returns>
        private Line? GetLineWithMultipleTimeTables(List<Line> lines, out List<TimeTable>? timeTables)
        {
            timeTables = null;

            foreach (var line in lines.Where(l => l.IsPathChanged && l.IsUpdated))
            {
                timeTables = SimpleDownloader.DownloadTimeTables(line);

                if (timeTables?.Any() ?? false)
                    return line;
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line without multiple time tables. </summary>
        /// <param name="lines"> Lines list. </param>
        /// <returns> Line without multiple time tables. </returns>
        private Line? GetLineWithoutMultipleTimeTables(List<Line> lines)
        {
            return lines.FirstOrDefault(l => !l.IsPathChanged 
                && !(SimpleDownloader.DownloadTimeTables(l)?.Any() ?? false));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line stop departures to test. </summary>
        /// <param name="lines"> Lines set. </param>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> First line direction stop. </returns>
        private Dictionary<DepartureGroup, List<Departure>>? GetDeparturesForTest(Dictionary<TransportType, List<Line>> lines, TransportType transportType)
        {
            if (lines?.Any() ?? false)
            {
                var line = GetLineWithoutMultipleTimeTables(lines[transportType]);

                if (line != null)
                {
                    var lineDetails = SimpleDownloader.DownloadLineDetails(line);

                    if (lineDetails?.HasDirections ?? false)
                    {
                        var direction = lineDetails.Directions.FirstOrDefault(d => d.Stops.Any());
                        var stop = direction?.Stops.FirstOrDefault();

                        if (stop != null)
                            return SimpleDownloader.DownloadDepartures(line, stop);
                    }
                }
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get departure details to test. </summary>
        /// <param name="lines"> Lines set. </param>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Departure details. </returns>
        private DepartureDetails? GetArrivalsForTest(Dictionary<TransportType, List<Line>> lines, TransportType transportType)
        {
            if (lines?.Any() ?? false)
            {
                var line = GetLineWithoutMultipleTimeTables(lines[transportType]);

                if (line != null)
                {
                    var lineDetails = SimpleDownloader.DownloadLineDetails(line);

                    if (lineDetails?.HasDirections ?? false)
                    {
                        var direction = lineDetails.Directions.FirstOrDefault(d => d.Stops.Any());
                        var stop = direction?.Stops.FirstOrDefault();

                        if (stop != null)
                        {
                            var departures = SimpleDownloader.DownloadDepartures(line, stop);

                            if (departures?.Any() ?? false)
                            {
                                var departure = departures.FirstOrDefault().Value?.FirstOrDefault();

                                if (departure != null)
                                    return SimpleDownloader.DownloadArrivalsData(line, departure);
                            }
                        }
                    }
                }
            }

            return null;    
        }

        #endregion UTILITY METHODS

    }
}