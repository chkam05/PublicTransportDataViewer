using chkam05.ZtmDataDownloader.Data.Lines;
using chkam05.ZtmDataDownloader.Exceptions;
using chkam05.ZtmDataDownloader.Serialization;
using System.Net;


namespace chkam05.ZtmDataDownloader.Downloaders
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

            return DeserializeData(rawData);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data from http response. </summary>
        /// <param name="rawData"> Raw lines data from http response. </param>
        /// <returns> Lines response data model. </returns>
        private LinesResponseModel DeserializeData(string rawData)
        {
            var serializer = new LinesSerializer();
            var result = serializer.Deserialize(rawData);

            return result;
        }

    }
}
