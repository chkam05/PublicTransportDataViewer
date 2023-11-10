using MpkCzestochowaDownloader.Data.Arrives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.MpkCzestochowa
{
    public class ArrivalViewModel : BaseViewModel
    {

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

    }
}
