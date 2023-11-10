using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class TransportTypesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _bus = string.Empty;
        private string _busNight = string.Empty;
        private string _busSuburban = string.Empty;
        private string _tram = string.Empty;


        //  GETTERS & SETTERS

        public string Bus
        {
            get => _bus;
            set => SetStringProperty(ref _bus, nameof(Bus), value);
        }

        public string BusNight
        {
            get => _busNight;
            set => SetStringProperty(ref _busNight, nameof(BusNight), value);
        }

        public string BusSuburban
        {
            get => _busSuburban;
            set => SetStringProperty(ref _busSuburban, nameof(BusSuburban), value);
        }

        public string Tram
        {
            get => _tram;
            set => SetStringProperty(ref _tram, nameof(Tram), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TransportTypesLangConfig class constructor. </summary>
        [JsonConstructor]
        public TransportTypesLangConfig(
            string? bus = null,
            string? busNight = null,
            string? busSuburban = null,
            string? tram = null)
        {
            Bus = SetLanguageValue(bus, "Autobus");
            BusNight = SetLanguageValue(busNight, "Autobus nocny");
            BusSuburban = SetLanguageValue(busSuburban, "Autobus podmiejski");
            Tram = SetLanguageValue(tram, "Tramwaj");
        }

        #endregion CLASS METHODS

    }
}
