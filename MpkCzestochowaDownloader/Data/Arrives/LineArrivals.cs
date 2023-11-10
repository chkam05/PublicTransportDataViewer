using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Arrives
{
    public class LineArrivals
    {

        //  VARIABLES

        public List<Arrival> Arrivals { get; set; }
        public DateTime? TripTime { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineArrivals class constructor. </summary>
        public LineArrivals()
        {
            if (Arrivals == null)
                Arrivals = new List<Arrival>();
        }

        #endregion CLASS METHODS

    }
}
