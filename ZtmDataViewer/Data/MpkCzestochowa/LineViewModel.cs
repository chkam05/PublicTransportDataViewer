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
    public class LineViewModel : BaseViewModel
    {

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
                OnPropertyChanged(nameof(Value));
            }
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

    }
}
