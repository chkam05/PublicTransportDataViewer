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

namespace PublicTransportDataViewer.Data.ZtmData
{
    public class LineDirectionViewModel : BaseViewModel
    {

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
                AddCollectionChangedMethod(_stops, nameof(Stops));
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

    }
}
