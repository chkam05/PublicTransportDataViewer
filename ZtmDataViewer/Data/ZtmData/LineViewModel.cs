using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.Static;
using ZtmDataViewer.Utilities;

namespace ZtmDataViewer.Data.ZtmData
{
    public class LineViewModel : BaseViewModel
    {

        //  VARIABLES

        private Line _line;


        //  GETTERS & SETTERS

        public Line Line
        {
            get => _line;
            private set
            {
                _line = value;
                OnPropertyChanged(nameof(Line));
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(IsPathChanged));
                OnPropertyChanged(nameof(IsUpdated));
                OnPropertyChanged(nameof(Value));
            }
        }

        public Color Color
        {
            get => _line.GetColor();
        }

        public string Description
        {
            get => _line.Description;
        }

        public bool IsPathChanged
        {
            get => _line.IsPathChanged;
        }

        public bool IsUpdated
        {
            get => _line.IsUpdated;
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
