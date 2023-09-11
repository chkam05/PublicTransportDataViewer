using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Static;

namespace ZtmDataDownloader.Data.TimeTables
{
    public class TimeTableResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public string Line { get; set; }
        public List<TimeTable> TimeTables { get; set; }
        public TransportType TransportType { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => TimeTables != null && TimeTables.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TimeTableResponseModel class constructor. </summary>
        public TimeTableResponseModel() : base()
        {
            if (TimeTables == null)
                TimeTables = new List<TimeTable>();
        }

        #endregion CLASS METHODS

    }
}
