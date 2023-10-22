using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmTimeTableSelectorIMLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmTimeTableSelectionLoadingTitle = string.Empty;
        private string _ztmTimeTableSelectionLoadingDesc = string.Empty;
        private string _ztmTimeTableSelectionTitle = string.Empty;
        private string _ztmTimeTableSelectionOkButton = string.Empty;
        private string _ztmTimeTableSelectionCancelButton = string.Empty;
        private string _ztmTimeTableSelectionFromToTitle = string.Empty;
        private string _ztmTimeTableSelectionFrom = string.Empty;
        private string _ztmTimeTableSelectionTo = string.Empty;
        private string _ztmTimeTableSelectionDownloadErrorTitle = string.Empty;
        private string _ztmTimeTableSelectionDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmTimeTableSelectionLoadingTitle
        {
            get => _ztmTimeTableSelectionLoadingTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionLoadingTitle, nameof(ZtmTimeTableSelectionLoadingTitle), value);
        }

        public string ZtmTimeTableSelectionLoadingDesc
        {
            get => _ztmTimeTableSelectionLoadingDesc;
            set => SetStringProperty(ref _ztmTimeTableSelectionLoadingDesc, nameof(ZtmTimeTableSelectionLoadingDesc), value);
        }

        public string ZtmTimeTableSelectionTitle
        {
            get => _ztmTimeTableSelectionTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionTitle, nameof(ZtmTimeTableSelectionTitle), value);
        }

        public string ZtmTimeTableSelectionOkButton
        {
            get => _ztmTimeTableSelectionOkButton;
            set => SetStringProperty(ref _ztmTimeTableSelectionOkButton, nameof(ZtmTimeTableSelectionOkButton), value);
        }

        public string ZtmTimeTableSelectionCancelButton
        {
            get => _ztmTimeTableSelectionCancelButton;
            set => SetStringProperty(ref _ztmTimeTableSelectionCancelButton, nameof(ZtmTimeTableSelectionCancelButton), value);
        }

        public string ZtmTimeTableSelectionFromToTitle
        {
            get => _ztmTimeTableSelectionFromToTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionFromToTitle, nameof(ZtmTimeTableSelectionFromToTitle), value);
        }

        public string ZtmTimeTableSelectionFrom
        {
            get => _ztmTimeTableSelectionFrom;
            set => SetStringProperty(ref _ztmTimeTableSelectionFrom, nameof(ZtmTimeTableSelectionFrom), value);
        }

        public string ZtmTimeTableSelectionTo
        {
            get => _ztmTimeTableSelectionTo;
            set => SetStringProperty(ref _ztmTimeTableSelectionTo, nameof(ZtmTimeTableSelectionTo), value);
        }

        public string ZtmTimeTableSelectionDownloadErrorTitle
        {
            get => _ztmTimeTableSelectionDownloadErrorTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionDownloadErrorTitle, nameof(ZtmTimeTableSelectionDownloadErrorTitle), value);
        }

        public string ZtmTimeTableSelectionDownloadErrorDesc
        {
            get => _ztmTimeTableSelectionDownloadErrorDesc;
            set => SetStringProperty(ref _ztmTimeTableSelectionDownloadErrorDesc, nameof(ZtmTimeTableSelectionDownloadErrorDesc), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmTimeTableSelectorIMLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmTimeTableSelectorIMLangConfig(
            string? ztmTimeTableSelectionLoadingTitle = null,
            string? ztmTimeTableSelectionLoadingDesc = null,
            string? ztmTimeTableSelectionTitle = null,
            string? ztmTimeTableSelectionOkButton = null,
            string? ztmTimeTableSelectionCancelButton = null,
            string? ztmTimeTableSelectionFromToTitle = null,
            string? ztmTimeTableSelectionFrom = null,
            string? ztmTimeTableSelectionTo = null,
            string? ztmTimeTableSelectionDownloadErrorTitle = null,
            string? ztmTimeTableSelectionDownloadErrorDesc = null)
        {
            ZtmTimeTableSelectionLoadingTitle = SetLanguageValue(ztmTimeTableSelectionLoadingTitle, "Pobieranie danych");
            ZtmTimeTableSelectionLoadingDesc = SetLanguageValue(ztmTimeTableSelectionLoadingDesc, "Trwa pobieranie danych rozkładów jazdy. \r\nProszę czekać ...");
            ZtmTimeTableSelectionTitle = SetLanguageValue(ztmTimeTableSelectionTitle, "Wybór rozkładu jazdy");
            ZtmTimeTableSelectionOkButton = SetLanguageValue(ztmTimeTableSelectionOkButton, "Wybierz");
            ZtmTimeTableSelectionCancelButton = SetLanguageValue(ztmTimeTableSelectionCancelButton, "Anuluj");
            ZtmTimeTableSelectionFromToTitle = SetLanguageValue(ztmTimeTableSelectionFromToTitle, "Rozkład ważny");
            ZtmTimeTableSelectionFrom = SetLanguageValue(ztmTimeTableSelectionFrom, "od");
            ZtmTimeTableSelectionTo = SetLanguageValue(ztmTimeTableSelectionTo, "do");
            ZtmTimeTableSelectionDownloadErrorTitle = SetLanguageValue(ztmTimeTableSelectionDownloadErrorTitle, "Błąd pobierania danych");
            ZtmTimeTableSelectionDownloadErrorDesc = SetLanguageValue(ztmTimeTableSelectionDownloadErrorDesc, "Wystąpił problem podczas pobierania danych rozkładów jazdy. \r\nProszę spróbować ponownie za jakiś czas.");
        }

        #endregion CLASS METHODS

    }
}
