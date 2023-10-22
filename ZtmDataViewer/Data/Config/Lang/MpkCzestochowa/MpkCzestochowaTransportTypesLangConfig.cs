using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class MpkCzestochowaTransportTypesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _mpkCzestochowaTransportTypeBus = string.Empty;
        private string _mpkCzestochowaTransportTypeBusNight = string.Empty;
        private string _mpkCzestochowaTransportTypeBusSuburban = string.Empty;
        private string _mpkCzestochowaTransportTypeTram = string.Empty;


        //  GETTERS & SETTERS

        public string MpkCzestochowaTransportTypeBus
        {
            get => _mpkCzestochowaTransportTypeBus;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeBus, nameof(MpkCzestochowaTransportTypeBus), value);
        }

        public string MpkCzestochowaTransportTypeBusNight
        {
            get => _mpkCzestochowaTransportTypeBusNight;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeBusNight, nameof(MpkCzestochowaTransportTypeBusNight), value);
        }

        public string MpkCzestochowaTransportTypeBusSuburban
        {
            get => _mpkCzestochowaTransportTypeBusSuburban;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeBusSuburban, nameof(MpkCzestochowaTransportTypeBusSuburban), value);
        }

        public string MpkCzestochowaTransportTypeTram
        {
            get => _mpkCzestochowaTransportTypeTram;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeTram, nameof(MpkCzestochowaTransportTypeTram), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MpkCzestochowaTransportTypesLangConfig class constructor. </summary>
        [JsonConstructor]
        public MpkCzestochowaTransportTypesLangConfig(
            string? mpkCzestochowaTransportTypeBus = null,
            string? mpkCzestochowaTransportTypeBusNight = null,
            string? mpkCzestochowaTransportTypeBusSuburban = null,
            string? mpkCzestochowaTransportTypeTram = null)
        {
            MpkCzestochowaTransportTypeBus = SetLanguageValue(mpkCzestochowaTransportTypeBus, "Autobus");
            MpkCzestochowaTransportTypeBusNight = SetLanguageValue(mpkCzestochowaTransportTypeBusNight, "Autobus nocny");
            MpkCzestochowaTransportTypeBusSuburban = SetLanguageValue(mpkCzestochowaTransportTypeBusSuburban, "Autobus podmiejski");
            MpkCzestochowaTransportTypeTram = SetLanguageValue(mpkCzestochowaTransportTypeTram, "Tramwaj");
        }

        #endregion CLASS METHODS

    }
}
