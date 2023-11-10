using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.MpkCzestochowa
{
    public class LineStopViewModel : BaseViewModel
    {

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

    }
}
