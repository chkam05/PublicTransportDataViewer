using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Arrivals;
using ZtmDataDownloader.Data.Global;

namespace ZtmDataViewer.Data.ZtmData
{
    public class DepartureDetailsViewModel : BaseViewModel
    {

        //  VARIABLES

        private DepartureDetails _departureDetails;
        private ObservableCollection<ArrivalViewModel> _arrivals;
        private ObservableCollection<KeyValuePair<string, string>> _informations;


        //  GETTERS & SETTERS

        public DepartureDetails DepartureDetails
        {
            get => _departureDetails;
            private set
            {
                _departureDetails = value;
                OnPropertyChanged(nameof(DepartureDetails));
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Description
        {
            get => string.Join(Environment.NewLine,
                _departureDetails.Description.Split(": ", StringSplitOptions.RemoveEmptyEntries));
        }

        public ObservableCollection<ArrivalViewModel> Arrivals
        {
            get => _arrivals;
            private set
            {
                _arrivals = value;
                AddCollectionChangedMethod(_arrivals, nameof(Arrivals));
                OnPropertyChanged(nameof(Arrivals));
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> Informations
        {
            get => _informations;
            private set
            {
                _informations = value;
                AddCollectionChangedMethod(_informations, nameof(Informations));
                OnPropertyChanged(nameof(Informations));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DepartureDetailsViewModel class constructor. </summary>
        /// <param name="departureDetails"> Departure details data. </param>
        public DepartureDetailsViewModel(DepartureDetails departureDetails)
        {
            DepartureDetails = departureDetails;

            Arrivals = new ObservableCollection<ArrivalViewModel>(
                departureDetails.Arrivals.Select(a => new ArrivalViewModel(a)));

            Informations = new ObservableCollection<KeyValuePair<string, string>>(
                departureDetails.Informations.Select(kvp => kvp));
        }

        #endregion CLASS METHODS

    }
}
