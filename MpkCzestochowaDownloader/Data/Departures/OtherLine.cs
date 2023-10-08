using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace MpkCzestochowaDownloader.Data.Departures
{
    public class OtherLine
    {

        //  VARIABLES

        public List<string> Attributes { get; set; }
        public string URL { get; set; }
        public string Value { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> OtherLine class constructor. </summary>
        public OtherLine()
        {
            if (Attributes == null)
                Attributes = new List<string>();
        }

        #endregion CLASS METHODS

    }
}
