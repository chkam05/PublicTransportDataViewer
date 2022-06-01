using System.Collections.Generic;
using System.Windows.Media;


namespace chkam05.ZtmDataDownloader.Data.Arrivals
{
    public class Arrival
    {

        //  CONST

        private static readonly Color _defaultColor = Colors.White;


        //  VARIABLES

        public string AdditionalInfo { get; set; }
        public string Name { get; set; }
        public List<ArrivalLink> Links { get; set; }
        public string Color { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string FullTime { get; set; }
        public string PartTime { get; set; }
        public string FullDistance { get; set; }
        public string PartDistance { get; set; }


        //  GETTERS & SETTERS

        public bool HasSpecialTime
        {
            get => !string.IsNullOrEmpty(FullTime);
        }

        public string SpecialTime
        {
            get
            {
                return HasSpecialTime
                    ? $"+{FullTime} min (+ {PartTime} min)"
                    : "-";
            }
        }

        public bool HasSpecialDsitance
        {
            get => !string.IsNullOrEmpty(FullDistance);
        }

        public string SpecialDistance
        {
            get
            {
                return HasSpecialDsitance
                    ? $"+{FullDistance} km (+ {PartDistance} km)"
                    : "-";
            }
        }

        public string Value
        {
            get => ToString();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Arrival class constructor. </summary>
        public Arrival()
        {
            if (Links == null)
                Links = new List<ArrivalLink>();
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get line stop color. </summary>
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

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            var hour = Hour < 10 ? $"0{Hour}" : $"{Hour}";
            var min = Minute < 10 ? $"0{Minute}" : $"{Minute}";

            return $"{hour}:{min}";
        }

    }
}
