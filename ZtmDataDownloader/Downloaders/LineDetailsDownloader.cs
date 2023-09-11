using DownloaderCore;
using DownloaderCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Serializers;

namespace ZtmDataDownloader.Downloaders
{
    public class LineDetailsDownloader : BaseDownloader<LineDetailsRequestModel, LineDetailsResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download line data method. </summary>
        /// <param name="request"> Line request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Line response data model. </returns>
        public override LineDetailsResponseModel DownloadData(LineDetailsRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(LineDetailsResponseModel));

            return DeserializeData(rawData, request.TransportType);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data from http response. </summary>
        /// <param name="rawData"> Raw lines data from http response. </param>
        /// <returns> Lines response data model. </returns>
        private LineDetailsResponseModel DeserializeData(string rawData, Data.Static.TransportType transportType)
        {
            var serializer = new LineDetailsSerializer();
            var result = serializer.Deserialize(rawData, new object[] { transportType });

            return result;
        }

    }
}
