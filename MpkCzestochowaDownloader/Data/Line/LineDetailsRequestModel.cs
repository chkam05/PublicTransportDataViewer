using DownloaderCore.Data;
using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class LineDetailsRequestModel : BaseRequestModel
    {

        //  VARIABLES

        public TransportType TransportType { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsRequestModel class constructor. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <param name="lineNumber"> Line number. </param>
        /// <param name="date"> Time table date. </param>
        /// <param name="route"> Route variant. </param>
        public LineDetailsRequestModel(TransportType transportType, string lineNumber, DateTime? date = null, string? route = null)
        {
            TransportType = transportType;
            URL = $"{StaticConfig.LinesURL}?linia={lineNumber}";

            if (date.HasValue)
                URL += $"&data={date.Value.ToString("yyyy-MM-dd")}";

            if (!string.IsNullOrEmpty(route))
                URL += $"&trasa={route}";
        }

        #endregion CLASS METHODS

    }
}
