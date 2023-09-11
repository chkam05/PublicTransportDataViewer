using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataDownloader.Data.Departures
{
    public class DepartureGroup
    {

        //  VARIABLES

        public int Index { get; set; }
        public string Description { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DepartureGroup class constructor. </summary>
        public DepartureGroup()
        {
            //
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            return $"{Index} {Description}";
        }

        #endregion CLASS METHODS

    }
}
