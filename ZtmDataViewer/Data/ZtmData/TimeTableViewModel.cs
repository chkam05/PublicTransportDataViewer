using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.TimeTables;

namespace PublicTransportDataViewer.Data.ZtmData
{
    public class TimeTableViewModel : BaseViewModel
    {

        //  VARIABLES

        private TimeTable _timeTable;


        //  GETTERS & SETTERS

        public TimeTable TimeTable
        {
            get => _timeTable;
            private set
            {
                _timeTable = value;
                OnPropertyChanged(nameof(TimeTable));
                OnPropertyChanged(nameof(HasDates));
                OnPropertyChanged(nameof(HasStartDate));
                OnPropertyChanged(nameof(HasEndDate));
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool HasDates
        {
            get => _timeTable.StartDate.HasValue || _timeTable.EndDate.HasValue;
        }

        public bool HasStartDate
        {
            get => _timeTable.StartDate.HasValue;
        }

        public bool HasEndDate
        {
            get => _timeTable.EndDate.HasValue;
        }

        public string? StartDate
        {
            get => _timeTable.StartDate?.ToString("yyyy.MM.dd");
        }

        public string? EndDate
        {
            get => _timeTable.EndDate?.ToString("yyyy.MM.dd");
        }

        public string Value
        {
            get => _timeTable.Value;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TimeTableViewModel class constructor. </summary>
        /// <param name="timeTable"> Time table data. </param>
        public TimeTableViewModel(TimeTable timeTable)
        {
            TimeTable = timeTable;
        }

        #endregion CLASS METHODS

    }
}
