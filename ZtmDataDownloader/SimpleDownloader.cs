using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ZtmDataDownloader.Data.Arrivals;
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Data.TimeTables;
using ZtmDataDownloader.Downloaders;

namespace ZtmDataDownloader
{
    public static class SimpleDownloader
    {

        //  METHODS

        #region DOWNLOAD DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download lines data. </summary>
        /// <param name="transportTypes"> List of transport type data to dwonload. </param>
        /// <returns> Lines data. </returns>
        public static Dictionary<TransportType, List<Line>> DownloadLines(List<TransportType> transportTypes = null)
        {
            var downloader = new LinesDownloader();
            var request = new LinesRequestModel();
            var response = downloader.DownloadData(request);

            if (response != null && response.HasData)
            {
                if (transportTypes != null)
                    return response.Lines
                        .Where(kvp => transportTypes.Contains(kvp.Key))
                        .ToDictionary(k => k.Key, v => v.Value);
                else
                    return response.Lines;
            }

            return new Dictionary<TransportType, List<Line>>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Download special transport types data. </summary>
        /// <param name="line"> Line item. </param>
        /// <returns> Special time tables data. </returns>
        public static List<TimeTable> DownloadTimeTables(Line line)
        {
            if (line != null)
            {
                var downloader = new TimeTablesDownloader();
                var request = new TimeTableRequestModel(line.Value, line.Type);
                var response = downloader.DownloadData(request);

                if (response != null && response.HasData)
                    return response.TimeTables;
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Download detailed line data. </summary>
        /// <param name="line"> Line item. </param>
        /// <param name="timeTableId"> Special time table id. </param>
        /// <returns> Detailed line data. </returns>
        public static LineDetails DownloadLineDetails(Line line, string? timeTableId = null)
        {
            if (line != null)
            {
                var downloader = new LineDetailsDownloader();
                var request = new LineDetailsRequestModel(line.Value, line.Type, timeTableId);
                var response = downloader.DownloadData(request);

                if (response != null && response.HasData)
                    return response.Line;
            }

            return null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Download departures line data for particular stop. </summary>
        /// <param name="line"> Detailed line data. </param>
        /// <param name="stop"> Line stop data. </param>
        /// <returns> Departures data. </returns>
        public static Dictionary<DepartureGroup, List<Departure>> DownloadDepartures(Line line, LineStop stop)
        {
            if (line != null && stop != null)
            {
                var downloader = new DeparturesDownloader();
                var request = new DeparturesRequestModel(
                    line.Value, line.Type, stop.DirectionID, stop.ID, stop.TimeTableID);
                var response = downloader.DownloadData(request);

                if (response != null && response.HasData)
                    return response.Departures;
            }

            return new Dictionary<DepartureGroup, List<Departure>>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Download arrivals line data for next stops from particular stop and departure. </summary>
        /// <param name="line"> Detailed line data. </param>
        /// <param name="departure"> Departure stop data. </param>
        /// <returns> Arrivals data. </returns>
        public static DepartureDetails DownloadArrivalsData(Line line, Departure departure)
        {
            if (line != null && departure != null)
            {
                var downloader = new ArrivalsDownloader();
                var request = new ArrivalRequestModel(
                    line.Value, line.Type, departure.DirectionID, departure.StopID,
                    departure.TimeTableID, departure.ID, departure.DirectID);
                var response = downloader.DownloadData(request);

                if (response != null && response.HasData)
                    return response.DepartureDetails;
            }

            return null;
        }

        #endregion DOWNLOAD DATA METHODS

    }
}
