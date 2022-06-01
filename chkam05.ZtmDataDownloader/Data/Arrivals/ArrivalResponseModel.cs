using chkam05.ZtmDataDownloader.Data.Base;


namespace chkam05.ZtmDataDownloader.Data.Arrivals
{
    public class ArrivalResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public DepartureDetails DepartureDetails { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => DepartureDetails != null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalResponseModel class constructor. </summary>
        public ArrivalResponseModel()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
