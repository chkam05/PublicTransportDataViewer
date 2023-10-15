using DownloaderCore;
using DownloaderCore.Exceptions;
using MpkCzestochowaDownloader.Data.Arrives;
using MpkCzestochowaDownloader.Data.Departures;
using MpkCzestochowaDownloader.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Downloaders
{
    public class LineArrivalsDownloader : BaseDownloader<LineArrivalsRequestModel, LineArrivalsResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download line arrivals data method. </summary>
        /// <param name="request"> Line arrivals request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Line arrivals response data model. </returns>
        public override LineArrivalsResponseModel DownloadData(LineArrivalsRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(LineArrivalsResponseModel));

            return DeserializeData(rawData);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw line arrivals data from http response. </summary>
        /// <param name="rawData"> Raw line arrivals data from http response. </param>
        /// <returns> Line arrivals response data model. </returns>
        private LineArrivalsResponseModel DeserializeData(string rawData)
        {
            var serializer = new LineArrivalsSerializer();
            var result = serializer.Deserialize(rawData);

            return result;
        }

    }
}
