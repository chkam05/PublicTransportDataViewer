using chkam05.ZtmDataDownloader.Data.Arrivals;
using System;
using System.Collections.Generic;
using System.Linq;


namespace chkam05.ZtmDataDownloader.Data.Departures
{
    public class Departure
    {

        //  VARIABLES

        public DepartureGroup Group { get; set; }
        public string Description { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public bool LowFloor { get; set; }
        public string URL { get; set; }
        public string Variant { get; set; } = null;

        public DepartureDetails DepartureDetails { get; set; }


        //  GETTERS & SETTERS

        public int GroupID
        {
            get => Group?.Index ?? -1;
        }

        public string GroupDescription
        {
            get => Group?.Description ?? "No group";
        }

        public bool IsLowFloor
        {
            get => LowFloor;
        }

        public bool IsVariant
        {
            get => !string.IsNullOrEmpty(Variant);
        }

        public string Value
        {
            get => ToString();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Departure class constructor. </summary>
        public Departure()
        {
            //
        }

        #endregion CLASS METHODS

        public string DirectID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - 1];

                return null;
            }
        }

        public string ID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - 2];

                return null;
            }
        }

        public string StopID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - 3];

                return null;
            }
        }

        public string DirectionID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - 4];

                return null;
            }
        }

        public string TimeTableID
        {
            get
            {
                var urlParts = SeparateUrl();

                if (urlParts.Any())
                    return urlParts[urlParts.Count - 5];

                return null;
            }
        }


        #region UTILITY METHODS

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

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            var hour = Hour < 10 ? $"0{Hour}" : $"{Hour}";
            var min = Minute < 10 ? $"0{Minute}" : $"{Minute}";

            return $"{hour}:{min}" + (IsVariant ? $" {Variant}" : "");
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public string ToStringNoVariant()
        {
            var hour = Hour < 10 ? $"0{Hour}" : $"{Hour}";
            var min = Minute < 10 ? $"0{Minute}" : $"{Minute}";

            return $"{hour}:{min}";
        }

    }
}
