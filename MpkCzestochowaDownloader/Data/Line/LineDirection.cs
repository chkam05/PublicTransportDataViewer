using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class LineDirection
    {

        //  VARIABLES

        public string Name { get; set; }
        public List<LineStop> Stops { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDirection class constructor. </summary>
        public LineDirection()
        {
            if (Stops == null)
                Stops = new List<LineStop>();
        }

        #endregion CLASS METHODS

    }
}
