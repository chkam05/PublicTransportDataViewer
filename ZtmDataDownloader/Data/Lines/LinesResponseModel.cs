using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ZtmDataDownloader.Data.Static;

namespace ZtmDataDownloader.Data.Lines
{
    public class LinesResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public Dictionary<TransportType, List<Line>> Lines { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => Lines != null && Lines.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LinesResponseModel class constructor. </summary>
        public LinesResponseModel() : base()
        {
            if (Lines == null)
                Lines = new Dictionary<TransportType, List<Line>>();
        }

        #endregion CLASS METHODS

    }
}
