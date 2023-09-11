using DownloaderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZtmDataDownloader.Data.Arrivals;
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Utilities;

namespace ZtmDataDownloader.Serializers
{
    public class ArrivalsSerializer : BaseSerializer<ArrivalResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "<section>";
        private static readonly string _endTrimmer = "</section>";


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalsSerializer class constructor. </summary>
        public ArrivalsSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw arrivals data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized arrivals data from web response. </returns>
        public override ArrivalResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new ArrivalResponseModel();
            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                List<XElement> separatedDataNodes = SeparateDataNodes(xmlData);

                if (separatedDataNodes != null && separatedDataNodes.Any())
                {
                    XElement informationsDataNode = GetInformationsDataNode(separatedDataNodes);
                    List<XElement> arrivalsDataNodes = GetArrivalsDataNodes(separatedDataNodes);
                    XElement detailsDataNode = GetDetailsDataNode(separatedDataNodes);

                    var description = ReadDescription(informationsDataNode);
                    var infos = ReadInformationsListData(informationsDataNode);
                    var arrivals = ReadArrivalsData(arrivalsDataNodes);
                    var cities = ReadCities(detailsDataNode);
                    var informations = ReadInformationsData(detailsDataNode);

                    if (arrivals != null && arrivals.Any())
                        result.DepartureDetails = new DepartureDetails()
                        {
                            Description = description,
                            Infos = infos,
                            Arrivals = arrivals,
                            Cities = cities,
                            Informations = informations
                        };
                }
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
                .Replace("&nbsp;", "")
                .Replace("<br>", "<br />")
                .Replace("<hr>", "<hr />");

            trimmedData = StringUtils.RemoveExcessSpaces(trimmedData);
            trimmedData = AdvancePreprocessData(trimmedData);

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

            List<string> dataLines = lines.GetRange(startIndex, (endIndex - startIndex) + 1);
            CorrectInvalidLines(dataLines);

            return string.Join("\n", dataLines);
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

        //  --------------------------------------------------------------------------------
        /// <summary> Advance preprocess trimmed data, correcting table. </summary>
        /// <param name="trimmedData"> Trimmed raw data. </param>
        /// <returns> Preprocessed trimmed data. </returns>
        private string AdvancePreprocessData(string trimmedData)
        {
            List<string> preprocessedLines = trimmedData.Split(new string[] { Environment.NewLine, "\n" },
                StringSplitOptions.RemoveEmptyEntries).ToList();

            int tdCounter = 0;

            for (int lineIndex = 0; lineIndex < preprocessedLines.Count; lineIndex++)
            {
                string line = preprocessedLines[lineIndex];

                if (preprocessedLines[lineIndex].Contains("<td"))
                {
                    if (tdCounter > 0)
                    {
                        preprocessedLines.Insert(lineIndex, "</td>");
                        tdCounter = tdCounter - 1;
                        continue;
                    }
                    else
                    {
                        tdCounter = tdCounter + 1;
                    }
                }

                if (preprocessedLines[lineIndex].Contains("</td"))
                {
                    tdCounter = tdCounter - 1;
                }
            }

            return string.Join("\n", preprocessedLines);
        }

        #endregion PREPROCESSING DATA METHODS

        #region PROCESS DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Separate XML data nodes with arrivals data. </summary>
        /// <param name="xmlData"> XML arrivals data. </param>
        /// <returns> List of separated XML arrivals data nodes. </returns>
        private List<XElement> SeparateDataNodes(XElement xmlData)
        {
            return xmlData.Descendants("div")
                .Where(n => n.Attributes()
                    .Any(a => a.Name == "class" && a.Value == "list-group"))
                .FirstOrDefault()
                ?.Elements()
                .ToList();
        }

        #endregion PROCESS DATA METHODS

