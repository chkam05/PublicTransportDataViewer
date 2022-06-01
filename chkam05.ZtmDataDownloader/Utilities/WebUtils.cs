using System.Net;


namespace chkam05.ZtmDataDownloader.Utilities
{
    internal class WebUtils
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get http status code from WebException. </summary>
        /// <param name="exception"> WebException. </param>
        /// <returns> Http status code. </returns>
        public static int GetWebExceptionStatusCode(WebException exception)
        {
            return (int)((HttpWebResponse)exception.Response).StatusCode;
        }

    }
}
