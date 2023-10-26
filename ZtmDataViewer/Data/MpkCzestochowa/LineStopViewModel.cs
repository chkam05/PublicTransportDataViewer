using MpkCzestochowaDownloader.Data.Line;
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
    public class LineStopViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private LineStop _lineStop;
        private ObservableCollection<string> _attributes;


        //  GETTERS & SETTERS

        public LineStop LineStop
        {
            get => _lineStop;
            set
            {
                _lineStop = value;
                OnPropertyChanged(nameof(LineStop));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(URL));
            }
        }

        public string Name
        {
            get => _lineStop.Name;
        }

        public string? URL
        {
            get => _lineStop.URL;
        }

        public ObservableCollection<string> Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                _attributes.CollectionChanged += OnAttributesCollectionChanged;
                OnPropertyChanged(nameof(Attributes));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineStopViewModel class constructor. </summary>
        /// <param name="lineStop"> Line stop. </param>
        public LineStopViewModel(LineStop lineStop)
        {
            LineStop = lineStop;
            Attributes = new ObservableCollection<string>(lineStop.Attributes);
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
        /// <summary> Method invoked after attributes collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnAttributesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Attributes));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
