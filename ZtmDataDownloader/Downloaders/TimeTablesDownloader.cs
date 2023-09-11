using DownloaderCore;
using DownloaderCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Data.TimeTables;
using ZtmDataDownloader.Serializers;

namespace ZtmDataDownloader.Downloaders
{
    public class TimeTablesDownloader : BaseDownloader<TimeTableRequestModel, TimeTableResponseModel>
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Download special time tables data method. </summary>
        /// <param name="request"> Line request data model. </param>
        /// <param name="parameters"> Additional request parameters. </param>
        /// <returns> Special time tables response data model. </returns>
        public override TimeTableResponseModel DownloadData(
            TimeTableRequestModel request, params object[] parameters)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(request.URL);

            string rawData = PerformRequest(httpRequest);

            if (rawData == null)
                throw new DataSerializationException(typeof(TimeTableResponseModel));

            return DeserializeData(rawData, request.Line, request.TransportType);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw special time tables data from http response. </summary>
        /// <param name="rawData"> Raw special time tables data from http response. </param>
        /// <returns> Special time tables response data model. </returns>
        private TimeTableResponseModel DeserializeData(
            string rawData, string line, Data.Static.TransportType transportType)
        {
            var serializer = new TimeTablesSerializer();
            var result = serializer.Deserialize(rawData, new object[] { line, transportType });

            return result;
        }

    }
}
