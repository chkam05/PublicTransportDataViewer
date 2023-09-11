using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Global;

namespace ZtmDataDownloader.Data.Arrivals
{
    public class DepartureDetails
    {

        //  VARIABLES

        public List<Arrival> Arrivals { get; set; }
        public List<City> Cities { get; set; }
        public Dictionary<string, string> Informations { get; set; }
        public List<string> Infos { get; set; }
        public string Description { get; set; }


        //  GETTERS & SETTERS

        public bool HasArrivalData
        {
            get => Arrivals != null && Arrivals.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DepartureDetails class constructor. </summary>
        public DepartureDetails()
        {
            if (Infos == null)
                Infos = new List<string>();

            if (Arrivals == null)
                Arrivals = new List<Arrival>();

            if (Cities == null)
                Cities = new List<City>();

            if (Informations == null)
                Informations = new Dictionary<string, string>();
        }

        #endregion CLASS METHODS

    }
}
