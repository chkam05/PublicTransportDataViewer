using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class LineDetailsRequestModel : BaseRequestModel
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsRequestModel class constructor. </summary>
        /// <param name="url"> Request line details url. </param>
        public LineDetailsRequestModel(string url)
        {
            URL = url;
        }

        #endregion CLASS METHODS

    }
}
