using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class LineDetails
    {

        //  VARIABLES

        public Dictionary<string, string> Dates { get; set; }
        public string DirectionFrom { get; set; }
        public string DirectionTo { get; set; }
        public List<Direction> Directions { get; set; }
        public Dictionary<string, string> Relations { get; set; }
        public string Value { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetails class constructor. </summary>
        public LineDetails()
        {
            if (Dates == null)
                Dates = new Dictionary<string, string>();

            if (Directions == null)
                Directions = new List<Direction>();

            if (Relations == null)
                Relations = new Dictionary<string, string>();
        }

        #endregion CLASS METHODS

    }
}
