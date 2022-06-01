using chkam05.ZtmDataDownloader.Data.Base;
using chkam05.ZtmDataDownloader.Data.Static;
using System.Collections.Generic;
using System.Linq;


namespace chkam05.ZtmDataDownloader.Data.Lines
{
    public class LinesResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public Dictionary<TransportType, List<LineItem>> Lines { get; set; }


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
                Lines = new Dictionary<TransportType, List<LineItem>>();
        }

        #endregion CLASS METHODS

    }
}
