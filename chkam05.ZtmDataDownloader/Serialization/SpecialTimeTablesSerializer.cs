using chkam05.ZtmDataDownloader.Data.SpecialTimeTables;
using chkam05.ZtmDataDownloader.Data.Static;
using chkam05.ZtmDataDownloader.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace chkam05.ZtmDataDownloader.Serialization
{
    public class SpecialTimeTablesSerializer : BaseSerializer<SpecialTimeTableResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "<div class=\"lineList\">";
        private static readonly string _specialTTStartTrimmer = "<div class=\"col-xs-12\">";
        private static readonly string _endTrimmer = "</section>";


        //  VARIABLES

        private string _line;
        private TransportType _transportType;


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SpecialTimeTablesSerializer class constructor. </summary>
        public SpecialTimeTablesSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized lines data from web response. </returns>
        public override SpecialTimeTableResponseModel Deserialize(string rawData, params object[] parameters)
        {
            LoadParameters(parameters);

            var result = new SpecialTimeTableResponseModel()
            {
                Line = _line,
                TransportType = _transportType
            };

            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                if (!DetectSpecifiedTimeTables(xmlData))
                {
                    return result;
                }

                var timeTables = ReadTimeTables(xmlData);

                if (timeTables != null)
                    result.SpecialTimeTables = timeTables;
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

                //  Read line parameter.
                _line = parameters.Where(p => p is string).FirstOrDefault() as string;
            }
        }

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
        /// <summary> Read special time tables data from XML data node. </summary>
        /// <param name="xmlData"> XML special time tables data node. </param>
        /// <returns> List of special time tables data objects. </returns>
        private List<SpecialTimeTableItem> ReadTimeTables(XElement xmlData)
        {
            List<SpecialTimeTableItem> timeTables = new List<SpecialTimeTableItem>();
            XElement specialTimeTablesDataNode = GetTimeTablesDataNode(xmlData);
            List<string> headers = ReadHeaders(specialTimeTablesDataNode);

            var timeTablesDataNodes = specialTimeTablesDataNode.Descendants("tbody").Descendants("tr");

            if (timeTablesDataNodes != null && timeTablesDataNodes.Any())
                foreach (XElement timeTableDataNode in timeTablesDataNodes)
                {
                    SpecialTimeTableItem timeTable = ReadSpecialTimeTable(timeTableDataNode);

                    if (timeTable != null)
                        timeTables.Add(timeTable);
                }

            return timeTables;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML data node where special time tables are storred. </summary>
        /// <param name="xmlData"> XML special time tables data node. </param>
        /// <returns> XML data node where special time tables are storred. </returns>
        private XElement GetTimeTablesDataNode(XElement xmlData)
        {
            return xmlData.Descendants("table")
                .FirstOrDefault(nt => nt.Attributes()
                    .Any(a => a.Name == "class" && a.Value.Contains("table")));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get headers from special time tables. </summary>
        /// <param name="specialTimeTablesDataNode"> XML special time tables data node. </param>
        /// <returns> List of special time tables headers. </returns>
        private List<string> ReadHeaders(XElement specialTimeTablesDataNode)
        {
            return specialTimeTablesDataNode.Descendants("thead")
                .FirstOrDefault()
                .Descendants("th")
                .Select(th => th.Value)
                .ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read single special time table data from XML node. </summary>
        /// <param name="timeTableDataNode"> XML special time table data node. </param>
        /// <returns> Special time table data object. </returns>
        private SpecialTimeTableItem ReadSpecialTimeTable(XElement timeTableDataNode)
        {
            SpecialTimeTableItem timeTable = new SpecialTimeTableItem();
            int step = 0;

            foreach (XElement td in timeTableDataNode.Descendants("td"))
            {
                switch (step)
                {
                    case 0:
                        XElement aNode = td.Element("a");
                        timeTable.URL = ReadTimeTableUrl(aNode) ?? "";
                        timeTable.Value = ReadTimeTableValue(aNode);
                        break;

                    case 1:
                        timeTable.SetStartDate(td.Value);
                        break;

                    case 2:
                        timeTable.SetEndDate(td.Value);
                        break;
                }

                step++;
            }

            return timeTable;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read time table URL. </summary>
        /// <param name="aNode"> Link subnode from time table column data node. </param>
        /// <returns> Time table URL. </returns>
        private string ReadTimeTableUrl(XElement aNode)
        {
            if (aNode != null)
            {
                if (aNode.Attributes().Any(a => a.Name == "href"))
                    return aNode.Attribute("href").Value;
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read time table value. </summary>
        /// <param name="aNode"> Link subnode from time table column data node. </param>
        /// <returns> Time table value. </returns>
        private string ReadTimeTableValue(XElement aNode)
        {
            if (aNode != null)
            {
                if (aNode.Elements().Any(n => n.Name == "strong"))
                    return aNode.Element("strong").Value;
                else
                    return aNode.Value;
            }

            return null;
        }

        #endregion READ DATA METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Detect if line contains special time tables. </summary>
        /// <param name="xmlData"> XML data structure. </param>
        /// <returns> True - Line contains special time tables; False - otherwise. </returns>
        private bool DetectSpecifiedTimeTables(XElement serializedData)
        {
            return serializedData.Attribute("class").Value.Contains("col-xs-12");
        }

        #endregion UTILITY METHODS

    }
}
