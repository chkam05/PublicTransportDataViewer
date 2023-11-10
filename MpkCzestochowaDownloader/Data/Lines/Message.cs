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

        public DateTime? Date { get; set; }
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

        #endregion CLASS METHODS

    }
}
