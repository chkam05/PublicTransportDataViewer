using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class LineStop
    {

        //  VARIABLES

        public List<string> Attributes { get; set; }
        public string Name { get; set; }
        public string? URL { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineStop class constructor. </summary>
        public LineStop()
        {
            if (Attributes == null)
                Attributes = new List<string>();
        }

        #endregion CLASS METHODS

    }
}
