using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmLinesViewLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmLineViewPageTitle = string.Empty;
        private string _ztmLineViewLinesMenuItem = string.Empty;
        private string _ztmLineViewLoadingTitle = string.Empty;
        private string _ztmLineViewLoadingDesc = string.Empty;
        private string _ztmLineViewRefreshButton = string.Empty;
        private string _ztmLineViewDownloadErrorTitle = string.Empty;
        private string _ztmLineViewDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmLineViewPageTitle
        {
            get => _ztmLineViewPageTitle;
            set => SetStringProperty(ref _ztmLineViewPageTitle, nameof(ZtmLineViewPageTitle), value);
        }

        public string ZtmLineViewLinesMenuItem
        {
            get => _ztmLineViewLinesMenuItem;
            set => SetStringProperty(ref _ztmLineViewLinesMenuItem, nameof(ZtmLineViewLinesMenuItem), value);
        }

        public string ZtmLineViewLoadingTitle
        {
            get => _ztmLineViewLoadingTitle;
            set => SetStringProperty(ref _ztmLineViewLoadingTitle, nameof(ZtmLineViewLoadingTitle), value);
        }

        public string ZtmLineViewLoadingDesc
        {
            get => _ztmLineViewLoadingDesc;
            set => SetStringProperty(ref _ztmLineViewLoadingDesc, nameof(ZtmLineViewLoadingDesc), value);
        }

        public string ZtmLineViewRefreshButton
        {
            get => _ztmLineViewRefreshButton;
            set => SetStringProperty(ref _ztmLineViewRefreshButton, nameof(ZtmLineViewRefreshButton), value);
        }

        public string ZtmLineViewDownloadErrorTitle
        {
            get => _ztmLineViewDownloadErrorTitle;
            set => SetStringProperty(ref _ztmLineViewDownloadErrorTitle, nameof(ZtmLineViewDownloadErrorTitle), value);
        }

        public string ZtmLineViewDownloadErrorDesc
        {
            get => _ztmLineViewDownloadErrorDesc;
            set => SetStringProperty(ref _ztmLineViewDownloadErrorDesc, nameof(ZtmLineViewDownloadErrorDesc), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmLinesViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmLinesViewLangConfig(
            string? ztmLineViewPageTitle = null,
            string? ztmLineViewLinesMenuItem = null,
            string? ztmLineViewLoadingTitle = null,
            string? ztmLineViewLoadingDesc = null,
            string? ztmLineViewRefreshButton = null,
            string? ztmLineViewDownloadErrorTitle = null,
            string? ztmLineViewDownloadErrorDesc = null)
        {
            ZtmLineViewPageTitle = SetLanguageValue(ztmLineViewPageTitle, "Wybór linii");
            ZtmLineViewLinesMenuItem = SetLanguageValue(ztmLineViewLinesMenuItem, "Wybór linii");
            ZtmLineViewLoadingTitle = SetLanguageValue(ztmLineViewLoadingTitle, "Pobieranie danych");
            ZtmLineViewLoadingDesc = SetLanguageValue(ztmLineViewLoadingDesc, "Trwa pobieranie danych linii. \r\nProszę czekać...");
            ZtmLineViewRefreshButton = SetLanguageValue(ztmLineViewRefreshButton, "Odśwież");
            ZtmLineViewDownloadErrorTitle = SetLanguageValue(ztmLineViewDownloadErrorTitle, "Błąd pobierania danych");
            ZtmLineViewDownloadErrorDesc = SetLanguageValue(ztmLineViewDownloadErrorDesc, "Wystąpił problem podczas pobierania danych linii. \r\nProszę spróbować ponownie za jakiś czas.");
        }

        #endregion CLASS METHODS

    }
}
