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
    public class DepartureDetailsViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private DepartureDetails _departureDetails;
        private ObservableCollection<ArrivalViewModel> _arrivals;
        private ObservableCollection<CityViewModel> _cities;
        private ObservableCollection<string> _infos;
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
                _arrivals.CollectionChanged += OnArrivalsCollectionChanged;
                OnPropertyChanged(nameof(Arrivals));
            }
        }

        public ObservableCollection<CityViewModel> Cities
        {
            get => _cities;
            private set
            {
                _cities = value;
                _cities.CollectionChanged += OnCitiesCollectionChanged;
                OnPropertyChanged(nameof(Cities));
            }
        }

        public ObservableCollection<string> Infos
        {
            get => _infos;
            private set
            {
                _infos = value;
                _infos.CollectionChanged += OnInfosCollectionChanged;
                OnPropertyChanged(nameof(Infos));
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> Informations
        {
            get => _informations;
            private set
            {
                _informations = value;
                _informations.CollectionChanged += OnInformationsCollectionChanged;
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

            Cities = new ObservableCollection<CityViewModel>(
                departureDetails.Cities.Select(c => new CityViewModel(c)));

            Infos = new ObservableCollection<string>(
                departureDetails.Infos);

            Informations = new ObservableCollection<KeyValuePair<string, string>>(
                departureDetails.Informations.Select(kvp => kvp));
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

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after arrivals collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnArrivalsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Arrivals));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after cities collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnCitiesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Cities));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after infos collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnInfosCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Infos));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after informations collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnInformationsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Informations));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
