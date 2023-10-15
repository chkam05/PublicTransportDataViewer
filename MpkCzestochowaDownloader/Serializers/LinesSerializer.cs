using DownloaderCore;
using DownloaderCore.Utilities;
using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Data.Static;
using MpkCzestochowaDownloader.Mappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MpkCzestochowaDownloader.Serializers
{
    public class LinesSerializer : DedicatedSerializer<LinesResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "col-12  col-md-6 left-city";
        private static readonly string _endTrimmer = "col-12 col-md-6 right-city";


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LinesSerializer class constructor. </summary>
        public LinesSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized lines data from web response. </returns>
        public override LinesResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new LinesResponseModel();
            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                var sections = xmlData.Elements().Where(e => e.Name == "section").ToList();

                var grouppedLinesSectionDiv = GetGrouppedLinesSection(xmlData);
                var messagesSectionTable = GetMessagesSection(xmlData);

                var linesData = ReadLinesData(grouppedLinesSectionDiv);
                var messages = ReadMessagesData(messagesSectionTable);

                if (linesData?.Any() ?? false)
                    result.Lines = linesData;

                if (messages?.Any() ?? false)
                    result.Messages = messages;
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
                .Replace("\t", "");

            trimmedData = StringUtils.RemoveExcessSpaces(trimmedData);

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

            int startIndex = lines.IndexOf(lines.FirstOrDefault(l => l.Contains(_startTrimmer)));
            int endIndex = lines.IndexOf(lines.FirstOrDefault(l => lines.IndexOf(l) > startIndex && l.Contains(_endTrimmer)));

            List<string> dataLines = lines.GetRange(startIndex, endIndex - startIndex);
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

                if (FixBreakLine(dataLine, out string breakLineCorrected))
                {
                    dataLine = breakLineCorrected;
                    dataLines[index] = breakLineCorrected;
                }
            }
        }

        #endregion PREPROCESSING DATA METHODS

        #region READ LINES DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Read lines data. </summary>
        /// <param name="grouppedLinesSectionDiv"> Groupped by TransportType XML lines section [div] data. </param>
        /// <returns> Groupped by TransportType line data objects. </returns>
        private Dictionary<TransportType, List<Line>> ReadLinesData(Dictionary<TransportType, XElement> grouppedLinesSectionDiv)
        {
            var result = new Dictionary<TransportType, List<Line>>();

            foreach (var kvp in grouppedLinesSectionDiv)
            {
                var lineNodes = kvp.Value.Elements("a").ToList();

                if (lineNodes?.Any() ?? false)
                {
                    foreach (var lineNode in lineNodes)
                    {
                        var line = ReadLineData(kvp.Key, lineNode);

                        if (line != null)
                        {
                            if (!result.ContainsKey(kvp.Key))
                                result.Add(kvp.Key, new List<Line>());

                            result[kvp.Key].Add(line);
                        }
                    }
                }
            }

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line data. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <param name="xmlLineData"> XML line [a] data. </param>
        /// <returns> Line data object. </returns>
        private Line ReadLineData(TransportType transportType, XElement xmlLineData)
        {
            var attributes = xmlLineData.Attribute("class")?.Value.Trim().Split(" ").ToList();

            var line = new Line()
            {
                TransportType = transportType,
                URL = xmlLineData.Attribute("href")?.Value,
                Value = xmlLineData.Value.Trim()
            };

            if (attributes != null)
                line.Attributes = attributes;

            return line;
        }

        #endregion READ LINES DATA

        #region READ MESSAGES DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Read messages data. </summary>
        /// <param name="xmlMessagesNode"> XML messages data node. </param>
        /// <returns> List of messages objects. </returns>
        private List<Message>? ReadMessagesData(XElement xmlMessagesNode)
        {
            var rows = xmlMessagesNode
                .Descendants("tr")
                .Where(r => !r.Descendants("th").Any());

            if (rows == null || !rows.Any())
                return null;

            return rows.Select(r => ReadMessageData(r))
                .Where(r => r != null)
                .ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line data. </summary>
        /// <param name="xmlMessageRow"> XML message row [tr] data. </param>
        /// <returns> Message object. </returns>
        private Message ReadMessageData(XElement xmlMessageRow)
        {
            var columns = xmlMessageRow.Elements("td").ToList();

            var message = new Message()
            {
                Date = GetMessageDate(columns),
                URL = GetMessageURL(columns),
                Value = GetMessageValue(columns)
            };

            var lines = GetMessageLines(columns);

            if (lines != null)
                message.Lines = lines;

            return message;
        }

        #endregion READ MESSAGES DATA

        #region XML DATA GET METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get message date. </summary>
        /// <param name="xmlMessageColumns"> XML message columns [td] data. </param>
        /// <returns> Message date. </returns>
        private DateTime? GetMessageDate(List<XElement> xmlMessageColumns)
        {
            return xmlMessageColumns.Count() > 0
                ? DateTime.TryParseExact(xmlMessageColumns[0].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime) ? dateTime : null
                : null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get message lines. </summary>
        /// <param name="xmlMessageColumns"> XML message columns [td] data. </param>
        /// <returns> List of message lines. </returns>
        private List<string>? GetMessageLines(List<XElement> xmlMessageColumns)
        {
            var column = xmlMessageColumns.Count() > 1 ? xmlMessageColumns[1].Value : null;

            return column?.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim()).ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get message URL. </summary>
        /// <param name="xmlMessageColumns"> XML message columns [td] data. </param>
        /// <returns> Message URL. </returns>
        private string? GetMessageURL(List<XElement> xmlMessageColumns)
        {
            var column = xmlMessageColumns.Count() > 2 ? xmlMessageColumns[2].Element("a") : null;
            return column?.Attribute("href")?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get message value. </summary>
        /// <param name="xmlMessageColumns"> XML message columns [td] data. </param>
        /// <returns> Message value. </returns>
        private string? GetMessageValue(List<XElement> xmlMessageColumns)
        {
            var column = xmlMessageColumns.Count() > 2 ? xmlMessageColumns[2].Element("a") : null;
            return column?.Value.Trim();
        }

        #endregion XML DATA GET METHODS

        #region XML SECTIONS GET METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML lines data groupped by transport type. </summary>
        /// <param name="xmlLineDeparturesData"> XML lines data. </param>
        /// <returns> Groupped by transport type XML lines section data. </returns>
        private Dictionary<TransportType, XElement>? GetGrouppedLinesSection(XElement xmlLinesData)
        {
            var lines = xmlLinesData.Elements("section")
                .FirstOrDefault(s
                    => s.Elements().Any(e => e.Name == "h3")
                    && s.Elements().Any(e => e.Name == "div"))?
                .Elements()
                    .Where(e => e.Name == "h3" || e.Name == "div")
                .ToList();

            if (lines == null || !lines.Any())
                return null;

            XElement? key = null;
            var result = new Dictionary<TransportType, XElement>();

            foreach (var lineData in lines)
            {
                if (lineData.Name == "h3")
                    key = lineData;

                if (key != null && lineData.Name == "div")
                {
                    foreach (var i in key.Elements().Where(e => e.Name == "i"))
                    {
                        var classNames = i.Attribute("class")?.Value;

                        if (!string.IsNullOrEmpty(classNames))
                        {
                            var transportType = TransportTypesMapper.MapFromClass(
                                classNames.Split(" ", StringSplitOptions.RemoveEmptyEntries));

                            if (transportType.HasValue)
                            {
                                result.Add(transportType.Value, lineData);
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML messages data. </summary>
        /// <param name="xmlLinesData"> XML lines data. </param>
        /// <returns> XML messages section data. </returns>
        private XElement? GetMessagesSection(XElement xmlLinesData)
        {
            return xmlLinesData.Elements("section")
                .FirstOrDefault(s
                    => s.Elements().Any(e => e.Name == "h3")
                    && s.Elements().Any(e => e.Name == "div"))?
                .Elements()
                    .Where(e => e.Name == "table")
                .FirstOrDefault();
        }

        #endregion XML SECTIONS GET METHODS

    }
}
