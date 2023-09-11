using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloaderCore.Data
{
    public class BaseRequestModel
    {

        //  VARIABLES

        public string URL { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> BaseRequestModel class constructor. </summary>
        public BaseRequestModel()
        {
            //
        }

        //  --------------------------------------------------------------------------------
        /// <summary> BaseRequestModel class constructor. </summary>
        /// <param name="url"> Request URL. </param>
        public BaseRequestModel(string url)
        {
            URL = url;
        }

        #endregion CLASS METHODS

    }
}
