using chkam05.ZtmDataDownloader.Data.Departures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;


namespace chkam05.ZtmDataDownloader.Data.Global
{
    public class LineStop
    {

        //  CONST

        private static readonly Color _defaultColor = Colors.White;
        private static readonly string _nonPrimaryAttribute = "direction-non-primary";
        private static readonly int _optionalIndexLength = 5;


        //  VARIABLES

        public List<string> Attributes { get; set; }
        public string Color { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public bool Optional { get; set; } = false;
        public string Platform { get; set; }
        public string PlatformDescription { get; set; }
        public string URL { get; set; }

        public Dictionary<DepartureGroup, List<Departure>> Departures { get; set; }


        //  GETTERS & SETTERS

        public string ID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - GetIdShift(1, urlParts)];

                return null;
            }
        }

        public string DirectionID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - GetIdShift(2, urlParts)];

                return null;
            }
        }

        public string TimeTableID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - GetIdShift(3, urlParts)];

                return null;
            }
        }

        public bool NonPrimaryDirection
        {
            get => Attributes.Contains(_nonPrimaryAttribute);
        }

        public bool HasOptionalId
        {
            get => SeparateUrl()?.Count > _optionalIndexLength;
        }

        public string OptionalId
        {
            get
            {
                if (HasOptionalId)
                {
                    var urlParts = SeparateUrl();

                    if (urlParts.Any())
                        return urlParts[urlParts.Count - 1];
                }

                return null;
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineStop class constructor. </summary>
        public LineStop()
        {
            if (Attributes == null)
                Attributes = new List<string>();
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
        /// <summary> Map city by stop color. </summary>
        /// <param name="cities"> List of cities. </param>
        public void MapCity(List<City> cities)
        {
            City = cities.FirstOrDefault(c => c.Color == Color)?.Name;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            return $"{Name} {Platform}";
        }

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get shifted id place from urlParts. </summary>
        /// <param name="id"> Id position. </param>
        /// <param name="urlParts"> Url parts. </param>
        /// <returns> Shifted or not id place in urlParts. </returns>
        private int GetIdShift(int id, List<string> urlParts)
        {
            if (urlParts.Count > _optionalIndexLength)
                return id + (urlParts.Count - _optionalIndexLength);
            return id;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Spearate Timetable url to get identifiers. </summary>
        /// <returns> List of identifiers. </returns>
        private List<string> SeparateUrl()
        {
            if (!string.IsNullOrEmpty(URL))
                return URL.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return new List<string>();
        }

        #endregion UTILITY METHODS

    }
}
