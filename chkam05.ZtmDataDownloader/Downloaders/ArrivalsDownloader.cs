using chkam05.ZtmDataDownloader.Data.Arrivals;
using chkam05.ZtmDataDownloader.Exceptions;
using chkam05.ZtmDataDownloader.Serialization;
using System.Net;


namespace chkam05.ZtmDataDownloader.Downloaders
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
