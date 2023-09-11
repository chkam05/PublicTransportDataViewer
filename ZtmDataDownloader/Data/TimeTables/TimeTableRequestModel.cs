using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Mappers;

namespace ZtmDataDownloader.Data.TimeTables
{
    public class TimeTableRequestModel : BaseRequestModel
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
        /// <summary> TimeTableRequestModel class constructor with parameters. </summary>
        /// <param name="line"> Line. </param>
        /// <param name="transportType"> Line transport type. </param>
        public TimeTableRequestModel(string line, TransportType transportType)
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
