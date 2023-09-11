using DownloaderCore.Data;
using DownloaderCore.Exceptions;
using DownloaderCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DownloaderCore
{
    public class BaseDownloader<TRequest, TResponse>
        where TRequest : BaseRequestModel
        where TResponse : BaseResponseModel
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> BaseDownloader class constructor. </summary>
        public BaseDownloader()
        {
            //
        }

        #endregion CLASS METHODS

        #region INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Overridable downloading data method. </summary>
        /// <param name="request"> Data request model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Response data model. </returns>
        public virtual TResponse DownloadData(TRequest request, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        #endregion INTERACTION METHODS

        #region WEB REQUEST PERFORM METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Perform base http request. </summary>
        /// <param name="request"> Http request to perform. </param>
        /// <returns> Request result. </returns>
        protected string PerformRequest(HttpWebRequest request)
        {
            string result;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    result = reader.ReadToEnd();

                    reader.Close();
                    dataStream.Close();

                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Found)
                {
                    throw new DataDownloadException(
                        $"Data not found for this particular request.", (int)response.StatusCode);
                }
                else
                {
                    throw new DataDownloadException(
                        $"Request failed with status code: {(int)response.StatusCode}.", (int)response.StatusCode);
                }
            }
            catch (WebException exc)
            {
                if (exc.Response != null)
                {
                    int stausCode = WebUtils.GetWebExceptionStatusCode(exc);

                    throw new DataDownloadException(
                        $"Request failed with status code: {stausCode}.", stausCode);
                }
                else
                {
                    throw new DataDownloadException(
                        $"Unable to perform request, {exc.Message}", -1);
                }
            }
        }

        #endregion WEB REQUEST PERFORM METHODS

    }
}
