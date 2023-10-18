using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Arrives
{
    public class Arrival
    {

        //  VARIABLES

        public int Id { get; set; }
        public string? StopName { get; set; }
        public DateTime? Time { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Arrival class constructor. </summary>
        public Arrival()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
