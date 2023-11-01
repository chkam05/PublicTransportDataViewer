using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class TransportTypesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _bus = string.Empty;
        private string _busAirport = string.Empty;
        private string _busMetropolitan = string.Empty;
        private string _busNight = string.Empty;
        private string _busReplacement = string.Empty;
        private string _tram = string.Empty;
        private string _trolley = string.Empty;


        //  GETTERS & SETTERS

        public string Bus
        {
            get => _bus;
            set => SetStringProperty(ref _bus, nameof(Bus), value);
        }

        public string BusAirport
        {
            get => _busAirport;
            set => SetStringProperty(ref _busAirport, nameof(BusAirport), value);
        }

        public string BusMetropolitan
        {
            get => _busMetropolitan;
            set => SetStringProperty(ref _busMetropolitan, nameof(BusMetropolitan), value);
        }

        public string BusNight
        {
            get => _busNight;
            set => SetStringProperty(ref _busNight, nameof(BusNight), value);
        }

        public string BusReplacement
        {
            get => _busReplacement;
            set => SetStringProperty(ref _busReplacement, nameof(BusReplacement), value);
        }

        public string Tram
        {
            get => _tram;
            set => SetStringProperty(ref _tram, nameof(Tram), value);
        }

        public string Trolley
        {
            get => _trolley;
            set => SetStringProperty(ref _trolley, nameof(Trolley), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TransportTypesLangConfig class constructor. </summary>
        [JsonConstructor]
        public TransportTypesLangConfig(
            string? bus = null,
            string? busAirport = null,
            string? busMetropolitan = null,
            string? busNight = null,
            string? busReplacement = null,
            string? tram = null,
            string? trolley = null)
        {
            Bus = SetLanguageValue(bus, "Autobus");
            BusAirport = SetLanguageValue(busAirport, "Autobus na lotnisko");
            BusMetropolitan = SetLanguageValue(busMetropolitan, "Autobus metropolitalny");
            BusNight = SetLanguageValue(busNight, "Autobus nocny");
            BusReplacement = SetLanguageValue(busReplacement, "Autobus zastępczy");
            Tram = SetLanguageValue(tram, "Tramwaj");
            Trolley = SetLanguageValue(trolley, "Trolejbus");
        }

        #endregion CLASS METHODS

    }
}
