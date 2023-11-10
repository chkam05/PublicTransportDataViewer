using MpkCzestochowaDownloader.Data.Departures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.MpkCzestochowa
{
    public class OtherLineViewModel : BaseViewModel
    {

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

    }
}
