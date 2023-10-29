using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmArrivalsIMLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmArrivalTripTimeHeader = string.Empty;
        private string _ztmArrivalTripDistanceHeader = string.Empty;


        //  METHODS

        public string ZtmArrivalTripTimeHeader
        {
            get => _ztmArrivalTripTimeHeader;
            set => SetStringProperty(ref _ztmArrivalTripTimeHeader, nameof(ZtmArrivalTripTimeHeader), value);
        }

        public string ZtmArrivalTripDistanceHeader
        {
            get => _ztmArrivalTripDistanceHeader;
            set => SetStringProperty(ref _ztmArrivalTripDistanceHeader, nameof(ZtmArrivalTripDistanceHeader), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmArrivalsIMLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmArrivalsIMLangConfig(
            string? ztmArrivalTripTimeHeader = null,
            string? ztmArrivalTripDistanceHeader = null)
        {
            ZtmArrivalTripTimeHeader = SetLanguageValue(ztmArrivalTripTimeHeader, "Czas podróży");
            ZtmArrivalTripDistanceHeader = SetLanguageValue(ztmArrivalTripDistanceHeader, "Odległość");
        }

        #endregion CLASS METHODS

    }
}
