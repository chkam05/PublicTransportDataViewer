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

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsRequestModel class constructor. </summary>
        /// <param name="lineNumber"> Line number. </param>
        public LineDetailsRequestModel(string lineNumber, DateTime? date = null, string? route = null)
        {
            URL = $"{StaticConfig.LinesURL}?linia={lineNumber}";

            if (date.HasValue)
                URL += $"&data={date.Value.ToString("yyyy-MM-dd")}";

            if (!string.IsNullOrEmpty(route))
                URL += $"&trasa={route}";
        }

        #endregion CLASS METHODS

    }
}
