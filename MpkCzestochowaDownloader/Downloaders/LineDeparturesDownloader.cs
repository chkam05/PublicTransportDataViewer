using DownloaderCore.Exceptions;
using DownloaderCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MpkCzestochowaDownloader.Data.Departures;
using MpkCzestochowaDownloader.Serializers;

namespace MpkCzestochowaDownloader.Downloaders
{
    public class LineDeparturesDownloader : BaseDownloader<LineDeparturesRequestModel, LineDeparturesResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download line departures data method. </summary>
        /// <param name="request"> Line departures request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Line departures response data model. </returns>
        public override LineDeparturesResponseModel DownloadData(LineDeparturesRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(LineDeparturesResponseModel));

            return DeserializeData(rawData, request.URL);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw line departures data from http response. </summary>
        /// <param name="rawData"> Raw line departures data from http response. </param>
        /// <param name="parameters"> Additional deserialize parameters. </param>
        /// <returns> Line departures response data model. </returns>
        protected override LineDeparturesResponseModel DeserializeData(string rawData, params object[] parameters)
        {
            var serializer = new LineDeparturesSerializer();
            var result = serializer.Deserialize(rawData, parameters);

            result.URL = (string)parameters[0];

            return result;
        }

    }
}
