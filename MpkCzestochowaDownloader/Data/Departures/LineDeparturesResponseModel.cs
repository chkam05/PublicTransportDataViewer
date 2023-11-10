using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Departures
{
    public class LineDeparturesResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public LineDepartures LineDepartures { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => LineDepartures != null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDeparturesResponseModel class constructor. </summary>
        public LineDeparturesResponseModel() : base()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
