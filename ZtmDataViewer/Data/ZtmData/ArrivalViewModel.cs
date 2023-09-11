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
        private ObservableCollection<ArrivalLinkViewModel> _links;


        //  GETTERS & SETTERS

        public Arrival Arrival
        {
            get => _arrival;
            private set
            {
                _arrival = value;
                OnPropertyChanged(nameof(Arrival));
                OnPropertyChanged(nameof(AdditionalInfo));
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(FullDistance));
                OnPropertyChanged(nameof(FullTime));
                OnPropertyChanged(nameof(Hour));
                OnPropertyChanged(nameof(Minute));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(PartDistance));
                OnPropertyChanged(nameof(PartTime));
                OnPropertyChanged(nameof(SpecialDistance));
                OnPropertyChanged(nameof(SpecialTime));
                OnPropertyChanged(nameof(Value));
            }
        }

        public string AdditionalInfo
        {
            get => _arrival.AdditionalInfo;
        }

        public Color Color
        {
            get => _arrival.GetColor();
        }

        public string FullDistance
        {
            get => _arrival.FullDistance;
        }

        public string FullTime
        {
            get => _arrival.FullTime;
        }

        public int Hour
        {
            get => _arrival.Hour;
        }

        public int Minute
        {
            get => _arrival.Minute;
        }

        public string Name
        {
            get => _arrival.Name;
        }

        public string PartDistance
        {
            get => _arrival.PartDistance;
        }

        public string PartTime
        {
            get => _arrival.PartTime;
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

        public ObservableCollection<ArrivalLinkViewModel> Links
        {
            get => _links;
            private set
            {
                _links = value;
                _links.CollectionChanged += OnLinksCollectionChanged;
                OnPropertyChanged(nameof(Links));
            }
        }

        
        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalViewModel class constructor. </summary>
        /// <param name="arrival"> Arrival link data. </param>
        public ArrivalViewModel(Arrival arrival)
        {
            Arrival = arrival;

            Links = new ObservableCollection<ArrivalLinkViewModel>(
                arrival.Links.Select(l => new ArrivalLinkViewModel(l)));
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
        /// <summary> Method invoked after links collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnLinksCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Links));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
