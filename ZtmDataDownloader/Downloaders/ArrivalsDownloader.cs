using DownloaderCore;
using DownloaderCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Arrivals;
using ZtmDataDownloader.Serializers;

namespace ZtmDataDownloader.Downloaders
{
    public class ArrivalsDownloader : BaseDownloader<ArrivalRequestModel, ArrivalResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download line data method. </summary>
        /// <param name="request"> Line request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Line response data model. </returns>
        public override ArrivalResponseModel DownloadData(ArrivalRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(ArrivalResponseModel));

            return DeserializeData(rawData);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data from http response. </summary>
        /// <param name="rawData"> Raw lines data from http response. </param>
        /// <returns> Arrivals response data model. </returns>
        private ArrivalResponseModel DeserializeData(string rawData)
        {
            var serializer = new ArrivalsSerializer();
            var result = serializer.Deserialize(rawData, new object[] { });

            return result;
        }

    }
}
