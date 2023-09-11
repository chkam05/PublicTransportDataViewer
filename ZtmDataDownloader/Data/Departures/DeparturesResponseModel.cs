using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataDownloader.Data.Departures
{
    public class DeparturesResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public Dictionary<DepartureGroup, List<Departure>> Departures { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => Departures != null && Departures.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DeparturesResponseModel class constructor. </summary>
        public DeparturesResponseModel() : base()
        {
            if (Departures == null)
                Departures = new Dictionary<DepartureGroup, List<Departure>>();
        }

        #endregion CLASS METHODS

    }
}
