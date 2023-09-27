using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class Direction
    {

        //  VARIABLES

        public string Name { get; set; }
        public Dictionary<string, string> Stops { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Direction class constructor. </summary>
        public Direction()
        {
            if (Stops == null)
                Stops = new Dictionary<string, string>();
        }

        #endregion CLASS METHODS

    }
}
