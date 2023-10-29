using MpkCzestochowaDownloader.Data.Arrives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class LineArrivalsViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private LineArrivals _lineArrivals;
        private ObservableCollection<ArrivalViewModel> _arrivals;


        //  GETTERS & SETTERS

        public LineArrivals LineArrivals
        {
            get => _lineArrivals;
            set
            {
                _lineArrivals = value;
                OnPropertyChanged(nameof(LineArrivals));
            }
        }

        public ObservableCollection<ArrivalViewModel> Arrivals
        {
            get => _arrivals;
            set
            {
                _arrivals = value;
                _arrivals.CollectionChanged += OnArrivalsCollectionChanged;
                OnPropertyChanged(nameof(Arrivals));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineArrivalsViewModel class constructor. </summary>
        /// <param name="lineArrivals"> Line arrivals data object. </param>
        public LineArrivalsViewModel(LineArrivals lineArrivals)
        {
            LineArrivals = lineArrivals;

            Arrivals = new ObservableCollection<ArrivalViewModel>(lineArrivals.Arrivals.Select(a
                => new ArrivalViewModel(a)));
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

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
