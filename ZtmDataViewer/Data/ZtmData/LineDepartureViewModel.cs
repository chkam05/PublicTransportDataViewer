using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Departures;

namespace ZtmDataViewer.Data.ZtmData
{
    public class LineDepartureViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private Departure _departure;


        //  GETTERS & SETTERS

        public Departure Departure
        {
            get => _departure;
            private set
            {
                _departure = value;
                OnPropertyChanged(nameof(Departure));
                OnPropertyChanged(nameof(Hour));
                OnPropertyChanged(nameof(Minute));
                OnPropertyChanged(nameof(IsVariant));
                OnPropertyChanged(nameof(LowFloor));
                OnPropertyChanged(nameof(Variant));
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Description
        {
            get => _departure.Description;
        }

        public int Hour
        {
            get => _departure.Hour;
        }

        public int Minute
        {
            get => _departure.Minute;
        }

        public bool IsVariant
        {
            get => _departure.IsVariant;
        }

        public bool LowFloor
        {
            get => _departure.LowFloor;
        }

        public string Variant
        {
            get => _departure.Variant;
        }

        public string Value
        {
            get => ToString();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDepartureViewModel class constructor. </summary>
        /// <param name="departure"> Departure data. </param>
        public LineDepartureViewModel(Departure departure)
        {
            Departure = departure;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            var hour = Hour < 10 ? $"0{Hour}" : $"{Hour}";
            var minute = Minute < 10 ? $"0{Minute}" : $"{Minute}";

            return IsVariant
                ? $"{hour}:{minute} {Variant}"
                : $"{hour}:{minute}";
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
