using DownloaderCore;
using DownloaderCore.Exceptions;
using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Downloaders
{
    public class LinesDownloader : BaseDownloader<LinesRequestModel, LinesResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download lines data method. </summary>
        /// <param name="request"> Lines request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Lines response data model. </returns>
        public override LinesResponseModel DownloadData(LinesRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(LinesResponseModel));

            return DeserializeData(rawData, request.URL);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data from http response. </summary>
        /// <param name="rawData"> Raw lines data from http response. </param>
        /// <param name="parameters"> Additional deserialize parameters. </param>
        /// <returns> Lines response data model. </returns>
        protected override LinesResponseModel DeserializeData(string rawData, params object[] parameters)
        {
            var serializer = new LinesSerializer();
            var result = serializer.Deserialize(rawData);

            result.URL = (string)parameters[0];

            return result;
        }

    }
}
