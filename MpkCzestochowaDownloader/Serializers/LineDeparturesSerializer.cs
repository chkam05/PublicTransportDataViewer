using DownloaderCore;
using DownloaderCore.Utilities;
using MpkCzestochowaDownloader.Data.Departures;
using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MpkCzestochowaDownloader.Serializers
{
    public class LineDeparturesSerializer : BaseSerializer<LineDeparturesResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "<section class=\"section\">";
        private static readonly string _endTrimmer = "</section>";


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDeparturesSerializer class constructor. </summary>
        public LineDeparturesSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw line departures data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized line departures data from web response. </returns>
        public override LineDeparturesResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new LineDeparturesResponseModel();
            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                var lineDepartures = ReadLineDeparturesData(xmlData);

                if (lineDepartures != null)
                    result.LineDepartures = lineDepartures;
            }

            if (_errorMessages.Any())
                _errorMessages.ForEach(e => result.AddError(e));

            return result;
        }

        #endregion SERIALIZATION METHODS

        #region PREPROCESSING DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Preprocess raw data recived from web response to be correct XML data. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Preprocessed raw data. </returns>
        private string PreprocessData(string rawData)
        {
            string trimmedData = TrimData(rawData)
                .Trim()
                .Replace("\t", "")
                .Replace("&", "&amp;");

            trimmedData = StringUtils.RemoveExcessSpaces(trimmedData);

            trimmedData = trimmedData
                .Replace("selected ", "selected=\"\" ");

            return trimmedData;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Trim the data to get concrete results. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Trimmed raw data. </returns>
        private string TrimData(string rawData)
        {
            List<string> lines = rawData.Split(new string[] { Environment.NewLine, "\n" },
                StringSplitOptions.RemoveEmptyEntries).ToList();

            var startIndexes = FindPhrase(lines, _startTrimmer);
            var startIndex = startIndexes[1];

            if (startIndex.Item2 > 0)
                lines[startIndex.Item1] = lines[startIndex.Item1].Substring(startIndex.Item2);

            var endIndexes = FindPhrase(lines, _endTrimmer, startIndex.Item1);
            var endIndex = endIndexes[0];

            if (endIndex.Item2 > 0)
                lines[endIndex.Item1] = lines[endIndex.Item1].Substring(0, (endIndex.Item2 + _endTrimmer.Length));

            List<string> dataLines = lines.GetRange(startIndex.Item1, endIndex.Item1 - startIndex.Item1 + 1);
            CorrectInvalidLines(dataLines);

            return string.Join(string.Empty, dataLines);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Find phrase in lines of texts. </summary>
        /// <param name="textLines"> List of texts. </param>
        /// <param name="phrase"> Phrase to find. </param>
        /// <param name="lineStartIndex"> Line start index. </param>
        /// <returns> Tuple of line index, phrase position in line. </returns>
        private List<(int, int)> FindPhrase(List<string> textLines, string phrase, int lineStartIndex = 0)
        {
            var indexes = new List<(int, int)>();
            var startIndex = Math.Max(0, Math.Min(lineStartIndex, textLines.Count - 1));

            for (int index = startIndex; index < textLines.Count; index++)
            {
                int foundIndex = -1;

                while (true)
                {
                    foundIndex = textLines[index].IndexOf(phrase, foundIndex + 1);

                    if (foundIndex == -1)
                        break;

                    indexes.Add((index, foundIndex));
                }
            }

            return indexes;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Correct lines contains broken data. </summary>
        /// <param name="dataLines"> Raw data as lines. </param>
        private void CorrectInvalidLines(List<string> dataLines)
        {
            for (int index = 0; index < dataLines.Count; index++)
            {
                string dataLine = dataLines[index];

                if (FixImage(dataLine, out string imageCorrected))
                {
                    dataLine = imageCorrected;
                    dataLines[index] = imageCorrected;
                }

                if (FixInput(dataLine, out string inputCorrected))
                {
                    dataLine = inputCorrected;
                    dataLines[index] = inputCorrected;
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fix image nodes. </summary>
        /// <param name="dataLine"> Raw single line data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        private bool FixImage(string dataLine, out string correctedDataLine)
        {
            correctedDataLine = dataLine;

            if (dataLine.Contains("<img"))
            {
                int startIndex = dataLine.IndexOf("<img");
                int endIndex = dataLine.IndexOf(">", startIndex) + 1;

                string imgNode = dataLine.Substring(startIndex, endIndex - startIndex);

                if (!imgNode.EndsWith("/>"))
                {
                    correctedDataLine = dataLine.Replace(imgNode, imgNode.Replace(">", "/>"));
                    return true;
                }
            }

            return false;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fix input nodes. </summary>
        /// <param name="dataLine"> Raw single line data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        private bool FixInput(string dataLine, out string correctedDataLine)
        {
            correctedDataLine = dataLine;

            if (dataLine.Contains("<input"))
            {
                int startIndex = dataLine.IndexOf("<input");
                int endIndex = dataLine.IndexOf(">", startIndex) + 1;

                string imgNode = dataLine.Substring(startIndex, endIndex - startIndex);

                if (!imgNode.EndsWith("/>"))
                {
                    correctedDataLine = dataLine.Replace(imgNode, imgNode.Replace(">", "/>"));
                    return true;
                }
            }

            return false;
        }

        #endregion PREPROCESSING DATA METHODS

        #region READ LINE DEPARTURES METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line departures data. </summary>
        /// <param name="xmlLineDeparturesData"> XML line departures data. </param>
        /// <returns> Line departures data object. </returns>
        private LineDepartures? ReadLineDeparturesData(XElement xmlLineDeparturesData)
        {
            var departureTimesSectionTable = GetDepartureTimesSection(xmlLineDeparturesData);
            var routeHeaderSectionSpan = GetRouteHeaderSection(xmlLineDeparturesData);
            var stopsSectionTable = GetStopsSection(xmlLineDeparturesData);
            var timeTableTitleSectionForm = GetTimeTableTitleSection(xmlLineDeparturesData);

            if (departureTimesSectionTable == null)
                return null;

            if (routeHeaderSectionSpan == null)
                return null;

            if (stopsSectionTable == null)
                return null;

            if (timeTableTitleSectionForm == null)
                return null;

            var lineDepartures = new LineDepartures()
            {
                DirectionId = GetLineDepartureDirectionId(timeTableTitleSectionForm),
                LineId = GetLineDepartureLineId(timeTableTitleSectionForm),
                RouteVariantId = GetLineDepartureRouteVariantId(timeTableTitleSectionForm),
                StopId = GetLineDepartureStopId(timeTableTitleSectionForm),
                DirectionName = GetLineDirection(routeHeaderSectionSpan),
                StopName = GetLineStop(routeHeaderSectionSpan),
                Value = GetLineValue(routeHeaderSectionSpan),
            };

            var dates = ReadTimeTableDates(timeTableTitleSectionForm);
            var departures = ReadDepartures(departureTimesSectionTable);
            var stops = ReadLineStops(stopsSectionTable);
            var otherLines = ReadOtherLines(xmlLineDeparturesData);

            if (dates != null)
                lineDepartures.Dates = dates;

            if (departures != null)
                lineDepartures.Departures = departures;

            if (stops != null)
                lineDepartures.Stops = stops;

            if (otherLines != null)
                lineDepartures.OtherLines = otherLines;

            return lineDepartures;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read time table dates data. </summary>
        /// <param name="timeTableTitleSectionForm"> XML time table title section [form] data. </param>
        /// <returns> List of time table dates data objects. </returns>
        private List<TimeTableDate>? ReadTimeTableDates(XElement timeTableTitleSectionForm)
        {
            var options = timeTableTitleSectionForm.Element("select")
                ?.Descendants("option");

            if (options == null)
                return null;

            return options.Select(o => new TimeTableDate()
            {
                Date = DateTime.TryParseExact(o.Attribute("value")?.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDate) ? selectedDate : null,
                Selected = o.Attribute("selected") != null,
                Title = o.Value.Trim()
            }).ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read departures data. </summary>
        /// <param name="departureTimesSectionTable"> XML departure times section [table] data. </param>
        /// <returns> List of departures data objects. </returns>
        private List<Departure>? ReadDepartures(XElement departureTimesSectionTable)
        {
            var departures = departureTimesSectionTable
                .Element("tbody")
                ?.Descendants("span");

            if (departures == null)
                return null;

            return departures.Select(d =>
            {
                var departure = new Departure()
                {
                    DataRoute = d.Attribute("data-route")?.Value,
                    DataTrip = d.Attribute("data-trip")?.Value,
                    Time = DateTime.TryParseExact(d.Attribute("data-time")?.Value, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime) ? dateTime : null
                };

                var attributes = d.Attribute("class")?.Value.Trim().Split(" ").ToList();

                if (attributes != null)
                    departure.Attributes = attributes;

                return departure;
            }).ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line stops data. </summary>
        /// <param name="stopsSectionTable"> XML stops section [table] data. </param>
        /// <returns> List of line stops data objects. </returns>
        private List<LineStop>? ReadLineStops(XElement stopsSectionTable)
        {
            var stops = stopsSectionTable
                .Element("tbody")
                ?.Descendants("a");

            if (stops == null)
                return null;

            return stops.Select(s =>
            {
                var stop = new LineStop()
                {
                    Name = s.Value.Trim(),
                    URL = s.Attribute("href")?.Value
                };

                var attributes = s.Attribute("class")?.Value.Trim().Split(" ").ToList();

                if (attributes != null)
                    stop.Attributes = attributes;

                return stop;
            }).ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read other lines data. </summary>
        /// <param name="xmlLineDeparturesData"> XML line departures data. </param>
        /// <returns> List of other line data objects. </returns>
        private List<OtherLine>? ReadOtherLines(XElement xmlLineDeparturesData)
        {
            var otherRoutes = xmlLineDeparturesData.Elements("div")
                ?.FirstOrDefault(e => e.Attribute("class")?.Value.Contains("other-routes") ?? false)
                ?.Descendants("a");

            if (otherRoutes == null)
                return null;

            return otherRoutes.Select(o =>
            {
                var otherLine = new OtherLine()
                {
                    URL = o.Attribute("href")?.Value,
                    Value = o.Value.Trim()
                };

                var attributes = o.Attribute("class")?.Value.Trim().Split(" ").ToList();

                if (attributes != null)
                    otherLine.Attributes = attributes;

                return otherLine;
            }).ToList();
        }

        #endregion READ LINE DEPARTURES METHODS

        #region XML DATA GET METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get line departure direction id. </summary>
        /// <param name="timeTableTitleSectionForm"> XML time table title section [form] data. </param>
        /// <returns> Line departure direction id. </returns>
        private string? GetLineDepartureDirectionId(XElement timeTableTitleSectionForm)
        {
            return timeTableTitleSectionForm
                .Descendants("input")
                .FirstOrDefault(e => e.Attribute("name")?.Value == "kierunek")
                ?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line departure line id. </summary>
        /// <param name="timeTableTitleSectionForm"> XML time table title section [form] data. </param>
        /// <returns> Line departure line id. </returns>
        private string? GetLineDepartureLineId(XElement timeTableTitleSectionForm)
        {
            return timeTableTitleSectionForm
                .Descendants("input")
                .FirstOrDefault(e => e.Attribute("name")?.Value == "linia")
                ?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line departure route variant id. </summary>
        /// <param name="timeTableTitleSectionForm"> XML time table title section [form] data. </param>
        /// <returns> Line departure route variant id. </returns>
        private string? GetLineDepartureRouteVariantId(XElement timeTableTitleSectionForm)
        {
            return timeTableTitleSectionForm
                .Descendants("input")
                .FirstOrDefault(e => e.Attribute("name")?.Value == "trasa")
                ?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line departure stop id. </summary>
        /// <param name="timeTableTitleSectionForm"> XML time table title section [form] data. </param>
        /// <returns> Line departure stop id. </returns>
        private string? GetLineDepartureStopId(XElement timeTableTitleSectionForm)
        {
            return timeTableTitleSectionForm
                .Descendants("input")
                .FirstOrDefault(e => e.Attribute("name")?.Value == "przystanek")
                ?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line direction. </summary>
        /// <param name="timeTableTitleSectionForm"> XML route header section [span] data. </param>
        /// <returns> Line direction. </returns>
        private string? GetLineDirection(XElement routeHeaderSectionSpan)
        {
            return routeHeaderSectionSpan.Elements("span")
                ?.FirstOrDefault(e => e.Attribute("class")?.Value.Contains("d-block") ?? false)
                ?.Elements("span")?.FirstOrDefault(e => e.Value.Contains("kierunek"))
                ?.Elements("span")?.LastOrDefault()
                ?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line stop. </summary>
        /// <param name="timeTableTitleSectionForm"> XML route header section [span] data. </param>
        /// <returns> Line stop. </returns>
        private string? GetLineStop(XElement routeHeaderSectionSpan)
        {
            return routeHeaderSectionSpan.Elements("span")
                ?.FirstOrDefault(e => e.Attribute("class")?.Value.Contains("d-block") ?? false)
                ?.Elements("span")?.FirstOrDefault(e => e.Value.Contains("przystanek"))
                ?.Elements("span")?.LastOrDefault()
                ?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line number. </summary>
        /// <param name="timeTableTitleSectionForm"> XML route header section [span] data. </param>
        /// <returns> Line number. </returns>
        private string? GetLineValue(XElement routeHeaderSectionSpan)
        {
            return routeHeaderSectionSpan.Elements("span")
                ?.FirstOrDefault(e => e.Attribute("class")?.Value.Contains("route") ?? false)
                ?.Value;
        }

        #endregion XML DATA GET METHODS

        #region XML SECTIONS GET METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML departure times section data. </summary>
        /// <param name="xmlLineDeparturesData"> XML line depratures data. </param>
        /// <returns> XML departure times section data. </returns>
        private XElement? GetDepartureTimesSection(XElement xmlLineDeparturesData)
        {
            return xmlLineDeparturesData.Elements("div")
                .FirstOrDefault(e => e.Attribute("class")?.Value.Contains("row") ?? false)
                ?.Descendants("table")
                ?.FirstOrDefault(e => e.Attribute("class")?.Value.Contains("table") ?? false);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML route header section data. </summary>
        /// <param name="xmlLineDeparturesData"> XML line depratures data. </param>
        /// <returns> XML route header section data. </returns>
        private XElement? GetRouteHeaderSection(XElement xmlLineDeparturesData)
        {
            return xmlLineDeparturesData.Elements("div")
                .FirstOrDefault(e => e.Attribute("class")?.Value.Contains("route-header") ?? false)
                ?.Descendants("span")
                ?.FirstOrDefault(e => e.Attribute("class")?.Value.Contains("route-wrapper") ?? false);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML stops section data. </summary>
        /// <param name="xmlLineDeparturesData"> XML line depratures data. </param>
        /// <returns> XML stops section data. </returns>
        private XElement? GetStopsSection(XElement xmlLineDeparturesData)
        {
            return xmlLineDeparturesData.Elements("div")
                .FirstOrDefault(e => e.Attribute("class")?.Value.Contains("row") ?? false)
                ?.Descendants("table")
                ?.FirstOrDefault(e => e.Attribute("class")?.Value.Contains("stops") ?? false);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML time table title section data. </summary>
        /// <param name="xmlLineDeparturesData"> XML line depratures data. </param>
        /// <returns> XML time table title section data. </returns>
        private XElement? GetTimeTableTitleSection(XElement xmlLineDeparturesData)
        {
            return xmlLineDeparturesData.Element("h3")
                ?.Descendants("form")
                ?.FirstOrDefault();
        }

        #endregion XML SECTIONS GET METHODS

    }
}
