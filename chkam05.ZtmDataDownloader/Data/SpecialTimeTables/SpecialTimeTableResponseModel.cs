using chkam05.ZtmDataDownloader.Data.Base;
using chkam05.ZtmDataDownloader.Data.Static;
using System.Collections.Generic;
using System.Linq;


namespace chkam05.ZtmDataDownloader.Data.SpecialTimeTables
{
    public class SpecialTimeTableResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public string Line { get; set; }
        public List<SpecialTimeTableItem> SpecialTimeTables { get; set; }
        public TransportType TransportType { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => SpecialTimeTables != null && SpecialTimeTables.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SpecialTimeTableResponseModel class constructor. </summary>
        public SpecialTimeTableResponseModel() : base()
        {
            if (SpecialTimeTables == null)
                SpecialTimeTables = new List<SpecialTimeTableItem>();
        }

        #endregion CLASS METHODS

    }
}
