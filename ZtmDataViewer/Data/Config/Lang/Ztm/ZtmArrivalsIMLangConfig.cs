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

        private string _ztmArrivalsTitle = string.Empty;
        private string _ztmArrivalStopHeader = string.Empty;
        private string _ztmArrivalDepartureHeader = string.Empty;
        private string _ztmArrivalTripTimeHeader = string.Empty;
        private string _ztmArrivalTripDistanceHeader = string.Empty;


        //  METHODS

        public string ZtmArrivalsTitle
        {
            get => _ztmArrivalsTitle;
            set => SetStringProperty(ref _ztmArrivalsTitle, nameof(ZtmArrivalsTitle), value);
        }

        public string ZtmArrivalStopHeader
        {
            get => _ztmArrivalStopHeader;
            set => SetStringProperty(ref _ztmArrivalStopHeader, nameof(ZtmArrivalStopHeader), value);
        }

        public string ZtmArrivalDepartureHeader
        {
            get => _ztmArrivalDepartureHeader;
            set => SetStringProperty(ref _ztmArrivalDepartureHeader, nameof(ZtmArrivalDepartureHeader), value);
        }

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
            string? ztmArrivalsTitle = null,
            string? ztmArrivalStopHeader = null,
            string? ztmArrivalDepartureHeader = null,
            string? ztmArrivalTripTimeHeader = null,
            string? ztmArrivalTripDistanceHeader = null)
        {
            ZtmArrivalsTitle = SetLanguageValue(ztmArrivalsTitle, "Godziny przyjazdów");
            ZtmArrivalStopHeader = SetLanguageValue(ztmArrivalStopHeader, "Nazwa przystanku");
            ZtmArrivalDepartureHeader = SetLanguageValue(ztmArrivalDepartureHeader, "Godz.");
            ZtmArrivalTripTimeHeader = SetLanguageValue(ztmArrivalTripTimeHeader, "Czas podróży");
            ZtmArrivalTripDistanceHeader = SetLanguageValue(ztmArrivalTripDistanceHeader, "Odległość");
        }

        #endregion CLASS METHODS

    }
}
