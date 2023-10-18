using DownloaderCore.Utilities;
using MpkCzestochowaDownloader.Data.Arrives;
using MpkCzestochowaDownloader.Data.Departures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MpkCzestochowaDownloader.Serializers
{
    public class LineArrivalsSerializer : DedicatedSerializer<LineArrivalsResponseModel>
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineArrivalsSerializer class constructor. </summary>
        public LineArrivalsSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw line arrivals data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized line arrivals data from web response. </returns>
        public override LineArrivalsResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new LineArrivalsResponseModel();
            var jsonResponse = JsonConvert.DeserializeObject<LineArrivalsJsonResponse>(rawData);
            string preprocessedData = PreprocessData(jsonResponse.Html);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                var lineArrivals = ReadLineArrivalsData(xmlData);

                if (lineArrivals != null)
                    result.LineArrivals = lineArrivals;
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
        private string? PreprocessData(string rawData)
        {
            string trimmedData = TrimData(rawData)
                .Trim()
                .Replace("\t", "")
                .Replace("&", "&amp;");

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

            return string.Join(string.Empty, lines);
        }

        #endregion PREPROCESSING DATA METHODS

        #region READ LINE ARRIVALS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Read line arrivals data. </summary>
        /// <param name="xmlLineArrivalsData"> XML line arrivals data. </param>
        /// <returns> Line arrivals data object. </returns>
        private LineArrivals? ReadLineArrivalsData(XElement xmlLineArrivalsData)
        {
            var modalHeaderSection = GetModalHeaderSection(xmlLineArrivalsData);
            var modalBodySection = GetModalBodySection(xmlLineArrivalsData);

            if (modalHeaderSection == null)
                return null;

            if (modalBodySection == null)
                return null;

            var lineArrivals = new LineArrivals()
            {
                TripTime = GetTripTime(modalHeaderSection)
            };

            var arrivals = ReadArrivals(modalBodySection);

            if (arrivals != null)
                lineArrivals.Arrivals = arrivals;

            return lineArrivals;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Read line arrivals data. </summary>
        /// <param name="xmlModalBodySection"> XML modal body section. </param>
        /// <returns> List of arrivals data objects. </returns>
        private List<Arrival>? ReadArrivals(XElement xmlModalBodySection)
        {
            var xmlArrivals = xmlModalBodySection
                .Element("table")
                ?.Element("tbody")
                ?.Descendants("tr");

            if (xmlArrivals?.Any() ?? false)
            {
                return xmlArrivals.Select(e => new Arrival()
                {
                    Id = GetArrivalId(e),
                    StopName = GetArrivalStopName(e),
                    Time = GetArrivalTime(e),
                }).ToList();
            }

            return null;
        }

        #endregion READ LINE ARRIVALS METHODS

        #region XML DATA GET METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get trip time. </summary>
        /// <param name="xmlModalHeaderSection"> XML modal header section. </param>
        /// <returns> Trip time data. </returns>
        private DateTime? GetTripTime(XElement xmlModalHeaderSection)
        {
            var spanTripTime = xmlModalHeaderSection
                .Element("h5")
                ?.Descendants("span")
                .FirstOrDefault(e => e.Attribute("id")?.Value == "trip-time")
                ?.Value;

            if (!string.IsNullOrEmpty(spanTripTime)
                    && DateTime.TryParseExact(spanTripTime,
                        "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                return dt;

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival id. </summary>
        /// <param name="xmlArrival"> XML arrival data. </param>
        /// <returns> Arrival id data. </returns>
        private int GetArrivalId(XElement xmlArrival)
        {
            return int.TryParse(xmlArrival
                        .Elements("td")
                        .FirstOrDefault(e => e.Attribute("class")?.Value == "text-right")
                        ?.Value,
                    out int id)
                ? id : 0;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival stop name. </summary>
        /// <param name="xmlArrival"> XML arrival data. </param>
        /// <returns> Arrival stop name data. </returns>
        private string? GetArrivalStopName(XElement xmlArrival)
        {
            return xmlArrival.Descendants("span")
                .FirstOrDefault(e => e.Attribute("class")?.Value == "bus-stop")
                ?.Value;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get arrival time. </summary>
        /// <param name="xmlArrival"> XML arrival data. </param>
        /// <returns> Arrival time data. </returns>
        private DateTime? GetArrivalTime(XElement xmlArrival)
        {
            var timeComponents = xmlArrival
                .Elements("td")
                .FirstOrDefault(e => e.Attribute("class")?.Value.StartsWith("time") ?? false)
                ?.Value.Split(":")
                .ToList();

            if (!(timeComponents?.Any() ?? false))
                return null;

            if (int.TryParse(timeComponents.First(), out int hour) && hour >= 24)
                timeComponents[0] = hour - 24 < 10 ? $"0{hour - 24}" : $"{hour - 24}";

            return DateTime.TryParseExact(
                    string.Join(":", timeComponents),
                    "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt)
                ? dt : null;
        }

        #endregion XML DATA GET METHODS

        #region XML SECTIONS GET METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get XML arrivals header section data. </summary>
        /// <param name="xmlLineArrivalsData"> XML line arrivals data. </param>
        /// <returns> XML arrivals header section data. </returns>
        private XElement? GetModalHeaderSection(XElement xmlLineArrivalsData)
        {
            return xmlLineArrivalsData.Descendants("div")
                .FirstOrDefault(e => e.Attribute("class")?.Value == "modal-header");
        }


        //  --------------------------------------------------------------------------------
        /// <summary> Get XML arrivals body section data. </summary>
        /// <param name="xmlLineArrivalsData"> XML line arrivals data. </param>
        /// <returns> XML arrivals body section data. </returns>
        private XElement? GetModalBodySection(XElement xmlLineArrivalsData)
        {
            return xmlLineArrivalsData.Descendants("div")
                .FirstOrDefault(e => e.Attribute("class")?.Value == "modal-body");
        }

        #endregion XML SECTIONS GET METHODS

    }
}
