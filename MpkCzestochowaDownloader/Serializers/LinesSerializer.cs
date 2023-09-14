using DownloaderCore;
using DownloaderCore.Utilities;
using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Data.Static;
using MpkCzestochowaDownloader.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MpkCzestochowaDownloader.Serializers
{
    public class LinesSerializer : BaseSerializer<LinesResponseModel>
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

                var groupedDataNodes = GroupLinesDataByTransportType(sections);
                var messagesDataNode = GetMessagesDataNode(sections);

                var linesData = ReadLinesData(groupedDataNodes);
                var messages = ReadMessagesData(messagesDataNode);

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
        /// <summary> Fix break line nodes. </summary>
        /// <param name="dataLine"> Raw single line data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        private bool FixBreakLine(string dataLine, out string correctedDataLine)
        {
            correctedDataLine = dataLine;

            if (dataLine.Contains("<br>"))
            {
                while (correctedDataLine.Contains("<br>"))
                    correctedDataLine = correctedDataLine.Replace("<br>", "<br />");

                return true;
            }

            return false;
        }

        #endregion PREPROCESSING DATA METHODS

        #region PROCESS DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Group XML lines data by TransportType. </summary>
        /// <param name="xmlSections"> XML sections data. </param>
        /// <returns> Groupped XML lines data by TransportType. </returns>
        private Dictionary<TransportType, XElement> GroupLinesDataByTransportType(List<XElement> xmlSections)
        {
            var result = new Dictionary<TransportType, XElement>();

            var xmlLinesData = xmlSections?
                .FirstOrDefault(s
                    => s.Elements().Any(e => e.Name == "h3")
                    && s.Elements().Any(e => e.Name == "div"))?
                .Elements()
                    .Where(e => e.Name == "h3" || e.Name == "div")
                .ToList();

            XElement? key = null;

            if (xmlLinesData?.Any() ?? false)
            {
                foreach (var xmlLineData in xmlLinesData)
                {
                    if (xmlLineData.Name == "h3")
                        key = xmlLineData;

                    if (key != null && xmlLineData.Name == "div")
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
                                    result.Add(transportType.Value, xmlLineData);
                                    break;
                                }
                            }
                        }

                        key = null;
                    }
                }
            }

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Extract messages node from sections. </summary>
        /// <param name="xmlSections"> XML sections data. </param>
        /// <returns> Messages data node. </returns>
        private XElement? GetMessagesDataNode(List<XElement> xmlSections)
        {
            var xmlMessagesData = xmlSections?
                .FirstOrDefault(s
                    => s.Elements().Any(e => e.Name == "h3")
                    && s.Elements().Any(e => e.Name == "div"))?
                .Elements()
                    .Where(e => e.Name == "table")
                .FirstOrDefault();

            return xmlMessagesData;
        }

        #endregion PROCESS DATA METHODS

        #region READ LINES DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Read lines data. </summary>
        /// <param name="grouppedNodes"> Groupped XML lines data by TransportType. </param>
        /// <returns> Groupped line objects by TransportType. </returns>
        private Dictionary<TransportType, List<Line>> ReadLinesData(Dictionary<TransportType, XElement> grouppedNodes)
        {
            var result = new Dictionary<TransportType, List<Line>>();

            foreach (var kvp in grouppedNodes)
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
        /// <param name="xmlLineNode"> Line XML data node. </param>
        /// <returns> Line object. </returns>
        private Line ReadLineData(TransportType transportType, XElement xmlLineNode)
        {
            var attributes = xmlLineNode.Attribute("class")?.Value;

            var line = new Line()
            {
                TransportType = transportType,
                URL = xmlLineNode.Attribute("href")?.Value,
                Value = xmlLineNode.Value.Trim()
            };

            if (!string.IsNullOrEmpty(attributes))
                line.Attributes = attributes.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            return line;
        }

        #endregion READ LINES DATA

        #region READ MESSAGES DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Read messages data. </summary>
        /// <param name="xmlMessagesNode"> XML messages data node. </param>
        /// <returns> List of messages objects. </returns>
        private List<Message> ReadMessagesData(XElement xmlMessagesNode)
        {
            var result = new List<Message>();
            var rows = xmlMessagesNode.Descendants("tr");

            foreach (var row in rows)
            {
                //  Ignore header.
                if (row.Descendants("th").Any())
                    continue;

                var message = ReadMessageData(row);

                if (message != null)
                    result.Add(message);
            }

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line data. </summary>
        /// <param name="row"> XML message row data node. </param>
        /// <returns> Message object. </returns>
        private Message ReadMessageData(XElement row)
        {
            var columns = row.Elements("td").ToList();

            var message = new Message()
            {
                Date = columns.Count() > 0 ? columns[0].Value : null
            };

            var linesNode = columns.Count() > 1 ? columns[1].Value : null;

            if (!string.IsNullOrEmpty(linesNode))
            {
                message.Lines = linesNode.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim()).ToList();
            }

            var messageNode = columns.Count() > 2 ? columns[2].Element("a") : null;

            if (messageNode != null)
            {
                message.URL = messageNode.Attribute("href")?.Value;
                message.Value = messageNode.Value.Trim();
            }

            return message;
        }

        #endregion READ MESSAGES DATA

    }
}
