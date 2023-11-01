using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZtmDataViewer.Data.MpkCzestochowa
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
            set
            {
                _lineDirection = value;
                OnPropertyChanged(nameof(LineDirection));
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Name
        {
            get => _lineDirection.Name;
        }

        public ObservableCollection<LineStopViewModel> Stops
        {
            get => _stops;
            set
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
        /// <param name="lineDirection"> Line direction. </param>
        public LineDirectionViewModel(LineDirection lineDirection)
        {
            LineDirection = lineDirection;

            Stops = new ObservableCollection<LineStopViewModel>(
                lineDirection.Stops.Select(s => new LineStopViewModel(s)));
        }

        #endregion CLASS METHODS

    }
}
