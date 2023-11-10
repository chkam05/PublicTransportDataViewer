using MpkCzestochowaDownloader.Data.Departures;
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
    public class LineDeparturesViewModel : BaseViewModel
    {

        //  VARIABLES

        private LineDepartures _lineDepartures;
        private ObservableCollection<DepartureViewModel> _departures;
        private ObservableCollection<OtherLineViewModel> _otherLines;


        //  GETTERS & SETTERS

        public LineDepartures LineDepartures
        {
            get => _lineDepartures;
            set
            {
                _lineDepartures = value;
                OnPropertyChanged(nameof(LineDepartures));
            }
        }

        public ObservableCollection<DepartureViewModel> Departures
        {
            get => _departures;
            set
            {
                _departures = value;
                AddCollectionChangedMethod(_departures, nameof(Departures));
                OnPropertyChanged(nameof(Departures));
            }
        }

        public ObservableCollection<OtherLineViewModel> OtherLines
        {
            get => _otherLines;
            set
            {
                _otherLines = value;
                AddCollectionChangedMethod(_otherLines, nameof(OtherLines));
                OnPropertyChanged(nameof(OtherLines));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDeparturesViewModel class constructor. </summary>
        /// <param name="lineDepartures"> Line departures data object. </param>
        public LineDeparturesViewModel(LineDepartures lineDepartures)
        {
            LineDepartures = lineDepartures;

            Departures = new ObservableCollection<DepartureViewModel>(
                lineDepartures.Departures.Select(d => new DepartureViewModel(d)));

            OtherLines = new ObservableCollection<OtherLineViewModel>(
                lineDepartures.OtherLines.Select(o => new OtherLineViewModel(o)));
        }

        #endregion CLASS METHODS

    }
}