        #region READ INFORMATIONS DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Get informations XML data node from separated XML data nodes. </summary>
        /// <param name="separatedDataNodes"> Separated XML arrivals data nodes. </param>
        /// <returns> XML informations data node. </returns>
        private XElement GetInformationsDataNode(List<XElement> separatedDataNodes)
        {
            return separatedDataNodes
                .FirstOrDefault(n => n.Name == "div"
                    && n.Attributes()
                        .Any(a => a.Name == "class" && a.Value.Contains("list-group-item")));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read description from XML data node. </summary>
        /// <param name="xmlData"> Informations XML data node. </param>
        /// <returns> Description. </returns>
        private string ReadDescription(XElement informationsDataNode)
        {
            return informationsDataNode.Value?.Trim();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read informations from XML data node. </summary>
        /// <param name="xmlData"> Informations XML data node. </param>
        /// <returns> List of informations from XML data nodes. </returns>
        private List<string> ReadInformationsListData(XElement informationsDataNode)
        {
            List<string> headerInfos = informationsDataNode
                .Descendants("strong")
                .Select(n => n.Value?.Trim())
                .ToList();

            return headerInfos != null && headerInfos.Any()
                ? headerInfos
                : new List<string>();
        }

        #endregion READ INFORMATIONS DATA

        #region READ ARRIVALS DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrivals XML data node from separated XML data nodes. </summary>
        /// <param name="separatedDataNodes"> Separated XML arrivals data nodes. </param>
        /// <returns> XML arrivals data nodes. </returns>
        private List<XElement> GetArrivalsDataNodes(List<XElement> separatedDataNodes)
        {
            var arrivalsDataNode = separatedDataNodes
                .FirstOrDefault(n => n.Name == "table"
                    && n.Attributes()
                        .Any(a => a.Name == "class" && a.Value.Contains("table")));

            if (arrivalsDataNode != null)
                return arrivalsDataNode.Descendants("tbody")
                    .FirstOrDefault()
                    ?.Descendants("tr")
                        .Where(n => n.Attributes()
                            .Any(a => a.Name == "class"))
                        .ToList();

            return new List<XElement>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read arrivals data from arrivals XML data nodes. </summary>
        /// <param name="arrivalsDataNodes"> Arrivals XML data nodes. </param>
        /// <returns> Arrivals data objects. </returns>
        private List<Arrival> ReadArrivalsData(List<XElement> arrivalsDataNodes)
        {
            var arrivals = new List<Arrival>();

            if (arrivalsDataNodes != null && arrivalsDataNodes.Any())
            {
                foreach (var arrivalDataNode in arrivalsDataNodes)
                {
                    var arrival = ReadSingleArrivalData(arrivalDataNode);

                    if (arrival != null)
                        arrivals.Add(arrival);
                }
            }

            return arrivals;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read single arrival data from XML arrival data node. </summary>
        /// <param name="arrivalDataNode"> XML arrival data node. </param>
        /// <returns> Arrival data object. </returns>
        private Arrival ReadSingleArrivalData(XElement arrivalDataNode)
        {
            List<XElement> arrivalsTableDataNodes = GetArrivalsTableDataNodes(arrivalDataNode);
            XElement arrivalTitleDataNode = GetArrivalTitleDataNode(arrivalsTableDataNodes);
            XElement arrivalInfoDataNode = GetArrivalInfoDataNode(arrivalsTableDataNodes);

            var (hour, minute) = arrivalsTableDataNodes.Count() > 1
                ? GetArrivalTimeData(arrivalsTableDataNodes[1])
                : (-1, -1);

            var (fullTime, partTime) = arrivalsTableDataNodes.Count() > 2
                ? GetArrivalAdditionalTimeData(arrivalsTableDataNodes[2])
                : (null, null);

            var (fullDistance, partDistance) = arrivalsTableDataNodes.Count() > 3
                ? GetArrivalAdditionalDistanceData(arrivalsTableDataNodes[3])
                : (null, null);

            Arrival arrival = new Arrival()
            {
                Name = ReadArrivalName(arrivalTitleDataNode),
                Links = ReadArrivalLinks(arrivalTitleDataNode),
                Color = GetArrivalColor(arrivalInfoDataNode),
                AdditionalInfo = GetArrivalAdditionalInformations(arrivalInfoDataNode),
                Hour = Math.Max(0, hour),
                Minute = Math.Max(0, minute),
                FullTime = fullTime,
                PartTime = partTime,
                FullDistance = fullDistance,
                PartDistance = partDistance
            };

            return arrival;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival table items XML data nodes from arrival XML data node. </summary>
        /// <param name="separatedDataNodes"> XML arrival data node. </param>
        /// <returns> Arrival table items XML data nodes. </returns>
        private List<XElement> GetArrivalsTableDataNodes(XElement arrivalDataNode)
        {
            return arrivalDataNode.Descendants("td").ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival title XML data node. </summary>
        /// <param name="arrivalsTableDataNodes"> Arrival table items XML data nodes. </param>
        /// <returns> Arrival title XML data node. </returns>
        private XElement GetArrivalTitleDataNode(List<XElement> arrivalsTableDataNodes)
        {
            return arrivalsTableDataNodes
                .FirstOrDefault(n => n.Elements()
                    .Any(e => e.Name == "div" && e.Attributes()
                        .Any(a => a.Name == "class" && a.Value == "dropdown")))
                ?.Elements()
                    .FirstOrDefault();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival info XML data node. </summary>
        /// <param name="arrivalsTableDataNodes"> Arrival table items XML data nodes. </param>
        /// <returns> Arrival info XML data node. </returns>
        private XElement GetArrivalInfoDataNode(List<XElement> arrivalsTableDataNodes)
        {
            return arrivalsTableDataNodes
                .FirstOrDefault(n => n.Attributes()
                    .Any(a => a.Name == "style" && a.Value.Contains("border-left")));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read arrival name from XML arrival title data node. </summary>
        /// <param name="arrivalTitleDataNode"> XML arrival title data node. </param>
        /// <returns> Arrival name. </returns>
        private string ReadArrivalName(XElement arrivalTitleDataNode)
        {
            if (arrivalTitleDataNode != null)
            {
                XElement titleDataNode = arrivalTitleDataNode.Descendants("a")
                    .Where(n => n.Attributes()
                        .Any(a => a.Name == "class" && a.Value.Contains("dropdown-toggle")))
                    .FirstOrDefault();

                return titleDataNode?.Value?.Trim();
            }

            return string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read arrival links data from XML arrival title data node. </summary>
        /// <param name="arrivalTitleDataNode"> XML arrival title data node. </param>
        /// <returns> Arrival links data objects. </returns>
        private List<ArrivalLink> ReadArrivalLinks(XElement arrivalTitleDataNode)
        {
            List<ArrivalLink> links = new List<ArrivalLink>();
            List<XElement> arrivalLinksDataNodes = GetArrivalLinksDataNodes(arrivalTitleDataNode);

            if (arrivalLinksDataNodes != null && arrivalLinksDataNodes.Any())
            {
                foreach (var arrivalLinkDataNode in arrivalLinksDataNodes)
                {
                    ArrivalLink link = ReadArrivalLink(arrivalLinkDataNode);

                    if (link != null)
                        links.Add(link);
                }
            }

            return links;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival links XML data nodes. </summary>
        /// <param name="arrivalTitleDataNode"> XML arrival title data node. </param>
        /// <returns> Arrival links XML data objects. </returns>
        private List<XElement> GetArrivalLinksDataNodes(XElement arrivalTitleDataNode)
        {
            return arrivalTitleDataNode.Descendants("li")
                .Where(n => n.Elements().Any(e => e.Name == "a"))
                .Select(l => l.Elements("a").FirstOrDefault())
                .ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read arrival link data from XML arrival link data node. </summary>
        /// <param name="arrivalLinkDataNode"> XML arrival link data node. </param>
        /// <returns> Arrival link data object. </returns>
        private ArrivalLink ReadArrivalLink(XElement arrivalLinkDataNode)
        {
            var title = arrivalLinkDataNode?.Value?.Trim();
            var url = arrivalLinkDataNode?.Attribute("href")?.Value;

            return new ArrivalLink()
            {
                Title = title,
                URL = url
            };
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival color from XML arrival info data node. </summary>
        /// <param name="arrivalInfoDataNode"> XML arrival info data node. </param>
        /// <returns> Arrival color as string. </returns>
        private string GetArrivalColor(XElement arrivalInfoDataNode)
        {
            if (arrivalInfoDataNode != null)
                return GetLineStopBorderColor(
                    arrivalInfoDataNode.Attribute("style").Value.Trim(), "border-left");

            return string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival additional informations from XML arrival info data node. </summary>
        /// <param name="arrivalInfoDataNode"> XML arrival info data node. </param>
        /// <returns> Arrival additional informations. </returns>
        private string GetArrivalAdditionalInformations(XElement arrivalInfoDataNode)
        {
            return arrivalInfoDataNode != null
                ? arrivalInfoDataNode?.Value?.Trim()
                : string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival time from XML arrival time data node. </summary>
        /// <param name="arrivalTimeDataNode"> XML arrival time data node. </param>
        /// <returns> Tuple (hour, minute) arrival time. </returns>
        private (int, int) GetArrivalTimeData(XElement arrivalTimeDataNode)
        {
            int hour = -1;
            int minute = -1;

            string[] value = arrivalTimeDataNode?.Value?.Trim().Split(
                new string[] { ":" }, StringSplitOptions.None);

            if (value.Length > 1)
            {
                int.TryParse(value[0], out hour);
                int.TryParse(value[1], out minute);
            }

            return (hour, minute);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival additional time from XML arrival additional time data node. </summary>
        /// <param name="arrivalAdditionalTimeDataNode"> XML arrival additional time data node. </param>
        /// <returns> Tuple arrival additional time. </returns>
        private (string, string) GetArrivalAdditionalTimeData(XElement arrivalAdditionalTimeDataNode)
        {
            var part = arrivalAdditionalTimeDataNode.Descendants("small").FirstOrDefault();

            if (part != null)
            {
                string fullValue = arrivalAdditionalTimeDataNode?.Value?.Trim();
                string partValue = part?.Value?.Trim();

                return (
                    SimplifyPartData(fullValue.Replace(partValue, string.Empty).Trim()),
                    SimplifyPartData(partValue));
            }

            return (null, null);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival additional distance from XML arrival additional distance data node. </summary>
        /// <param name="arrivalAdditionalTimeDataNode"> XML arrival additional distance data node. </param>
        /// <returns> Tuple arrival additional distance. </returns>
        private (string, string) GetArrivalAdditionalDistanceData(XElement arrivalAdditionalDistanceDataNode)
        {
            var part = arrivalAdditionalDistanceDataNode.Descendants("small").FirstOrDefault();

            if (part != null)
            {
                string fullValue = arrivalAdditionalDistanceDataNode?.Value?.Trim();
                string partValue = part?.Value?.Trim();

                return (
                    SimplifyPartData(fullValue.Replace(partValue, string.Empty).Trim()),
                    SimplifyPartData(partValue));
            }

            return (null, null);
        }

        #endregion READ ARRIVALS DATA

        #region READ DETAILS DATA

        //  --------------------------------------------------------------------------------
        /// <summary> Get details XML data node from separated XML data nodes. </summary>
        /// <param name="separatedDataNodes"> Separated XML arrivals data nodes. </param>
        /// <returns> XML details data nodes. </returns>
        private XElement GetDetailsDataNode(List<XElement> separatedDataNodes)
        {
            return separatedDataNodes
                .FirstOrDefault(n => n.Name == "div" && !n.Attributes().Any());
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read informations from XML details data node. </summary>
        /// <param name="detailsDataNode"> XML details data node. </param>
        /// <returns> Informations dictionary data object. </returns>
        public Dictionary<string, string> ReadInformationsData(XElement detailsDataNode)
        {
            List<string> rawInformations = GetRawInformationsData(detailsDataNode);
            Dictionary<string, string> informations = new Dictionary<string, string>();
            string infoKey = null;

            foreach (var rawInfo in rawInformations)
            {
                string[] splittedInfo = rawInfo.Split(new string[] { ":", "-" }, StringSplitOptions.RemoveEmptyEntries);

                if (splittedInfo.Length > 1)
                {
                    informations.Add(splittedInfo[0].Trim(), splittedInfo[1].Trim());
                    infoKey = null;
                }
                else if (splittedInfo.Any())
                {
                    if (infoKey == null)
                    {
                        infoKey = splittedInfo[0];
                    }
                    else if (infoKey != null && !rawInfo.Contains(":") && !rawInfo.Contains("-"))
                    {
                        informations.Add(infoKey.Trim(), rawInfo.Trim());
                        infoKey = null;
                    }
                    else if (infoKey != null)
                    {
                        infoKey = splittedInfo[0];
                    }
                }
            }

            return informations;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get raw informations data list from XML details data node. </summary>
        /// <param name="detailsDataNode"> XML details data node. </param>
        /// <returns> List of raw informations data. </returns>
        private List<string> GetRawInformationsData(XElement detailsDataNode)
        {
            XElement ignoreFragment = detailsDataNode.Descendants("ul").FirstOrDefault();

            return detailsDataNode.Value
                .Replace(ignoreFragment?.Value, string.Empty)
                .Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => v.Trim())
                .Where(v => !string.IsNullOrEmpty(v))
                .ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read cities data from XML details data node. </summary>
        /// <param name="detailsDataNode"> XML details data node. </param>
        /// <returns> List of cities data objects. </returns>
        private List<City> ReadCities(XElement detailsDataNode)
        {
            List<City> cities = new List<City>();
            List<XElement> citiesDataNodes = GetCitiesDataNodes(detailsDataNode);

            if (citiesDataNodes != null && citiesDataNodes.Any())
            {
                foreach (XElement cityDataNode in citiesDataNodes)
                {
                    City city = ReadSingleCityData(cityDataNode);

                    if (city != null)
                        cities.Add(city);
                }
            }

            return cities;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML cities data nodes from XML details data node. </summary>
        /// <param name="detailsDataNode"> XML details data node. </param>
        /// <returns> List of cities XML data nodes. </returns>
        private List<XElement> GetCitiesDataNodes(XElement detailsDataNode)
        {
            XElement citiesDataNode = detailsDataNode.Descendants("ul").FirstOrDefault();

            return citiesDataNode != null ?
                citiesDataNode
                    .Descendants("li")
                    .ToList()
                : new List<XElement>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read single city data from XML city data node. </summary>
        /// <param name="cityDataNode"> XML city data node. </param>
        /// <returns> Single city data object. </returns>
        private City ReadSingleCityData(XElement cityDataNode)
        {
            return new City()
            {
                Color = ReadCityColor(cityDataNode),
                Name = ReadCityName(cityDataNode)
            };
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read city color from XML city data node. </summary>
        /// <param name="cityDataNode"> XML city data node. </param>
        /// <returns> City color as string. </returns>
        private string ReadCityColor(XElement cityDataNode)
        {
            return GetLineStopBorderColor(cityDataNode.Attribute("style").Value.Trim(), "border-left");
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read city name from XML city data node. </summary>
        /// <param name="cityDataNode"> XML city data node. </param>
        /// <returns> City name. </returns>
        private string ReadCityName(XElement cityDataNode)
        {
            return cityDataNode.Value?.Trim();
        }

        #endregion READ DETAILS DATA

        #region UTILITIES

        //  --------------------------------------------------------------------------------
        /// <summary> Simplify arrival additional data. </summary>
        /// <param name="data"> Additional arrival data. </param>
        /// <returns> Simplified arrival additional data. </returns>
        private string SimplifyPartData(string data)
        {
            string[] removeStrings = { "\n", "(", "+", ")", " ", "km", "min" };
            string result = data;

            foreach (var rmStr in removeStrings)
                result = result.Replace(rmStr, string.Empty);

            return result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival stop border color from style attribute in XML line data. </summary>
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

        #endregion UTILITIES

    }
}
