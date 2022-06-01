using System;


namespace chkam05.ZtmDataDownloader.Exceptions
{
    public class DataDownloadException : Exception
    {

        //  VARIABLES

        public int StatusCode { get; private set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DataDownloadException class constructor. </summary>
        /// <param name="message"> Exception message. </param>
        /// <param name="code"> Http response status code. </param>
        public DataDownloadException(string message, int code) : base(message)
        {
            StatusCode = code;
        }

        #endregion CLASS METHODS

    }
}
