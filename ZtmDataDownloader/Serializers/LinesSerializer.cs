using DownloaderCore;
using DownloaderCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Mappers;

namespace ZtmDataDownloader.Serializers
{
    public class LinesSerializer : BaseSerializer<LinesResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "col-xs-12 col-md-9";
        private static readonly string _endTrimmer = "col-md-3 visible-md visible-lg";

        private static readonly List<string> _invalidDataInLines = new List<string>()
        {
            "<!---->"
        };


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
                var groupedDataNodes = GrupDataByTransportType(xmlData);
                var linesData = ReadData(groupedDataNodes);

                if (linesData.Any())
                    result.Lines = linesData;
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
            RemoveInvalidLines(dataLines);

            return string.Join(string.Empty, dataLines);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Remove lines contains invalid data. </summary>
        /// <param name="dataLines"> Raw data as lines. </param>
        private void RemoveInvalidLines(List<string> dataLines)
        {
            foreach (var invalidData in _invalidDataInLines)
                dataLines.RemoveAll(l => l.Contains(invalidData));
        }

        #endregion PREPROCESSING DATA METHODS

        #region PROCESS DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Group xml data by transport type. </summary>
        /// <param name="xmlData"> XML data structure. </param>
        /// <returns> XML data grouped by transport type. </returns>
        private Dictionary<TransportType, XElement> GrupDataByTransportType(XElement xmlData)
        {
            var result = new Dictionary<TransportType, XElement>();
            var transportTypes = TransportTypesMapper.GetTypes();

            foreach (var transportType in transportTypes)
            {
                XElement transportTypeDataNode = GetTransportTypeXmlDataNode(xmlData, transportType);

                if (transportTypeDataNode != null)
                    result.Add(transportType, transportTypeDataNode);
            }

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Separate XML data of specific transport type. </summary>
        /// <param name="xmlData"> XML data structure. </param>
        /// <param name="transportType"> Transport type to separate. </param>
        /// <returns> XML data node of sepcific transport type. </returns>
        private XElement GetTransportTypeXmlDataNode(XElement xmlData, TransportType transportType)
        {
            string strongName = TransportTypesMapper.MapToNodeStrongName(transportType);
            string valueName = TransportTypesMapper.MapToNodeValue(transportType);

            return xmlData.Descendants("div")
                .FirstOrDefault(n =>
                    n.Attributes()
                        .Any(a => a.Name == "class" && a.Value == "panel panel-default")
                    && n.Descendants("h2")
                        .Any(nt =>
                            nt.Attributes()
                                .Any(nta => nta.Name == "class" && nta.Value == "panel-title")
                            && nt.Descendants("strong")
                                .Any(ns => ns.Value == strongName)
                    && nt.Value.Contains(valueName)));
        }

        #endregion PROCESS DATA METHODS

        #region READ DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read lines data from groupped XML lines data nodes. </summary>
        /// <param name="groupedDataNodes"> Grouped XML lines data nodes. </param>
        /// <returns> Groupped lines data objects by TransportType. </returns>
        private Dictionary<TransportType, List<Line>> ReadData(Dictionary<TransportType, XElement> groupedDataNodes)
        {
            var result = new Dictionary<TransportType, List<Line>>();

            if (groupedDataNodes != null && groupedDataNodes.Any())
                foreach (var kvp in groupedDataNodes)
                {
                    var transportType = kvp.Key;
                    var dataNode = kvp.Value;

                    var linesDataNodes = dataNode.Descendants("a")
                        .Where(cn =>
                            cn.Attributes()
                                .Any(a => a.Name == "class" && a.Value.Contains("btn")));

                    if (linesDataNodes != null && linesDataNodes.Any())
                    {
                        var lines = ReadLinesData(linesDataNodes, transportType);

                        if (lines.Any())
                            result.Add(transportType, lines);
                    }
                }

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read lines data from xml data nodes of specific transport type group. </summary>
        /// <param name="linesDataNodes"> List of XML lines data nodes for specific transport type group. </param>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> List of lines items. </returns>
        private List<Line> ReadLinesData(IEnumerable<XElement> linesDataNodes, TransportType transportType)
        {
            var result = new List<Line>();

            foreach (var lineDataNode in linesDataNodes)
                result.Add(ReadSingleLineData(lineDataNode, transportType));

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line data from XML data node. </summary>
        /// <param name="lineDataNode"> XML line data node. </param>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Line item. </returns>
        private Line ReadSingleLineData(XElement lineDataNode, TransportType transportType)
        {
            return new Line()
            {
                Attributes = ReadLineAttributes(lineDataNode),
                Color = ReadLineColorAttribute(lineDataNode),
                Description = lineDataNode.Attribute("title")?.Value,
                Type = transportType,
                URL = lineDataNode.Attribute("href")?.Value,
                Value = lineDataNode.Value
            };
        }

        #endregion READ DATA METHODS

        #region READ LINE DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line attributes. </summary>
        /// <param name="lineDataNode"> XML line data node. </param>
        /// <returns> List of line attributes. </returns>
        private List<string> ReadLineAttributes(XElement lineDataNode)
        {
            if (lineDataNode.Attributes().Any(a => a.Name == "class"))
            {
                var attributes = lineDataNode.Attribute("class");

                if (!string.IsNullOrEmpty(attributes?.Value))
                    return attributes.Value.Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).ToList();
            }

            return new List<string>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line color. </summary>
        /// <param name="lineDataNode"> XML line data node. </param>
        /// <returns> Line color. </returns>
        private string ReadLineColorAttribute(XElement lineDataNode)
        {
            if (lineDataNode.Attributes().Any(a => a.Name == "style" && a.Value.Contains("background")))
            {
                var attributes = lineDataNode.Attribute("style").Value.Split(';');

                if (attributes.Any(a => a.Contains("background")))
                    return attributes
                        .Where(a => a.Contains("background"))
                        .FirstOrDefault()
                        .Split(new string[] { ": " }, StringSplitOptions.None)[1];
            }

            return null;
        }

        #endregion READ LINE DATA METHODS

    }
}
