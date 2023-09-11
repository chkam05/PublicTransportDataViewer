using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Static;

namespace ZtmDataDownloader.Data.Lines
{
    public class LinesRequestModel : BaseRequestModel
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LinesRequestModel class constructor. </summary>
        public LinesRequestModel()
        {
            URL = StaticConfig.ZtmLinesURL;
        }

        #endregion CLASS METHODS

    }
}
