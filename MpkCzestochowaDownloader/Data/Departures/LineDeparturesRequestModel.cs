using DownloaderCore.Data;
using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Departures
{
    public class LineDeparturesRequestModel : BaseRequestModel
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDeparturesRequestModel class constructor. </summary>
        /// <param name="url"> Line stop url. </param>
        public LineDeparturesRequestModel(string url)
        {
            URL = url;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> LineDeparturesRequestModel class constructor. </summary>
        /// <param name="lineNumber"> Line number. </param>
        /// <param name="lineStopId"> Line stop identifier. </param>
        /// <param name="directionId"> Direction identifier. </param>
        /// <param name="routeVaraintId"> Route variant identifier. </param>
        public LineDeparturesRequestModel(string lineNumber, string lineStopId, string directionId, string routeVaraintId)
        {
            URL = $"{StaticConfig.DeparturesURL}?linia={lineNumber}" +
                $"&przystanek={lineStopId}&kierunek={directionId}&trasa={routeVaraintId}";
        }

        #endregion CLASS METHODS

    }
}
