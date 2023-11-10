using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Departures;

namespace PublicTransportDataViewer.Data.ZtmData
{
    public class LineDepartureViewModel : BaseViewModel
    {

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
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(IsVariant));
                OnPropertyChanged(nameof(LowFloor));
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Description
        {
            get => _departure.Description;
        }

        public bool IsVariant
        {
            get => _departure.IsVariant;
        }

        public bool LowFloor
        {
            get => _departure.LowFloor;
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
            var hour = _departure.Hour < 10 ? $"0{_departure.Hour}" : $"{_departure.Hour}";
            var minute = _departure.Minute < 10 ? $"0{_departure.Minute}" : $"{_departure.Minute}";

            return IsVariant
                ? $"{hour}:{minute} {_departure.Variant}"
                : $"{hour}:{minute}";
        }

        #endregion CLASS METHODS

    }
}
