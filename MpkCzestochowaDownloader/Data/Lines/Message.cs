using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Lines
{
    public class Message
    {

        //  CONST

        private readonly static string _dateFormat = "yyyy-MM-dd";


        //  VARIABLES

        public string? Date { get; set; }
        public List<string> Lines { get; set; }
        public string? URL { get; set; }
        public string? Value { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Message class constructor. </summary>
        public Message()
        {
            if (Lines == null)
                Lines = new List<string>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            return $"{(!string.IsNullOrEmpty(Date) ? Date : "Missing date")}: " +
                $"{(!string.IsNullOrEmpty(Value) ? Value : "No message")}";
        }

        #endregion CLASS METHODS

        #region GET METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get message date as DateTime. </summary>
        /// <returns> Message date as DateTime. </returns>
        public DateTime? GetDateTime()
        {
            if (!string.IsNullOrEmpty(Date))
            {
                var culture = CultureInfo.InvariantCulture;
                var style = DateTimeStyles.None;

                return DateTime.TryParseExact(Date, _dateFormat, culture, style, out DateTime dateTime)
                    ? dateTime : null;
            }

            return null;
        }

        #endregion GET METHODS

    }
}
