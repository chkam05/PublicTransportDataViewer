using MpkCzestochowaDownloader.Data.Departures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.MpkCzestochowa
{
    public class DepartureViewModel : BaseViewModel
    {

        //  VARIABLES

        private Departure _departure;


        //  GETTERS & SETTERS

        public Departure Departure
        {
            get => _departure;
            set
            {
                _departure = value;
                OnPropertyChanged(nameof(Departure));
                OnPropertyChanged(nameof(Time));
            }
        }

        public string? Time
        {
            get => _departure.Time?.ToString("HH:mm");
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DepartureViewModel class constructor. </summary>
        /// <param name="departure"> Departure data object. </param>
        public DepartureViewModel(Departure departure)
        {
            Departure = departure;
        }

        #endregion CLASS METHODS

    }
}
