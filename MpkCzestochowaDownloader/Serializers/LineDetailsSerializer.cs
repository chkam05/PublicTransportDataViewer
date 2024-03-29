﻿using DownloaderCore;
using DownloaderCore.Utilities;
using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MpkCzestochowaDownloader.Serializers
{
    public class LineDetailsSerializer : DedicatedSerializer<LineDetailsResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "<section class=\"section\">";
        private static readonly string _endTrimmer = "</section>";


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsSerializer class constructor. </summary>
        public LineDetailsSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw line details data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized line details data from web response. </returns>
        public override LineDetailsResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new LineDetailsResponseModel();
            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                var lineDetails = ReadLineDetailsData(xmlData);

                if (lineDetails != null)
                    result.LineDetails = lineDetails;
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
                .Replace("selected ", "selected=\"\" ")
                .Replace("<br>", "<br/>");

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

                /*if (FixParagraph(dataLine, out string paragraphCorrected))
                {
                    dataLine = paragraphCorrected;
                    dataLines[index] = paragraphCorrected;
                }*/
            }
        }

        #endregion PREPROCESSING DATA METHODS

        #region READ LINE DETAILS DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Read line details data. </summary>
        /// <param name="grouppedNodes"> XML line details data. </param>
        /// <returns> Line details data object. </returns>
        private LineDetails? ReadLineDetailsData(XElement xmlLineDetailsData)
        {
            var directionsSectionDiv = GetDirectionsSection(xmlLineDetailsData);
            var routeHeaderSectionDiv = GetRouteHeaderSection(xmlLineDetailsData);
            var timeTableTitleSectionForm = GetTimeTableTitleSection(xmlLineDetailsData);

            if (directionsSectionDiv == null)
                return null;

            if (routeHeaderSectionDiv == null)
                return null;

            if (timeTableTitleSectionForm == null)
                return null;

            var lineDetails = new LineDetails()
            {
                LineId = GetLineId(timeTableTitleSectionForm),
                Value = GetLineValue(routeHeaderSectionDiv)
            };

            var dates = ReadTimeTableDates(timeTableTitleSectionForm);
            var directionsNames = GetDirectionsNames(routeHeaderSectionDiv);
            var lineAttributes = GetLineAttributes(routeHeaderSectionDiv);
            var lineDirections = ReadLineDirections(directionsSectionDiv);
            var variants = ReadRouteVariants(routeHeaderSectionDiv);

            if (dates != null)
                lineDetails.Dates = dates;

            if (directionsNames?.Any() ?? false)
            {
                lineDetails.DirectionFrom = directionsNames.First();
                lineDetails.DirectionTo = directionsNames.Last();
            }

            if (lineAttributes != null)
                lineDetails.Attributes = lineAttributes;

            if (lineDirections != null)
                lineDetails.Directions = lineDirections;

            if (variants != null)
                lineDetails.RouteVariants = variants;

            return lineDetails;
        }

        //  --------------------------------------------------------------------------------
        private List<LineDirection>? ReadLineDirections(XElement directionsSectionDiv)
        {
            var directions = directionsSectionDiv?.Descendants("table");

            if (directions != null)
            {
                return directions.Select(d =>
                {
                    var lineDirection = new LineDirection()
                    {
                        Name = d.Descendants("th").First().Value
                    };

                    var stops = ReadLineDirectionStops(d.Element("tbody"));

                    if (stops != null)
                        lineDirection.Stops = stops;

                    return lineDirection;
                }).ToList();
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        private List<LineStop>? ReadLineDirectionStops(XElement? directionTBody)
        {
            if (directionTBody == null)
                return null;

            var stops = directionTBody.Descendants("a");

            if (stops.Any())
            {
                return stops.Select(s =>
                {
                    var stop = new LineStop()
                    {
                        Name = s.Value,
                        URL = s.Attribute("href")?.Value
                    };

                    var attributes = s.Attribute("class")?.Value.Trim().Split(" ").ToList();

                    if (attributes != null)
                        stop.Attributes = attributes;

                    return stop;
                }).ToList();
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        private List<RouteVariant>? ReadRouteVariants(XElement optionsHeaderNode)
        {
            var variants = optionsHeaderNode.Element("div")?.Descendants("option");

            if (variants?.Any() ?? false)
            {
                return variants.Select(o => new RouteVariant()
                {
                    Name = o.Value.Trim(),
                    Selected = o.Attribute("selected") != null,
                    Variant = o.Attribute("value")?.Value
                }).ToList();
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        private List<TimeTableDate>? ReadTimeTableDates(XElement timeTableTitleSectionForm)
        {
            var options = timeTableTitleSectionForm.Descendants("option");

            if (options.Any())
            {
                return options.Select(o => new TimeTableDate()
                {
                    Date = DateTime.TryParseExact(o.Attribute("value")?.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDate) ? selectedDate : null,
                    Selected = o.Attribute("selected") != null,
                    Title = o.Value.Trim()
                }).ToList();
            }

            return null;
        }

        #endregion READ LINE DETAILS DATA

        #region XML DATA GET METHODS

        //  --------------------------------------------------------------------------------
        private List<string>? GetDirectionsNames(XElement routeHeaderSectionDiv)
        {
            return routeHeaderSectionDiv.Element("span")
                ?.Elements("span")
                .Where(e => e.Attribute("class")?.Value.Contains("d-block") ?? false)
                .Select(e => e.Value.Trim())
                .ToList();
        }

        //  --------------------------------------------------------------------------------
        private List<string>? GetLineAttributes(XElement routeHeaderSectionDiv)
        {
            return routeHeaderSectionDiv.Element("span")
                ?.Elements("span")
                .FirstOrDefault(e => e.Attribute("class")?.Value.Contains("route") ?? false)
                ?.Attribute("class")?.Value.Trim().Split(" ").ToList();
        }

        //  --------------------------------------------------------------------------------
        private string? GetLineId(XElement timeTableTitleSectionForm)
        {
            return timeTableTitleSectionForm.Descendants("input")
                ?.FirstOrDefault(e => e.Attribute("name")?.Value == "linia")
                ?.Attribute("value")?.Value;
        }

        //  --------------------------------------------------------------------------------
        private string? GetLineValue(XElement routeHeaderSectionDiv)
        {
            return routeHeaderSectionDiv.Element("span")
                ?.Elements("span")
                .FirstOrDefault(e => e.Attribute("class")?.Value.Contains("route") ?? false)
                ?.Value.Trim();
        }

        #endregion XML DATA GET METHODS

        #region XML SECTIONS GET METHODS

        //  --------------------------------------------------------------------------------
        private XElement? GetDirectionsSection(XElement xmlLineDeparturesData)
        {
            return xmlLineDeparturesData.Elements("div")
                .FirstOrDefault(e => e.Attribute("class")?.Value.Contains("row") ?? false);
        }

        //  --------------------------------------------------------------------------------
        private XElement? GetRouteHeaderSection(XElement xmlLineDeparturesData)
        {
            return xmlLineDeparturesData.Elements("div")
                .FirstOrDefault(e => e.Attribute("class")?.Value.Contains("route-header") ?? false);
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
