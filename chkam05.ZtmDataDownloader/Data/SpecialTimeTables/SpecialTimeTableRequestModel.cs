using chkam05.ZtmDataDownloader.Data.Base;
using chkam05.ZtmDataDownloader.Data.Static;
using chkam05.ZtmDataDownloader.Mappers;
using System;


namespace chkam05.ZtmDataDownloader.Data.SpecialTimeTables
{
    public class SpecialTimeTableRequestModel : BaseRequestModel
    {

        //  VARIABLES

        private string _line;
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
        /// <summary> SpecialTimeTableRequestModel class constructor with parameters. </summary>
        /// <param name="line"> Line. </param>
        /// <param name="transportType"> Line transport type. </param>
        public SpecialTimeTableRequestModel(string line, TransportType transportType)
        {
            _line = line;
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
            URL = StaticConfig.ZtmLinesURL + $"{transportTypeWebIndex}-{_line}";
        }

        #endregion UTILITY METHODS

    }
}
