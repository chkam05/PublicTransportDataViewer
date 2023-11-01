using MpkCzestochowaDownloader.Data.Lines;
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
    public class LineGroupViewModel : BaseViewModel
    {

        //  VARIABLES

        private ObservableCollection<LineViewModel> _lines;
        private TransportType _transportType;


        //  GETTERS & SETTERS

        public ObservableCollection<LineViewModel> Lines
        {
            get => _lines;
            private set
            {
                _lines = value;
                AddCollectionChangedMethod(_lines, nameof(Lines));
                OnPropertyChanged(nameof(Lines));
            }
        }

        public TransportType TransportType
        {
            get => _transportType;
            private set
            {
                _transportType = value;
                OnPropertyChanged(nameof(TransportType));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineGroupViewModel class constructor. </summary>
        /// <param name="transportTypeLines"> Transport type lines key value pair. </param>
        public LineGroupViewModel(KeyValuePair<TransportType, List<Line>> transportTypeLines)
        {
            TransportType = transportTypeLines.Key;

            Lines = new ObservableCollection<LineViewModel>(transportTypeLines.Value
                .Select(l => new LineViewModel(l)));
        }

        #endregion CLASS METHODS

    }
}
