using DownloaderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Utilities;

namespace ZtmDataDownloader.Serializers
{
    public class DeparturesSerializer : BaseSerializer<DeparturesResponseModel>
    {

        //  CONST

        private static readonly string[] _startTrimmers = new string[] { "<section>", "<section class=\"\">" };
        private static readonly string _endTrimmer = "</section>";


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DeparturesSerializer class constructor. </summary>
        public DeparturesSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw departures data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized departures data from web response. </returns>
        public override DeparturesResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new DeparturesResponseModel();
            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                var grouppedDepartureDataNodes = GetGrouppedDeparturesDataNodes(xmlData);

                if (grouppedDepartureDataNodes.Any())
                {
                    var departures = ReadDeparturesData(grouppedDepartureDataNodes);

                    if (departures != null && departures.Any())
                        result.Departures = departures;
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
                .Replace("<hr>", "<hr />")
                .Replace("<br>", "<br />");

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

            int startIndex = lines.IndexOf(lines.FirstOrDefault(l => _startTrimmers.Any(t => l.Contains(t))));
            int endIndex = lines.IndexOf(lines.FirstOrDefault(l => lines.IndexOf(l) > startIndex && l.Contains(_endTrimmer)));

            List<string> dataLines = lines.GetRange(startIndex, (endIndex - startIndex) + 1);
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

        #region PROCESS DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML data nodes with departures data. </summary>
        /// <param name="xmlData"> XML departures data. </param>
        /// <returns> XML data nodes where departures data are storred. </returns>
        private List<XElement> GetGrouppedDeparturesDataNodes(XElement xmlData)
        {
            var departureDataNodes = xmlData.Descendants("div")
                .Where(n => n.Attributes()
                    .Any(a => a.Name == "class"
                        && ((a.Value.Contains("panel ") && a.Value.Contains("panel-info")
                            || (a.Value.Contains("panel ") && a.Value.Contains("panel-default")))
                        && !a.Value.Contains("panel-kzkgop"))
                    )
                );

            if (departureDataNodes != null && departureDataNodes.Any())
                return departureDataNodes.ToList();
            else
                return new List<XElement>();
        }

        #endregion PROCESS DATA METHODS

        #region READ DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read departures data from groupped XML departures data nodes. </summary>
        /// <param name="grouppedDeparturesDataNodes"> List of groupped XML departures data nodes. </param>
        /// <returns> Groupped departures data objects by departure group. </returns>
        private Dictionary<DepartureGroup, List<Departure>> ReadDeparturesData(List<XElement> grouppedDeparturesDataNodes)
        {
            Dictionary<DepartureGroup, List<Departure>> grouppedDepartures = new Dictionary<DepartureGroup, List<Departure>>();
            int groupIndex = 0;

            foreach (var departureDataNode in grouppedDeparturesDataNodes)
            {
                var group = ReadDepartureGroup(departureDataNode, groupIndex);
                var departures = ReadDeparturesForGroup(departureDataNode);

                departures.ForEach(d => d.Group = group);

                grouppedDepartures.Add(group, departures);

                groupIndex++;
            }

            return grouppedDepartures;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read departure group from groupped XML departure data node. </summary>
        /// <param name="departureGroupDataNode"> Groupped XML departure data node. </param>
        /// <param name="groupIndex"> Index of group. </param>
        /// <returns> Departure group data object. </returns>
        private DepartureGroup ReadDepartureGroup(XElement departureGroupDataNode, int groupIndex)
        {
            DepartureGroup group = new DepartureGroup()
            {
                Index = groupIndex
            };

            var header = departureGroupDataNode.Descendants("div")
                .FirstOrDefault(a => a.Attributes()
                    .Any(at => at.Name == "class" && at.Value.Contains("panel-heading") && at.Value.Contains("clearfix")));

            if (header != null)
            {
                var headerValue = header.Descendants("strong").FirstOrDefault()?.Value;

                if (!string.IsNullOrEmpty(headerValue))
                {
                    group.Description = headerValue;
                }
            }

            return group;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read departures for group from groupped XML departure data node. </summary>
        /// <param name="departureGroupDataNode"> Groupped XML departure data node. </param>
        /// <returns> List of departures data objects. </returns>
        private List<Departure> ReadDeparturesForGroup(XElement departureGroupDataNode)
        {
            List<Departure> departures = new List<Departure>();
            List<XElement> departureDataNodes = GetDeparturesDataNodes(departureGroupDataNode);

            foreach (var departureDataNode in departureDataNodes)
            {
                var departuresFragmentData = ReadDepartures(departureDataNode);

                if (departuresFragmentData.Any())
                    departures.AddRange(departuresFragmentData);
            }

            return departures;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML departures data nodes from group. </summary>
        /// <param name="departureGroupDataNode"> Groupped XML departure data node. </param>
        /// <returns> List of XML departures data nodes from group. </returns>
        private List<XElement> GetDeparturesDataNodes(XElement departureGroupDataNode)
        {
            var departureDataNodes = departureGroupDataNode.Descendants("div")
                .Where(n => n.Attributes()
                    .Any(a => a.Name == "class" && a.Value == "arrival-time"));

            if (departureDataNodes != null && departureDataNodes.Any())
                return departureDataNodes.ToList();

            return new List<XElement>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read departures from departure data node. </summary>
        /// <param name="departureDataNode"> XML departure data node. </param>
        /// <returns> List of departures data objects. </returns>
        private List<Departure> ReadDepartures(XElement departureDataNode)
        {
            string hour = GetDepartureHour(departureDataNode);
            var subNodes = GetDepartureSubNodex(departureDataNode);

            List<Departure> departures = new List<Departure>();

            foreach (var subNode in subNodes)
            {
                Departure departure = new Departure();
                string minute = "";
                bool lowFloor = true;

                var aHref = subNode.Element("a");
                var classAttrib = aHref?.Attribute("class");
                var small = aHref?.Element("small");

                if (aHref != null)
                {
                    departure.Description = aHref.Attribute("title")?.Value;
                    departure.URL = aHref.Attribute("href").Value.Trim();
                    minute = aHref.Value.Trim();

                    //  Read low floor and remove info from minute.
                    if (minute.Contains("˙"))
                    {
                        lowFloor = false;
                        minute = minute.Replace("˙", "");
                    }

                    //  Read variant and remove info from minute.
                    if (small != null)
                    {
                        departure.Variant = small.Value.Trim();
                        minute = minute
                            .Replace(departure.Variant, "");
                    }

                    //  Check if low floor has special designation and read low floor.
                    if (classAttrib != null)
                    {
                        if (classAttrib.Value.Contains("btn-stoptime-underline"))
                            lowFloor = true;
                    }

                    //  Check if time data are correct and fill departure object.
                    if (!string.IsNullOrEmpty(minute) && !string.IsNullOrEmpty(hour))
                    {
                        departure.Hour = int.Parse(hour);
                        departure.Minute = int.Parse(minute);
                        departure.LowFloor = lowFloor;
                    }
                    else
                    {
                        continue;
                    }

                    departures.Add(departure);
                }
            }

            return departures;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read departure hour. </summary>
        /// <param name="departureDataNode"> XML departure data node. </param>
        /// <returns> Departure hour as string. </returns>
        private string GetDepartureHour(XElement departureDataNode)
        {
            return departureDataNode.Value.Trim().Split(
                new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)?[0];
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML departure sub nodes (departures for same hour). </summary>
        /// <param name="departureDataNode"> XML departure data node. </param>
        /// <returns> List of XML departure sub nodes. </returns>
        private List<XElement> GetDepartureSubNodex(XElement departureDataNode)
        {
            var subNodes = departureDataNode.Descendants("sup");

            if (subNodes != null && subNodes.Any())
                return subNodes.ToList();

            return new List<XElement>();
        }

        #endregion READ DATA METHODS

    }
}
