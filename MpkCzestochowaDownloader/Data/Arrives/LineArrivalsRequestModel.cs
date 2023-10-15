using DownloaderCore.Data;
using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Arrives
{
    public class LineArrivalsRequestModel : BaseRequestModel
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDeparturesRequestModel class constructor. </summary>
        /// <param name="url"> Line stop url. </param>
        public LineArrivalsRequestModel(string url)
        {
            URL = url;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> LineDeparturesRequestModel class constructor. </summary>
        /// <param name="dataTrip"> Trip nubler. </param>
        /// <param name="departureTime"> Departure time. </param>
        /// <param name="departureDate"> Departure date. </param>
        public LineArrivalsRequestModel(string dataTrip, string departureTime, string departureDate)
        {
            URL = $"{StaticConfig.ArrivesURL}?t={dataTrip}&ft={departureTime}&dt={departureDate}";
        }

        #endregion CLASS METHODS

    }
}
