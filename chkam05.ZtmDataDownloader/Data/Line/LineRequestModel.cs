using chkam05.ZtmDataDownloader.Data.Base;
using chkam05.ZtmDataDownloader.Data.Static;
using chkam05.ZtmDataDownloader.Mappers;
using System;


namespace chkam05.ZtmDataDownloader.Data.Line
{
    public class LineRequestModel : BaseRequestModel
    {

        //  VARIABLES

        private string _line;
        private string _timeTableId;
        private TransportType _transportType;


        //  GETTERS & SETTERS

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

        public string TimeTableID
        {
            get => _timeTableId;
            set
            {
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
        /// <summary> LineRequestModel class constructor with parameters. </summary>
        /// <param name="line"> Line. </param>
        /// <param name="transportType"> Line transport type. </param>
        /// <param name="timeTableId"> Special timetable id. </param>
        public LineRequestModel(string line, TransportType transportType, string timeTableId = null)
        {
            _line = line;
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

            if (!string.IsNullOrEmpty(_timeTableId))
                URL = StaticConfig.ZtmLinesURL + $"{transportTypeWebIndex}-{_line.ToLower()}/{_timeTableId}/";

            else
                URL = StaticConfig.ZtmLinesURL + $"{transportTypeWebIndex}-{_line.ToLower()}";
        }

        #endregion UTILITY METHODS

    }
}
