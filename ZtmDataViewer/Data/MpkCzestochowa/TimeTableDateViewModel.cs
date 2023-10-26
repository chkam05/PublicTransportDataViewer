using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class TimeTableDateViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


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
                OnPropertyChanged(nameof(IsSelected));
                OnPropertyChanged(nameof(Title));
            }
        }

        public string? Date
        {
            get => _timeTableDate.Date?.ToString("yyyy-MM-dd") ?? null;
        }

        public bool IsSelected
        {
            get => _timeTableDate.Selected;
        }

        public string Title
        {
            get => _timeTableDate.Title;
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
