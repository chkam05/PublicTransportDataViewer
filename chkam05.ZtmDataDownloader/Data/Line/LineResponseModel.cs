using chkam05.ZtmDataDownloader.Data.Base;


namespace chkam05.ZtmDataDownloader.Data.Line
{
    public class LineResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public bool HasSpecialTimeTables { get; set; }
        public Line Line { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => Line != null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineResponseModel class constructor. </summary>
        public LineResponseModel() : base()
        {
            HasSpecialTimeTables = false;
        }

        #endregion CLASS METHODS

    }
}
