using chkam05.ZtmDataDownloader.Data.SpecialTimeTables;
using chkam05.ZtmDataDownloader.Exceptions;
using chkam05.ZtmDataDownloader.Serialization;
using System.Net;


namespace chkam05.ZtmDataDownloader.Downloaders
{
    public class SpecialTimeTablesDownloader 
        : BaseDownloader<SpecialTimeTableRequestModel, SpecialTimeTableResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download special time tables data method. </summary>
        /// <param name="request"> Line request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Special time tables response data model. </returns>
        public override SpecialTimeTableResponseModel DownloadData(
            SpecialTimeTableRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(SpecialTimeTableResponseModel));

            return DeserializeData(rawData, request.Line, request.TransportType);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw special time tables data from http response. </summary>
        /// <param name="rawData"> Raw special time tables data from http response. </param>
        /// <returns> Special time tables response data model. </returns>
        private SpecialTimeTableResponseModel DeserializeData(
            string rawData, string line, Data.Static.TransportType transportType)
        {
            var serializer = new SpecialTimeTablesSerializer();
            var result = serializer.Deserialize(rawData, new object[] { line, transportType });

            return result;
        }

    }
}
