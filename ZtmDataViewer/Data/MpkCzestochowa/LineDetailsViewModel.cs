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
    public class LineDetailsViewModel : BaseViewModel
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
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(TransportType));
            }
        }

        public string Description
        {
            get
            {
                var result = !string.IsNullOrEmpty(_lineDetails.Value) ? $"{_lineDetails.Value} " : string.Empty;

                if (!string.IsNullOrEmpty(_lineDetails.DirectionFrom) && !string.IsNullOrEmpty(_lineDetails.DirectionTo))
                    result += $"({_lineDetails.DirectionFrom} - {_lineDetails.DirectionTo})";

                else if (!string.IsNullOrEmpty(_lineDetails.DirectionTo))
                    result += $"({_lineDetails.DirectionTo})";

                else if (!string.IsNullOrEmpty(_lineDetails.DirectionFrom))
                    result += $"({_lineDetails.DirectionFrom})";

                return result;
            }
        }

        public TransportType TransportType
        {
            get => _lineDetails.TransportType;
        }

        public ObservableCollection<TimeTableDateViewModel> Dates
        {
            get => _dates;
            set
            {
                _dates = value;
                AddCollectionChangedMethod(_dates, nameof(Dates));
                OnPropertyChanged(nameof(Dates));
            }
        }

        public ObservableCollection<LineDirectionViewModel> Directions
        {
            get => _directions;
            set
            {
                _directions = value;
                AddCollectionChangedMethod(_directions, nameof(Directions));
                OnPropertyChanged(nameof(Directions));
            }
        }

        public ObservableCollection<RouteVariantViewModel> RouteVariants
        {
            get => _routeVariants;
            set
            {
                _routeVariants = value;
                AddCollectionChangedMethod(_routeVariants, nameof(RouteVariants));
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

            SelectedDate = Dates.FirstOrDefault(d => d.TimeTableDate.Selected);
            SelectedDirection = Directions.FirstOrDefault();
            SelectedRouteVariant = RouteVariants.FirstOrDefault(r => r.RouteVariant.Selected);
        }

        #endregion CLASS METHODS

    }
}
