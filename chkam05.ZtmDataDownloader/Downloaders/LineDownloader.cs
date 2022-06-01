using chkam05.ZtmDataDownloader.Data.Line;
using chkam05.ZtmDataDownloader.Exceptions;
using chkam05.ZtmDataDownloader.Serialization;
using System.Net;


namespace chkam05.ZtmDataDownloader.Downloaders
{
    public class LineDownloader : BaseDownloader<LineRequestModel, LineResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download line data method. </summary>
        /// <param name="request"> Line request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Line response data model. </returns>
        public override LineResponseModel DownloadData(LineRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(LineResponseModel));

            return DeserializeData(rawData, request.TransportType);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data from http response. </summary>
        /// <param name="rawData"> Raw lines data from http response. </param>
        /// <returns> Lines response data model. </returns>
        private LineResponseModel DeserializeData(string rawData, Data.Static.TransportType transportType)
        {
            var serializer = new LineSerializer();
            var result = serializer.Deserialize(rawData, new object[] { transportType });

            return result;
        }

    }
}
