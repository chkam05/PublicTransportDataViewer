using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class LineViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private Line _line;


        //  GETTERS & SETTERS

        public Line Line
        {
            get => _line;
            set
            {
                _line = value;
                OnPropertyChanged(nameof(Line));
                OnPropertyChanged(nameof(TransportType));
                OnPropertyChanged(nameof(Value));
            }
        }

        public TransportType TransportType
        {
            get => _line.TransportType;
        }

        public string Value
        {
            get => _line.Value;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineViewModel class constructor. </summary>
        /// <param name="line"> Line data. </param>
        public LineViewModel(Line line)
        {
            Line = line;
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
