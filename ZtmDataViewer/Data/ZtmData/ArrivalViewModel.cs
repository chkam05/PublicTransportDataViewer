using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZtmDataDownloader.Data.Arrivals;

namespace ZtmDataViewer.Data.ZtmData
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
            private set
            {
                _arrival = value;
                OnPropertyChanged(nameof(Arrival));
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(SpecialDistance));
                OnPropertyChanged(nameof(SpecialTime));
                OnPropertyChanged(nameof(Value));
            }
        }

        public Color Color
        {
            get => _arrival.GetColor();
        }

        public string Name
        {
            get => _arrival.Name;
        }

        public string SpecialDistance
        {
            get => _arrival.SpecialDistance;
        }

        public string SpecialTime
        {
            get => _arrival.SpecialTime;
        }

        public string Value
        {
            get => _arrival.Value;
        }

        
        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalViewModel class constructor. </summary>
        /// <param name="arrival"> Arrival link data. </param>
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
