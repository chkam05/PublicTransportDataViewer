using DownloaderCore.Data;
using MpkCzestochowaDownloader.Data.Departures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Arrives
{
    public class LineArrivalsResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public LineArrivals LineArrivals { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => LineArrivals != null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineArrivalsResponseModel class constructor. </summary>
        public LineArrivalsResponseModel() : base()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
