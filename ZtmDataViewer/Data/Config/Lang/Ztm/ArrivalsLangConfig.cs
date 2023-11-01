using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ArrivalsLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _tripDistance = string.Empty;
        private string _tripTime = string.Empty;


        //  METHODS

        public string TripDistance
        {
            get => _tripDistance;
            set => SetStringProperty(ref _tripDistance, nameof(TripDistance), value);
        }

        public string TripTime
        {
            get => _tripTime;
            set => SetStringProperty(ref _tripTime, nameof(TripTime), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalsLangConfig class constructor. </summary>
        [JsonConstructor]
        public ArrivalsLangConfig(
            string? tripDistance = null,
            string? tripTime = null)
        {
            TripDistance = SetLanguageValue(tripDistance, "Odległość");
            TripTime = SetLanguageValue(tripTime, "Czas podróży");
        }

        #endregion CLASS METHODS

    }
}
