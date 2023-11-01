using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class TimeTableDateViewModel : BaseViewModel
    {

        //  VARIABLES

        private TimeTableDate _timeTableDate;


        //  GETTERS & SETTERS

        public TimeTableDate TimeTableDate
        {
            get => _timeTableDate;
            set
            {
                _timeTableDate = value;
                OnPropertyChanged(nameof(TimeTableDate));
                OnPropertyChanged(nameof(Date));
            }
        }

        public string? Date
        {
            get => _timeTableDate.Date?.ToString("yyyy-MM-dd") ?? null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TimeTableDateViewModel class constructor. </summary>
        /// <param name="timeTableDate"> Time table date. </param>
        public TimeTableDateViewModel(TimeTableDate timeTableDate)
        {
            TimeTableDate = timeTableDate;
        }

        #endregion CLASS METHODS

    }
}
