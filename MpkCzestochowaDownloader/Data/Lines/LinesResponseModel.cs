using DownloaderCore.Data;
using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Lines
{
    public class LinesResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public Dictionary<TransportType, List<Line>> Lines { get; set; }
        public List<Message> Messages { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => Lines != null && Lines.Any();
        }

        public bool HasMessages
        {
            get => Messages != null && Messages.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LinesResponseModel class constructor. </summary>
        public LinesResponseModel() : base()
        {
            if (Lines == null)
                Lines = new Dictionary<TransportType, List<Line>>();

            if (Messages == null)
                Messages = new List<Message>();
        }

        #endregion CLASS METHODS

    }
}
