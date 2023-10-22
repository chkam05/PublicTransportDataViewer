using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmDeparturesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmDeparturesViewLoadingTitle = string.Empty;
        private string _ztmDeparturesViewLoadingDesc = string.Empty;
        private string _ztmDeparturesViewDownloadErrorTitle = string.Empty;
        private string _ztmDeparturesViewDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmDeparturesViewLoadingTitle
        {
            get => _ztmDeparturesViewLoadingTitle;
            set => SetStringProperty(ref _ztmDeparturesViewLoadingTitle, nameof(ZtmDeparturesViewLoadingTitle), value);
        }

        public string ZtmDeparturesViewLoadingDesc
        {
            get => _ztmDeparturesViewLoadingDesc;
            set => SetStringProperty(ref _ztmDeparturesViewLoadingDesc, nameof(_ztmDeparturesViewLoadingDesc), value);
        }

        public string ZtmDeparturesViewDownloadErrorTitle
        {
            get => _ztmDeparturesViewDownloadErrorTitle;
            set => SetStringProperty(ref _ztmDeparturesViewDownloadErrorTitle, nameof(ZtmDeparturesViewDownloadErrorTitle), value);
        }

        public string ZtmDeparturesViewDownloadErrorDesc
        {
            get => _ztmDeparturesViewDownloadErrorDesc;
            set => SetStringProperty(ref _ztmDeparturesViewDownloadErrorDesc, nameof(ZtmDeparturesViewDownloadErrorDesc), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmDeparturesLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmDeparturesLangConfig(
            string? ztmDeparturesViewLoadingTitle = null,
            string? ztmDeparturesViewLoadingDesc = null,
            string? ztmDeparturesViewDownloadErrorTitle = null,
            string? ztmDeparturesViewDownloadErrorDesc = null)
        {
            ZtmDeparturesViewLoadingTitle = SetLanguageValue(ztmDeparturesViewLoadingTitle, "Pobieranie danych");
            ZtmDeparturesViewLoadingDesc = SetLanguageValue(ztmDeparturesViewLoadingDesc, "Trwa pobieranie danych godzin odjazdów. \r\nProszę czekać ...");
            ZtmDeparturesViewDownloadErrorTitle = SetLanguageValue(ztmDeparturesViewDownloadErrorTitle, "Błąd pobierania danych");
            ZtmDeparturesViewDownloadErrorDesc = SetLanguageValue(ztmDeparturesViewDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin odjazdów. \r\nProszę spróbować ponownie za jakiś czas.");
        }

        #endregion CLASS METHODS

    }
}
