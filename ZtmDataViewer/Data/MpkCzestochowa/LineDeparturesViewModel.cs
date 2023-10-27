using MpkCzestochowaDownloader.Data.Departures;
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
    public class LineDeparturesViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


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
                OnPropertyChanged(nameof(DirectionName));
                OnPropertyChanged(nameof(ImageURL));
                OnPropertyChanged(nameof(StopName));
                OnPropertyChanged(nameof(Value));
            }
        }

        public string? DirectionName
        {
            get => _lineDepartures.DirectionName;
        }

        public string ImageURL
        {
            get => _lineDepartures.ImageURL;
        }

        public string? StopName
        {
            get => _lineDepartures.StopName;
        }

        public string? Value
        {
            get => _lineDepartures.Value;
        }

        public ObservableCollection<DepartureViewModel> Departures
        {
            get => _departures;
            set
            {
                _departures = value;
                _departures.CollectionChanged += OnDeparturesCollectionChanged;
                OnPropertyChanged(nameof(Departures));
            }
        }

        public ObservableCollection<OtherLineViewModel> OtherLines
        {
            get => _otherLines;
            set
            {
                _otherLines = value;
                _otherLines.CollectionChanged += OnOtherLinesCollectionChanged;
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
        /// <summary> Method invoked after departures collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnDeparturesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Departures));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after other lines collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnOtherLinesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(OtherLines));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
