using System.Collections.Generic;


namespace chkam05.ZtmDataDownloader.Data.Global
{
    public class LineDirection
    {

        //  VARIABLES

        public string Direction { get; set; }
        public List<City> Cities { get; set; }
        public List<LineStop> Stops { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDirection class constructor. </summary>
        public LineDirection()
        {
            if (Cities == null)
                Cities = new List<City>();

            if (Stops == null)
                Stops = new List<LineStop>();
        }

        #endregion CLASS METHODS

    }
}
