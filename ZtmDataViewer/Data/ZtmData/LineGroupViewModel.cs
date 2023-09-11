using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.Static;

namespace ZtmDataViewer.Data.ZtmData
{
    public class LineGroupViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


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
                _lines.CollectionChanged += OnLinesCollectionChanged;
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
        /// <summary> Method invoked after lines collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnLinesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Lines));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
