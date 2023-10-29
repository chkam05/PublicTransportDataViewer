using MpkCzestochowaDownloader.Data.Departures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class OtherLineViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private OtherLine _otherLine;


        //  GETTERS & SETTERS

        public OtherLine OtherLine
        {
            get => _otherLine;
            set
            {
                _otherLine = value;
                OnPropertyChanged(nameof(OtherLine));
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Value
        {
            get => _otherLine.Value;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> OtherLineViewModel class constructor. </summary>
        /// <param name="otherLine"> Line departures data object. </param>
        public OtherLineViewModel(OtherLine otherLine)
        {
            OtherLine = otherLine;
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
