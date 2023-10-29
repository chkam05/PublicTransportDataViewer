using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZtmDataDownloader.Data.Global;

namespace ZtmDataViewer.Data.ZtmData
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
            private set
            {
                _lineStop = value;
                OnPropertyChanged(nameof(City));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(IsOptional));
                OnPropertyChanged(nameof(Platform));
                OnPropertyChanged(nameof(PlatformDescription));
            }
        }

        public string City
        {
            get => _lineStop.City;
        }

        public string Name
        {
            get => _lineStop.Name;
        }

        public bool IsOptional
        {
            get => _lineStop.Optional;
        }

        public string Platform
        {
            get => _lineStop.Platform;
        }

        public string PlatformDescription
        {
            get => _lineStop.PlatformDescription;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineStopViewModel class constructor. </summary>
        /// <param name="lineStop"> Line stop data. </param>
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
