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

        public string Title { get; set; }
        public DateTime? Date { get; set; }


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
