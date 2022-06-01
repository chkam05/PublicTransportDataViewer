using chkam05.ZtmDataDownloader.Data.Departures;
using chkam05.ZtmDataDownloader.Exceptions;
using chkam05.ZtmDataDownloader.Serialization;
using System.Net;


namespace chkam05.ZtmDataDownloader.Downloaders
{
    public class DeparturesDownloader : BaseDownloader<DeparturesRequestModel, DeparturesResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download line data method. </summary>
        /// <param name="request"> Line request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Departures response data model. </returns>
        public override DeparturesResponseModel DownloadData(DeparturesRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(DeparturesResponseModel));

            return DeserializeData(rawData, request.TransportType);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data from http response. </summary>
        /// <param name="rawData"> Raw lines data from http response. </param>
        /// <returns> Departures response data model. </returns>
        private DeparturesResponseModel DeserializeData(string rawData, Data.Static.TransportType transportType)
        {
            var serializer = new DeparturesSerializer();
            var result = serializer.Deserialize(rawData, new object[] { transportType });

            return result;
        }

    }
}
