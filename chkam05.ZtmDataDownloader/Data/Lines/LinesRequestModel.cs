using chkam05.ZtmDataDownloader.Data.Base;


namespace chkam05.ZtmDataDownloader.Data.Lines
{
    public class LinesRequestModel : BaseRequestModel
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LinesRequestModel class constructor. </summary>
        public LinesRequestModel()
        {
            URL = StaticConfig.ZtmLinesURL;
        }

        #endregion CLASS METHODS

    }
}
