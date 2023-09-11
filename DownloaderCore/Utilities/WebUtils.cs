using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DownloaderCore.Utilities
{
    public class WebUtils
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get http status code from WebException. </summary>
        /// <param name="exception"> WebException. </param>
        /// <returns> Http status code. </returns>
        public static int GetWebExceptionStatusCode(WebException exception)
        {
            var response = (HttpWebResponse?)exception.Response;
            return (int)(response?.StatusCode ?? HttpStatusCode.InternalServerError);
        }

    }
}
