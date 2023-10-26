using DownloaderCore.Exceptions;
using DownloaderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Serializers;
using MpkCzestochowaDownloader.Data.Static;

namespace MpkCzestochowaDownloader.Downloaders
{
    public class LineDetailsDownloader : BaseDownloader<LineDetailsRequestModel, LineDetailsResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download line details data method. </summary>
        /// <param name="request"> Line details request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Line details response data model. </returns>
        public override LineDetailsResponseModel DownloadData(LineDetailsRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(LineDetailsResponseModel));

            return DeserializeData(rawData, request.TransportType);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw line details data from http response. </summary>
        /// <param name="rawData"> Raw line details data from http response. </param>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Line details response data model. </returns>
        private LineDetailsResponseModel DeserializeData(string rawData, TransportType transportType)
        {
            var serializer = new LineDetailsSerializer();
            var result = serializer.Deserialize(rawData);

            if (result.HasData && !result.HasErrors)
            {
                result.LineDetails.TransportType = transportType;
            }

            return result;
        }

    }
}
