using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmLineDetailsViewLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmLineDetailsViewLoadingTitle = string.Empty;
        private string _ztmLineDetailsViewLoadingDesc = string.Empty;
        private string _ztmLineDetailsViewPageDirection = string.Empty;
        private string _ztmLineDetailsViewPageMessages = string.Empty;
        private string _ztmLineDetailsViewPageDepartures = string.Empty;
        private string _ztmLineDetailsViewDownloadErrorTitle = string.Empty;
        private string _ztmLineDetailsViewDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmLineDetailsViewLoadingTitle
        {
            get => _ztmLineDetailsViewLoadingTitle;
            set => SetStringProperty(ref _ztmLineDetailsViewLoadingTitle, nameof(ZtmLineDetailsViewLoadingTitle), value);
        }

        public string ZtmLineDetailsViewLoadingDesc
        {
            get => _ztmLineDetailsViewLoadingDesc;
            set => SetStringProperty(ref _ztmLineDetailsViewLoadingDesc, nameof(ZtmLineDetailsViewLoadingDesc), value);
        }

        public string ZtmLineDetailsViewPageDirection
        {
            get => _ztmLineDetailsViewPageDirection;
            set => SetStringProperty(ref _ztmLineDetailsViewPageDirection, nameof(ZtmLineDetailsViewPageDirection), value);
        }

        public string ZtmLineDetailsViewPageMessages
        {
            get => _ztmLineDetailsViewPageMessages;
            set => SetStringProperty(ref _ztmLineDetailsViewPageMessages, nameof(ZtmLineDetailsViewPageMessages), value);
        }

        public string ZtmLineDetailsViewPageDepartures
        {
            get => _ztmLineDetailsViewPageDepartures;
            set => SetStringProperty(ref _ztmLineDetailsViewPageDepartures, nameof(ZtmLineDetailsViewPageDepartures), value);
        }

        public string ZtmLineDetailsViewDownloadErrorTitle
        {
            get => _ztmLineDetailsViewDownloadErrorTitle;
            set => SetStringProperty(ref _ztmLineDetailsViewDownloadErrorTitle, nameof(ZtmLineDetailsViewDownloadErrorTitle), value);
        }

        public string ZtmLineDetailsViewDownloadErrorDesc
        {
            get => _ztmLineDetailsViewDownloadErrorDesc;
            set => SetStringProperty(ref _ztmLineDetailsViewDownloadErrorDesc, nameof(ZtmLineDetailsViewDownloadErrorDesc), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmLineDetailsViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmLineDetailsViewLangConfig(
            string? ztmLineDetailsViewLoadingTitle = null,
            string? ztmLineDetailsViewLoadingDesc = null,
            string? ztmLineDetailsViewPageDirection = null,
            string? ztmLineDetailsViewPageMessages = null,
            string? ztmLineDetailsViewPageDepartures = null,
            string? ztmLineDetailsViewDownloadErrorTitle = null,
            string? ztmLineDetailsViewDownloadErrorDesc = null)
        {
            ZtmLineDetailsViewLoadingTitle = SetLanguageValue(ztmLineDetailsViewLoadingTitle, "Pobieranie danych");
            ZtmLineDetailsViewLoadingDesc = SetLanguageValue(ztmLineDetailsViewLoadingDesc, "Trwa pobieranie szczegółowych danych linii. \r\nProszę czekać ...");
            ZtmLineDetailsViewPageDirection = SetLanguageValue(ztmLineDetailsViewPageDirection, "Wybór kierunku:");
            ZtmLineDetailsViewPageMessages = SetLanguageValue(ztmLineDetailsViewPageMessages, "Komunikaty:");
            ZtmLineDetailsViewPageDepartures = SetLanguageValue(ztmLineDetailsViewPageDepartures, "Godziny odjazdów:");
            ZtmLineDetailsViewDownloadErrorTitle = SetLanguageValue(ztmLineDetailsViewDownloadErrorTitle, "Błąd pobierania danych");
            ZtmLineDetailsViewDownloadErrorDesc = SetLanguageValue(ztmLineDetailsViewDownloadErrorDesc, "Wystąpił problem podczas pobierania szczegółowych danych linii. \r\nProszę spróbować ponownie za jakiś czas.");
        }

        #endregion CLASS METHODS

    }
}
