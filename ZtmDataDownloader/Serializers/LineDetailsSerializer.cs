using DownloaderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Utilities;

namespace ZtmDataDownloader.Serializers
{
    public class LineDetailsSerializer : BaseSerializer<LineDetailsResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "<div class=\"lineList\">";
        private static readonly string _specialTTStartTrimmer = "<div class=\"col-xs-12\">";
        private static readonly string _endTrimmer = "</section>";


        //  VARIABLES

        private TransportType _transportType;


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
        /// <summary> Deserialize raw line data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized line data from web response. </returns>
        public override LineDetailsResponseModel Deserialize(string rawData, params object[] parameters)
        {
            LoadParameters(parameters);

            var result = new LineDetailsResponseModel();
            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                if (DetectSpecifiedTimeTables(xmlData))
                {
                    result.HasSpecialTimeTables = true;
                    return result;
                }

                var line = ReadData(xmlData);

                if (line != null)
                    result.Line = line;
            }

            if (_errorMessages.Any())
                _errorMessages.ForEach(e => result.AddError(e));

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read parameters from deserialization method. </summary>
        /// <param name="parameters"> Parameters array. </param>
        private void LoadParameters(params object[] parameters)
        {
            if (parameters != null && parameters.Any())
            {
                //  Read transport type parameter.
                var transportType = parameters.Where(p => p.GetType() == typeof(TransportType)).FirstOrDefault();

                if (transportType != null)
                    _transportType = (TransportType)transportType;
            }
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
                .Replace("&nbsp;", "")
                .Replace("<hr>", "<hr />");

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
            int specialTTstartIndex = lines.IndexOf(lines.FirstOrDefault(l => l.Contains(_specialTTStartTrimmer)));
            int corrStartIndex = startIndex > 0 ? startIndex : specialTTstartIndex;

            int endIndex = lines.IndexOf(lines.FirstOrDefault(l => lines.IndexOf(l) > corrStartIndex && l.Contains(_endTrimmer)));

            List<string> dataLines = lines.GetRange(corrStartIndex, endIndex - corrStartIndex);
            CorrectInvalidLines(dataLines);

            return string.Join(string.Empty, dataLines);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Correct lines contains invalid data. </summary>
        /// <param name="dataLines"> Raw data as lines. </param>
        private void CorrectInvalidLines(List<string> dataLines)
        {
            var imgLine = dataLines.Where(l => l.Contains("img")).FirstOrDefault();
            if (imgLine != null)
                dataLines[dataLines.IndexOf(imgLine)] += "</img>";
        }

        #endregion PREPROCESSING DATA METHODS

        #region READ DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line data from XML data node. </summary>
        /// <param name="xmlData"> XML line data node. </param>
        /// <returns> Line data object. </returns>
        private LineDetails ReadData(XElement xmlData)
        {
            LineDetails result = new LineDetails()
            {
                TransportType = _transportType
            };

            FillLineMetadata(xmlData, result);

            result.Messages = ReadLineMessages(xmlData);
            result.Directions = ReadLineDirections(xmlData);

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fill line metadata from XML data node. </summary>
        /// <param name="xmlData"> XML line data node. </param>
        /// <param name="line"> Result line data. </param>
        private void FillLineMetadata(XElement xmlData, LineDetails line)
        {
            XElement metadataNode = xmlData.Descendants("div")
                .FirstOrDefault(n => n.Attributes()
                    .Any(a => a.Name == "class" && a.Value == "clearfix"));

            if (metadataNode != null)
            {
                line.Name = ReadLineName(metadataNode) ?? "";
                line.Description = ReadLineDescription(metadataNode) ?? "";
                line.Information = ReadLineInfo(metadataNode) ?? "";
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line name. </summary>
        /// <param name="metadataNode"> XML line metadata node. </param>
        /// <returns> Line name. </returns>
        private string ReadLineName(XElement metadataNode)
        {
            return metadataNode.Descendants("a")
                .FirstOrDefault(n => !n.HasElements)?.Value.Trim();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line description. </summary>
        /// <param name="metadataNode"> XML line metadata node. </param>
        /// <returns> Line description. </returns>
        private string ReadLineDescription(XElement metadataNode)
        {
            return metadataNode.Descendants("strong")
                    .FirstOrDefault(n => n.Attribute("class")?.Value == "sr-only")?.Value.Trim();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line informations. </summary>
        /// <param name="metadataNode"> XML line metadata node. </param>
        /// <returns> Line informations. </returns>
        private string ReadLineInfo(XElement metadataNode)
        {
            return metadataNode.Descendants("small")
                    .FirstOrDefault(n => !n.HasElements)?.Value.Trim();
        }

        #endregion READ DATA METHODS

        #region READ MESSAGES DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line messages from XML data node. </summary>
        /// <param name="xmlData"> XML line data node. </param>
        /// <returns> List of Message objects. </returns>
        private List<Message> ReadLineMessages(XElement xmlData)
        {
            List<Message> result = new List<Message>();
            XElement messagesNode = GetMessagesDataNode(xmlData);

            if (messagesNode != null)
            {
                foreach (XElement messageNode in messagesNode.Descendants("tbody").Descendants("tr"))
                {
                    Message msg = ReadMessage(messageNode);

                    if (msg != null)
                        result.Add(msg);
                }
            }

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML data node where messages are storred. </summary>
        /// <param name="xmlData"> XML line data node. </param>
        /// <returns> XML data node where messages are storred. </returns>
        private XElement GetMessagesDataNode(XElement xmlData)
        {
            XElement communicationsNode = xmlData.Descendants("div")
                .FirstOrDefault(n => n.Attributes()
                    .Any(a => a.Name == "id" && a.Value == "lineTypeScrollspy"));

            if (communicationsNode != null)
            {
                return xmlData.Descendants("table")
                    .FirstOrDefault(nt => nt.Attributes()
                        .Any(a => a.Name == "class" && a.Value.Contains("table")));
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read single message data from XML message node. </summary>
        /// <param name="messageNode"> XML message data node. </param>
        /// <returns> Message data object. </returns>
        private Message ReadMessage(XElement messageNode)
        {
            Message msg = new Message();

            msg.URL = ReadMessageURL(messageNode);
            msg.MessageText = ReadMessageText(messageNode, msg.URL);

            FillMessageDates(messageNode, msg);

            return msg;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read message URL. </summary>
        /// <param name="messageNode"> XML message data node. </param>
        /// <returns> Message URL. </returns>
        private string ReadMessageURL(XElement messageNode)
        {
            XElement anyAHref = messageNode.Descendants("a")
                .FirstOrDefault(n => n.Attributes()
                    .Any(a => a.Name == "href" && !string.IsNullOrEmpty(a.Value)));

            if (anyAHref != null)
                return anyAHref.Attribute("href").Value;

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read message locations. </summary>
        /// <param name="messageNode"> XML message data node. </param>
        /// <param name="lineURL"> Message URL. </returns>
        /// <returns> List of message locations. </returns>
        private List<string> ReadMessageLocations(XElement messageNode, string lineURL)
        {
            List<XElement> aHrefs = messageNode.Descendants("a")
                .Where(n =>
                    n.Attributes()
                        .Any(a => a.Name == "href" && a.Value == lineURL)
                    && !string.IsNullOrEmpty(n.Value))
                .ToList();

            if (aHrefs != null && aHrefs.Count > 1)
                return aHrefs[0].Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => v.Trim()).ToList();

            return new List<string>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read message text. </summary>
        /// <param name="messageNode"> XML message data node. </param>
        /// <param name="lineURL"> Message URL. </returns>
        /// <returns> Message text. </returns>
        private string ReadMessageText(XElement messageNode, string lineURL)
        {
            List<XElement> aHrefs = messageNode.Descendants("a")
                .Where(n =>
                    n.Attributes()
                        .Any(a => a.Name == "href" && a.Value == lineURL)
                    && !string.IsNullOrEmpty(n.Value))
                .ToList();

            if (aHrefs != null && aHrefs.Count > 0)
            {
                return string.Join(" ", aHrefs.Select(a => a.Value.Trim()));
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fill message dates. </summary>
        /// <param name="messageNode"> XML message data node. </param>
        /// <param name="message"> Message data object to fill. </param>
        private void FillMessageDates(XElement messageNode, Message message)
        {
            var dateInformations = messageNode.Descendants("td")
                .Where(n =>
                    n.Attributes()
                        .Any(a => a.Name == "class" && a.Value.Contains("text-right")))
                .Select(n =>
                    n.Descendants("small")
                        .FirstOrDefault()?.Value)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(dateInformations))
            {
                var dateParts = dateInformations.Trim().Split(new string[] { "od: ", "do: ", ": " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                switch (dateParts.Count)
                {
                    case 2:
                        message.DateDescription = dateParts[0]?.Trim();
                        message.SetStartDate(dateParts[1]?.Trim());
                        break;
                    case 3:
                        message.DateDescription = dateParts[0]?.Trim();
                        message.SetStartDate(dateParts[1]?.Trim());
                        message.SetEndDate(dateParts[2]?.Trim());
                        break;
                }
            }
        }

        #endregion READ MESSAGES DATA METHODS

        #region READ LINE STOPS DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line directions with stops from XML data node. </summary>
        /// <param name="xmlData"> XML line data node. </param>
        /// <returns> List of LineDirection objects. </returns>
        private List<LineDirection> ReadLineDirections(XElement xmlData)
        {
            List<LineDirection> directions = new List<LineDirection>();
            List<XElement> directionNodes = GetLineDirectionsDataNodes(xmlData);

            if (directionNodes != null && directionNodes.Any())
            {
                foreach (XElement directionNode in directionNodes)
                {
                    LineDirection direction = ReadLineDirection(directionNode);

                    if (direction != null)
                        directions.Add(direction);
                }
            }

            return directions;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML data nodes where line directions are storred. </summary>
        /// <param name="xmlData"> XML line data node. </param>
        /// <returns> List of XML data nodes where line directions are storred. </returns>
        private List<XElement> GetLineDirectionsDataNodes(XElement xmlData)
        {
            return xmlData.Descendants("div")
                .Where(n => n.Attributes()
                    .Any(a => a.Name == "class"
                        && a.Value.Contains("list-group")
                        && a.Value.Contains("col-xs-12")
                        && a.Value.Contains("col-sm-6")))
                .ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read single line direction data from XML message node. </summary>
        /// <param name="directionNode"> XML line direction data node. </param>
        /// <returns> LineDirection data object. </returns>
        private LineDirection ReadLineDirection(XElement directionNode)
        {
            LineDirection direction = new LineDirection();

            direction.Cities = ReadCities(directionNode);
            direction.Direction = ReadLineDirectionTitle(directionNode);
            direction.Stops = ReadLineStops(directionNode, direction.Cities);

            return direction;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line direction title. </summary>
        /// <param name="directionNode"> XML line direction data node. </param>
        /// <returns> Line direction title. </returns>
        private string ReadLineDirectionTitle(XElement directionNode)
        {
            XElement directionTitleNode = directionNode.Descendants("div")
                .FirstOrDefault(n => n.Attributes()
                    .Any(a => a.Name == "class"
                        && a.Value.Contains("list-group-item")
                        && a.Value.Contains("list-group-item-warning")));

            string fullDirectionName = directionTitleNode.Value.Trim();
            string directionName = directionTitleNode.Descendants("strong")
                .FirstOrDefault()?.Value;

            return directionName;
        }

        #endregion READ LINE STOPS DATA METHODS

        #region READ LINE CITIES DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line cities from line direction XML data node. </summary>
        /// <param name="directionNode"> XML line direction data node. </param>
        /// <returns> List of City objects. </returns>
        private List<City> ReadCities(XElement directionNode)
        {
            List<City> cities = new List<City>();
            List<XElement> citiesNodes = GetLineCitiesDataNodes(directionNode);

            if (citiesNodes != null && citiesNodes.Any())
            {
                foreach (XElement cityNode in citiesNodes)
                {
                    City city = ReadCity(cityNode);

                    if (city != null)
                        cities.Add(city);
                }
            }

            return cities;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML data nodes where cities are storred. </summary>
        /// <param name="directionNode"> XML line direction data node. </param>
        /// <returns> List of XML data nodes where cities are storred. </returns>
        private List<XElement> GetLineCitiesDataNodes(XElement directionNode)
        {
            XElement directionCitiesNode = directionNode.Descendants("div")
                .FirstOrDefault(n => n.Elements()
                    .Any(a => a.Name == "ul"));

            if (directionCitiesNode != null)
                return directionCitiesNode.Descendants("li").ToList();

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read single city data from XML city node. </summary>
        /// <param name="cityNode"> XML city data node. </param>
        /// <returns> City data object. </returns>
        private City ReadCity(XElement cityNode)
        {
            City city = new City();

            city.Color = ReadCityColor(cityNode);
            city.Name = ReadCityName(cityNode);

            return city;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read city color. </summary>
        /// <param name="cityNode"> XML city data node. </param>
        /// <returns> City color. </returns>
        private string ReadCityColor(XElement cityNode)
        {
            return GetLineStopBorderColor(cityNode.Attribute("style").Value.Trim(), "border-left");
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read city name. </summary>
        /// <param name="cityNode"> XML city data node. </param>
        /// <returns> City name. </returns>
        private string ReadCityName(XElement cityNode)
        {
            return cityNode.Value?.Trim();
        }

        #endregion READ LINE CITIES DATA METHODS

        #region READ LINE DIRECTION STOPS DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line direction stops from XML data node. </summary>
        /// <param name="directionNode"> XML line direction data node. </param>
        /// <param name="cities"> List of Cities data objects. </param>
        /// <returns> List of LineStop objects. </returns>
        private List<LineStop> ReadLineStops(XElement directionNode, List<City> cities)
        {
            List<LineStop> stops = new List<LineStop>();
            List<XElement> stopsNodes = GetLineStopsDataNodes(directionNode);

            if (stopsNodes != null && stopsNodes.Any())
            {
                foreach (XElement stopNode in stopsNodes)
                {
                    LineStop lineStop = ReadLineStop(stopNode);

                    if (lineStop != null)
                    {
                        lineStop.MapCity(cities);
                        stops.Add(lineStop);
                    }
                }
            }

            return stops;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML data nodes where line direction stops are storred. </summary>
        /// <param name="xmlData"> XML line direction data node. </param>
        /// <returns> List of XML data nodes where line direction stops are storred. </returns>
        private List<XElement> GetLineStopsDataNodes(XElement directionNode)
        {
            return directionNode.Descendants("a")
                .Where(n => n.Attributes()
                    .Any(a => a.Name == "class"
                        && a.Value.Contains("direction-list-group-item")
                        && a.Value.Contains("list-group-item")))
                .ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read single LineStop data from XML data node. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <returns> LineStop data object. </returns>
        private LineStop ReadLineStop(XElement lineStopNode)
        {
            LineStop lineStop = new LineStop();
            XElement optionalStopNode = GetLineStopOptionalDataNode(lineStopNode);

            lineStop.Attributes = ReadLineStopAttributes(lineStopNode);
            lineStop.Color = ReadLineStopColor(lineStopNode);
            lineStop.Name = ReadLineStopName(lineStopNode, optionalStopNode);
            lineStop.Optional = optionalStopNode != null;
            lineStop.Platform = ReadLineStopPlatform(lineStopNode, optionalStopNode);
            lineStop.PlatformDescription = ReadLineStopPlatformDescription(lineStopNode, optionalStopNode);
            lineStop.URL = ReadLineStopURL(lineStopNode);

            if (lineStop.Name.Contains(lineStop.Platform))
                lineStop.Name = lineStop.Name.Replace(lineStop.Platform, "").Trim();

            return lineStop;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML optional line direction stop data node. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <returns> XML optional line direction stop data node. </returns>
        private XElement GetLineStopOptionalDataNode(XElement lineStopNode)
        {
            return lineStopNode.Descendants("i")
                .FirstOrDefault(n => !n.Attributes().Any());
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line stop attributes. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <returns> Line stop attributes. </returns>
        private List<string> ReadLineStopAttributes(XElement lineStopNode)
        {
            return lineStopNode.Attribute("class").Value
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line stop color. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <returns> Line stop color. </returns>
        private string ReadLineStopColor(XElement lineStopNode)
        {
            return GetLineStopBorderColor(lineStopNode.Attribute("style").Value.Trim(), "border-right");
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line stop name. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <param name="optionalStopNode"> XML optional line stop data node. </param>
        /// <returns> Line stop name. </returns>
        private string ReadLineStopName(XElement lineStopNode, XElement optionalStopNode)
        {
            return (optionalStopNode ?? lineStopNode).Value.Trim();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line stop platform. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <param name="optionalStopNode"> XML optional line stop data node. </param>
        /// <returns> Line stop platform. </returns>
        private string ReadLineStopPlatform(XElement lineStopNode, XElement optionalStopNode)
        {
            var abbrNode = (optionalStopNode ?? lineStopNode).Descendants("abbr")
                .FirstOrDefault(n => !string.IsNullOrEmpty(n.Value));

            return abbrNode != null ? abbrNode.Value.Trim() : "";
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line stop platform description. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <param name="optionalStopNode"> XML optional line stop data node. </param>
        /// <returns> Line stop platform description. </returns>
        private string ReadLineStopPlatformDescription(XElement lineStopNode, XElement optionalStopNode)
        {
            var abbrNode = (optionalStopNode ?? lineStopNode).Descendants("abbr")
                .FirstOrDefault(n => n.Attributes()
                    .Any(a => a.Name == "title"));

            return abbrNode != null ? abbrNode.Attribute("title").Value.Trim() : "";
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line stop URL. </summary>
        /// <param name="lineStopNode"> XML line stop data node. </param>
        /// <returns> Line stop URL. </returns>
        private string ReadLineStopURL(XElement lineStopNode)
        {
            return lineStopNode.Attribute("href").Value.Trim();
        }

        #endregion READ LINE DIRECTION STOPS DATA METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Detect if line contains special time tables. </summary>
        /// <param name="xmlData"> XML data structure. </param>
        /// <returns> True - Line contains special time tables; False - otherwise. </returns>
        private bool DetectSpecifiedTimeTables(XElement serializedData)
        {
            return serializedData.Attribute("class").Value.Contains("col-xs-12");
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get line stop border color from style attribute in XML line data. </summary>
        /// <param name="style"> XML style data. </param>
        /// <param name="style_key"> Style key. </param>
        /// <returns> Color in hex string. </returns>
        private string GetLineStopBorderColor(string style, string style_key)
        {
            var styleOptions = style.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList()
                .Select(v => v.Split(new string[] { ":" }, StringSplitOptions.None))
                    .ToList()
                    .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            if (styleOptions.Any(o => o.Key == style_key))
                return styleOptions[style_key].Trim().Split(new string[] { " " }, StringSplitOptions.None).LastOrDefault();

            return "";
        }

        #endregion UTILITY METHODS

    }
}
