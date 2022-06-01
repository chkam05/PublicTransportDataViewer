using System.Windows.Media;


namespace chkam05.ZtmDataDownloader.Data.Global
{
    public class City
    {

        //  CONST

        private static readonly Color _defaultColor = Colors.White;


        //  VARIABLES

        public string Color { get; set; }
        public string Name { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> City class constructor. </summary>
        public City()
        {
            //
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get city color. </summary>
        /// <returns> Line color. </returns>
        public Color GetColor()
        {
            if (string.IsNullOrEmpty(this.Color))
                return _defaultColor;

            try
            {
                return (Color)ColorConverter.ConvertFromString(Color);
            }
            catch
            {
                return _defaultColor;
            }
        }

    }
}
