using System;
using System.Globalization;


namespace chkam05.ZtmDataDownloader.Data.Global
{
    public class Message
    {

        //  CONST

        private static readonly string _dateFormat = "dd.MM.yyyy";
        private static readonly string _dateTimeFormat = "dd.MM.yyyy HH:mm";
        private static readonly CultureInfo _dateFormatProvider = CultureInfo.InvariantCulture;
        private static readonly DateTimeStyles _dateTimeStyle = DateTimeStyles.None;


        //  VARIABLES

        public string DateDescription { get; set; }
        public string MessageText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string URL { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Message class constructor. </summary>
        public Message()
        {
            //
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Set start date from string. </summary>
        /// <param name="startDate"> Start date as string. </param>
        public void SetStartDate(string startDate)
        {
            DateTime result;

            if (!string.IsNullOrEmpty(startDate))
            {
                if (DateTime.TryParseExact(startDate, _dateTimeFormat, _dateFormatProvider, _dateTimeStyle, out result))
                    StartDate = result;
                else if (DateTime.TryParseExact(startDate, _dateFormat, _dateFormatProvider, _dateTimeStyle, out result))
                    StartDate = result;
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Set end date from string. </summary>
        /// <param name="endDate"> End date as string. </param>
        public void SetEndDate(string endDate)
        {
            DateTime result;

            if (!string.IsNullOrEmpty(endDate))
            {
                if (DateTime.TryParseExact(endDate, _dateTimeFormat, _dateFormatProvider, _dateTimeStyle, out result))
                    EndDate = result;
                else if (DateTime.TryParseExact(endDate, _dateFormat, _dateFormatProvider, _dateTimeStyle, out result))
                    EndDate = result;
            }
        }

    }
}
