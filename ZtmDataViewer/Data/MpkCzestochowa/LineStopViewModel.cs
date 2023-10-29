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


        //  GETTERS & SETTERS

        public LineStop LineStop
        {
            get => _lineStop;
            set
            {
                _lineStop = value;
                OnPropertyChanged(nameof(LineStop));
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Name
        {
            get => _lineStop.Name;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineStopViewModel class constructor. </summary>
        /// <param name="lineStop"> Line stop. </param>
        public LineStopViewModel(LineStop lineStop)
        {
            LineStop = lineStop;
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

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
