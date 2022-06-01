
namespace chkam05.ZtmDataDownloader.Data.Departures
{
    public class DepartureGroup
    {

        //  VARIABLES

        public int Index { get; set; }
        public string Description { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DepartureGroup class constructor. </summary>
        public DepartureGroup()
        {
            //
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            return $"{Index} {Description}";
        }

    }
}
