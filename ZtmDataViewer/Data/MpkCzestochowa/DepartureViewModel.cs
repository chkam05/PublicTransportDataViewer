using MpkCzestochowaDownloader.Data.Departures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class DepartureViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


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
                OnPropertyChanged(nameof(DataTrip));
                OnPropertyChanged(nameof(DataRoute));
                OnPropertyChanged(nameof(Time));
            }
        }

        public string? DataTrip
        {
            get => _departure.DataTrip;
        }

        public string? DataRoute
        {
            get => _departure.DataRoute;
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

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Invoke PropertyChangedEventHandler event method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
