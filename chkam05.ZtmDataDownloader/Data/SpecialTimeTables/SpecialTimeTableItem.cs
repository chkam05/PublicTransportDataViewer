using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace chkam05.ZtmDataDownloader.Data.SpecialTimeTables
{
    public class SpecialTimeTableItem
    {

        //  CONST

        private static readonly string _dateFormat = "dd.MM.yyyy";
        private static readonly CultureInfo _dateFormatProvider = CultureInfo.InvariantCulture;
        private static readonly DateTimeStyles _dateTimeStyle = DateTimeStyles.None;


        //  VARIABLES

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string URL { get; set; }
        public string Value { get; set; }


        //  GETTERS & SETTERS

        public string ID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts.LastOrDefault();

                return null;
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SpecialTimeTableItem class constructor. </summary>
        public SpecialTimeTableItem()
        {
            StartDate = null;
            EndDate = null;
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Set start date from string. </summary>
        /// <param name="startDate"> Start date as string. </param>
        public void SetStartDate(string startDate)
        {
            if (!string.IsNullOrEmpty(startDate))
                if (DateTime.TryParseExact(startDate, _dateFormat, _dateFormatProvider, _dateTimeStyle, out DateTime result))
                    StartDate = result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Set end date from string. </summary>
        /// <param name="endDate"> End date as string. </param>
        public void SetEndDate(string endDate)
        {
            if (!string.IsNullOrEmpty(endDate))
                if (DateTime.TryParseExact(endDate, _dateFormat, _dateFormatProvider, _dateTimeStyle, out DateTime result))
                    EndDate = result;
        }

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Spearate Timetable url to get identifiers. </summary>
        /// <returns> List of identifiers. </returns>
        private List<string> SeparateUrl()
        {
            if (!string.IsNullOrEmpty(URL))
                return URL.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return new List<string>();
        }

        #endregion UTILITY METHODS

    }
}
