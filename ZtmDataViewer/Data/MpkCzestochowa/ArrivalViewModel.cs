using MpkCzestochowaDownloader.Data.Arrives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class ArrivalViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private Arrival _arrival;


        //  GETTERS & SETTERS

        public Arrival Arrival
        {
            get => _arrival;
            set
            {
                _arrival = value;
                OnPropertyChanged(nameof(Arrival));
                OnPropertyChanged(nameof(StopName));
                OnPropertyChanged(nameof(Time));
            }
        }

        public string? StopName
        {
            get => _arrival.StopName;
        }

        public string? Time
        {
            get => _arrival.Time?.ToString("HH:mm");
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalViewModel class constructor. </summary>
        /// <param name="arrival"> Arrival data object. </param>
        public ArrivalViewModel(Arrival arrival)
        {
            Arrival = arrival;
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
