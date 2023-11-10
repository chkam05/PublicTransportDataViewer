using MpkCzestochowaDownloader.Data.Arrives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.MpkCzestochowa
{
    public class LineArrivalsViewModel : BaseViewModel
    {

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
                AddCollectionChangedMethod(_arrivals, nameof(Arrivals));
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

    }
}
