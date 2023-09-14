using DownloaderCore.Data;
using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Lines
{
    public class LinesRequestModel : BaseRequestModel
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LinesRequestModel class constructor. </summary>
        public LinesRequestModel()
        {
            URL = StaticConfig.LinesURL;
        }

        #endregion CLASS METHODS

    }
}
