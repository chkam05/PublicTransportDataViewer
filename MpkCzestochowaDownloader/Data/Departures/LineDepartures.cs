using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Departures
{
    public class LineDepartures
    {

        //  VARIABLES

        public string DirectionId { get; set; }
        public string LineId { get; set; }
        public string RouteVariantId { get; set; }
        public string StopId { get; set; }
        public TimeTableDate? Date { get; set; }
        public List<TimeTableDate> Dates { get; set; }
        public string Direction { get; set; }
        public string Name { get; set; }
        public List<OtherLine> OtherLines { get; set; }
        public List<LineStop> Stops { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDepartures class constructor. </summary>
        public LineDepartures()
        {
            if (Dates == null)
                Dates = new List<TimeTableDate>();

            if (OtherLines == null)
                OtherLines = new List<OtherLine>();

            if (Stops == null)
                Stops = new List<LineStop>();
        }

        #endregion CLASS METHODS

    }
}
