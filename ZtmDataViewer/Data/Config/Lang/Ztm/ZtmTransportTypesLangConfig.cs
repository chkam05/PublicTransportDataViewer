using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmTransportTypesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmTransportTypeBus = string.Empty;
        private string _ztmTransportTypeBusAirport = string.Empty;
        private string _ztmTransportTypeBusMetropolitan = string.Empty;
        private string _ztmTransportTypeBusNight = string.Empty;
        private string _ztmTransportTypeBusReplacement = string.Empty;
        private string _ztmTransportTypeTram = string.Empty;
        private string _ztmTransportTypeTrolley = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmTransportTypeBus
        {
            get => _ztmTransportTypeBus;
            set => SetStringProperty(ref _ztmTransportTypeBus, nameof(ZtmTransportTypeBus), value);
        }

        public string ZtmTransportTypeBusAirport
        {
            get => _ztmTransportTypeBusAirport;
            set => SetStringProperty(ref _ztmTransportTypeBusAirport, nameof(ZtmTransportTypeBusAirport), value);
        }

        public string ZtmTransportTypeBusMetropolitan
        {
            get => _ztmTransportTypeBusMetropolitan;
            set => SetStringProperty(ref _ztmTransportTypeBusMetropolitan, nameof(ZtmTransportTypeBusMetropolitan), value);
        }

        public string ZtmTransportTypeBusNight
        {
            get => _ztmTransportTypeBusNight;
            set => SetStringProperty(ref _ztmTransportTypeBusNight, nameof(ZtmTransportTypeBusNight), value);
        }

        public string ZtmTransportTypeBusReplacement
        {
            get => _ztmTransportTypeBusReplacement;
            set => SetStringProperty(ref _ztmTransportTypeBusReplacement, nameof(ZtmTransportTypeBusReplacement), value);
        }

        public string ZtmTransportTypeTram
        {
            get => _ztmTransportTypeTram;
            set => SetStringProperty(ref _ztmTransportTypeTram, nameof(ZtmTransportTypeTram), value);
        }

        public string ZtmTransportTypeTrolley
        {
            get => _ztmTransportTypeTrolley;
            set => SetStringProperty(ref _ztmTransportTypeTrolley, nameof(ZtmTransportTypeTrolley), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmTransportTypesLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmTransportTypesLangConfig(
            string? ztmTransportTypeBus = null,
            string? ztmTransportTypeBusAirport = null,
            string? ztmTransportTypeBusMetropolitan = null,
            string? ztmTransportTypeBusNight = null,
            string? ztmTransportTypeBusReplacement = null,
            string? ztmTransportTypeTram = null,
            string? ztmTransportTypeTrolley = null)
        {
            ZtmTransportTypeBus = SetLanguageValue(ztmTransportTypeBus, "Autobus");
            ZtmTransportTypeBusAirport = SetLanguageValue(ztmTransportTypeBusAirport, "Autobus na lotnisko");
            ZtmTransportTypeBusMetropolitan = SetLanguageValue(ztmTransportTypeBusMetropolitan, "Autobus metropolitalny");
            ZtmTransportTypeBusNight = SetLanguageValue(ztmTransportTypeBusNight, "Autobus nocny");
            ZtmTransportTypeBusReplacement = SetLanguageValue(ztmTransportTypeBusReplacement, "Autobus zastępczy");
            ZtmTransportTypeTram = SetLanguageValue(ztmTransportTypeTram, "Tramwaj");
            ZtmTransportTypeTrolley = SetLanguageValue(ztmTransportTypeTrolley, "Trolejbus");
        }

        #endregion CLASS METHODS

    }
}
