using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Line;

namespace ZtmDataViewer.Data.ZtmData
{
    public class LineDirectionViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private LineDirection _lineDirection;
        private ObservableCollection<LineStopViewModel> _stops;


        //  GETTERS & SETTERS

        public LineDirection LineDirection
        {
            get => _lineDirection;
            private set
            {
                _lineDirection = value;
                OnPropertyChanged(nameof(LineDirection));
                OnPropertyChanged(nameof(Direction));
            }
        }

        public string Direction
        {
            get => _lineDirection.Direction;
        }

        public ObservableCollection<LineStopViewModel> Stops
        {
            get => _stops;
            private set
            {
                _stops = value;
                _stops.CollectionChanged += OnStopsCollectionChanged;
                OnPropertyChanged(nameof(Stops));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDirectionViewModel class constructor. </summary>
        /// <param name="lineDirection"> Line direction data. </param>
        public LineDirectionViewModel(LineDirection lineDirection)
        {
            LineDirection = lineDirection;

            Stops = new ObservableCollection<LineStopViewModel>(
                lineDirection.Stops.Select(s => new LineStopViewModel(s)));
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
        /// <summary> Method invoked after stops collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnStopsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Stops));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
