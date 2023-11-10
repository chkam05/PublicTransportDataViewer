using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class TimeTableDate
    {

        //  VARIABLES

        public DateTime? Date { get; set; }
        public bool Selected { get; set; }
        public string Title { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TimeTableDate class constructor. </summary>
        public TimeTableDate()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
