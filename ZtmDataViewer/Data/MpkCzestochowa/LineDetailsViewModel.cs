using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Data.Static;
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
    public class LineDetailsViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private LineDetails _lineDetails;
        private ObservableCollection<TimeTableDateViewModel> _dates;
        private ObservableCollection<LineDirectionViewModel> _directions;
        private ObservableCollection<RouteVariantViewModel> _routeVariants;
        private TimeTableDateViewModel? _selectedDate;
        private LineDirectionViewModel? _selectedDirection;
        private RouteVariantViewModel? _selectedRouteVariant;


        //  GETTERS & SETTERS

        public LineDetails LineDetails
        {
            get => _lineDetails;
            set
            {
                _lineDetails = value;
                OnPropertyChanged(nameof(LineDetails));
                OnPropertyChanged(nameof(DirectionFrom));
                OnPropertyChanged(nameof(DirectionTo));
                OnPropertyChanged(nameof(LineId));
                OnPropertyChanged(nameof(TransportType));
                OnPropertyChanged(nameof(Value));
            }
        }

        public string? DirectionFrom
        {
            get => _lineDetails.DirectionFrom;
        }

        public string? DirectionTo
        {
            get => _lineDetails.DirectionTo;
        }

        public string? LineId
        {
            get => _lineDetails.LineId;
        }

        public TransportType TransportType
        {
            get => _lineDetails.TransportType;
        }

        public string? Value
        {
            get => _lineDetails.Value;
        }

        public ObservableCollection<TimeTableDateViewModel> Dates
        {
            get => _dates;
            set
            {
                _dates = value;
                _dates.CollectionChanged += OnTimeTableDatesCollectionChanged;
                OnPropertyChanged(nameof(Dates));
            }
        }

        public ObservableCollection<LineDirectionViewModel> Directions
        {
            get => _directions;
            set
            {
                _directions = value;
                _directions.CollectionChanged += OnDirectionsCollectionChanged;
                OnPropertyChanged(nameof(Directions));
            }
        }

        public ObservableCollection<RouteVariantViewModel> RouteVariants
        {
            get => _routeVariants;
            set
            {
                _routeVariants = value;
                _routeVariants.CollectionChanged += OnRouteVariantsCollectionChanged;
                OnPropertyChanged(nameof(RouteVariants));
            }
        }

        public TimeTableDateViewModel? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public LineDirectionViewModel? SelectedDirection
        {
            get => _selectedDirection;
            set
            {
                _selectedDirection = value;
                OnPropertyChanged(nameof(SelectedDirection));
            }
        }

        public RouteVariantViewModel? SelectedRouteVariant
        {
            get => _selectedRouteVariant;
            set
            {
                _selectedRouteVariant = value;
                OnPropertyChanged(nameof(SelectedRouteVariant));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsViewModel class constructor. </summary>
        /// <param name="lineDetails"> Line details. </param>
        public LineDetailsViewModel(LineDetails lineDetails)
        {
            LineDetails = lineDetails;

            Dates = new ObservableCollection<TimeTableDateViewModel>(
                lineDetails.Dates.Select(d => new TimeTableDateViewModel(d)));

            Directions = new ObservableCollection<LineDirectionViewModel>(
                lineDetails.Directions.Select(d => new LineDirectionViewModel(d)));

            RouteVariants = new ObservableCollection<RouteVariantViewModel>(
                lineDetails.RouteVariants.Select(r => new RouteVariantViewModel(r)));

            SelectedDate = Dates.FirstOrDefault(d => d.IsSelected);
            SelectedDirection = Directions.FirstOrDefault();
            SelectedRouteVariant = RouteVariants.FirstOrDefault(r => r.IsSelected);
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
        /// <summary> Method invoked after directions collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnDirectionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Directions));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after time table dates collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnTimeTableDatesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Dates));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after route variants collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnRouteVariantsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(RouteVariants));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
