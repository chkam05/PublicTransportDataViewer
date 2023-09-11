using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Mappers;

namespace ZtmDataDownloader.Data.Arrivals
{
    public class ArrivalRequestModel : BaseRequestModel
    {

        //  VARIABLES

        private string _departureId;
        private string _departureDirectId;
        private string _directionId;
        private string _line;
        private string _stopId;
        private string _timeTableId;
        private TransportType _transportType;


        //  GETTERS & SETTERS

        public string DepartureID
        {
            get => _departureId;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Departure id cannot be null and empty.");

                _departureId = value;
                UpdateURL();
            }
        }

        public string DepartureDirectID
        {
            get => _departureDirectId;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Departure direct id cannot be null and empty.");

                _departureDirectId = value;
                UpdateURL();
            }
        }

        public string Line
        {
            get => _line;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Line cannot be null and empty.");

                Line = value;
                UpdateURL();
            }
        }

        public string DirectionID
        {
            get => _directionId;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Direction id cannot be null and empty.");

                _directionId = value;
                UpdateURL();
            }
        }

        public string StopId
        {
            get => _stopId;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Stop id cannot be null and empty.");

                _stopId = value;
                UpdateURL();
            }
        }

        public string TimeTableID
        {
            get => _timeTableId;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Timetable id cannot be null and empty.");

                _timeTableId = value;
                UpdateURL();
            }
        }

        public TransportType TransportType
        {
            get => _transportType;
            set
            {
                _transportType = value;
                UpdateURL();
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DeparturesRequestModel class constructor with parameters. </summary>
        /// <param name="line"> Line. </param>
        /// <param name="transportType"> Line transport type. </param>
        /// <param name="directionId"> Line direction id. </param>
        /// <param name="stopId"> Line stop id. </param>
        /// <param name="timeTableId"> Line timetable id. </param>
        /// <param name="departureId"> Departure id. </param>
        /// <param name="directId"> Departure direct id. </param>
        public ArrivalRequestModel(string line, TransportType transportType,
            string directionId, string stopId, string timeTableId, string departureId, string directId)
        {
            _departureId = departureId;
            _departureDirectId = directId;
            _directionId = directionId;
            _line = line;
            _stopId = stopId;
            _timeTableId = timeTableId;
            _transportType = transportType;

            UpdateURL();
        }

        #endregion CLASS METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Update line request URL. </summary>
        private void UpdateURL()
        {
            int transportTypeWebIndex = TransportTypesMapper.MapToWebIndex(_transportType);

            URL = StaticConfig.ZtmLinesURL + $"{transportTypeWebIndex}-{_line.ToLower()}/" +
                $"{_timeTableId}/{_directionId}/{_stopId}/{_departureId}/{_departureDirectId}";
        }

        #endregion UTILITY METHODS

    }
}
