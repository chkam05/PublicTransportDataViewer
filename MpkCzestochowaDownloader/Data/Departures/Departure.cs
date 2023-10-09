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

        public List<string> Attributes { get; set; }
        public string? DataTrip { get; set; }
        public string? DataRoute { get; set; }
        public DateTime? Time { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Departure class constructor. </summary>
        public Departure()
        {
            if (Attributes == null)
                Attributes = new List<string>();
        }

        #endregion CLASS METHODS

    }
}
