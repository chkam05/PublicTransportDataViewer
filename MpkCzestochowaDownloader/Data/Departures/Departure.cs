using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Departures
{
    public class Departure
    {

        //  VARIABLES

        public string DataRoute { get; set; }
        public string DataTrip { get; set; }
        public bool Invariant { get; set; }
        public DateTime? Time { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Departure class constructor. </summary>
        public Departure()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
