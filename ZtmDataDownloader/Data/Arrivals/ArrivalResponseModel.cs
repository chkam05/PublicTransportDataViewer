using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataDownloader.Data.Arrivals
{
    public class ArrivalResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public DepartureDetails DepartureDetails { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => DepartureDetails != null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalResponseModel class constructor. </summary>
        public ArrivalResponseModel() : base()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
