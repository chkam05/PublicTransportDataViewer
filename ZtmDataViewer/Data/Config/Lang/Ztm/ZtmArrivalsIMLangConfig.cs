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

        private string _ztmArrivalsLoadingTitle = string.Empty;
        private string _ztmArrivalsLoadingDesc = string.Empty;
        private string _ztmArrivalsDownloadErrorTitle = string.Empty;
        private string _ztmArrivalsDownloadErrorDesc = string.Empty;
        private string _ztmArrivalsTitle = string.Empty;
        private string _ztmArrivalsCancelButton = string.Empty;
        private string _ztmArrivalStopHeader = string.Empty;
        private string _ztmArrivalDepartureHeader = string.Empty;
        private string _ztmArrivalTripTimeHeader = string.Empty;
        private string _ztmArrivalTripDistanceHeader = string.Empty;


        //  METHODS

        public string ZtmArrivalsLoadingTitle
        {
            get => _ztmArrivalsLoadingTitle;
            set => SetStringProperty(ref _ztmArrivalsLoadingTitle, nameof(ZtmArrivalsLoadingTitle), value);
        }

        public string ZtmArrivalsLoadingDesc
        {
            get => _ztmArrivalsLoadingDesc;
            set => SetStringProperty(ref _ztmArrivalsLoadingDesc, nameof(ZtmArrivalsLoadingDesc), value);
        }

        public string ZtmArrivalsDownloadErrorTitle
        {
            get => _ztmArrivalsDownloadErrorTitle;
            set => SetStringProperty(ref _ztmArrivalsDownloadErrorTitle, nameof(ZtmArrivalsDownloadErrorTitle), value);
        }

        public string ZtmArrivalsDownloadErrorDesc
        {
            get => _ztmArrivalsDownloadErrorDesc;
            set => SetStringProperty(ref _ztmArrivalsDownloadErrorDesc, nameof(ZtmArrivalsDownloadErrorDesc), value);
        }

        public string ZtmArrivalsTitle
        {
            get => _ztmArrivalsTitle;
            set => SetStringProperty(ref _ztmArrivalsTitle, nameof(ZtmArrivalsTitle), value);
        }

        public string ZtmArrivalsCancelButton
        {
            get => _ztmArrivalsCancelButton;
            set => SetStringProperty(ref _ztmArrivalsCancelButton, nameof(ZtmArrivalsCancelButton), value);
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
            string? ztmArrivalsLoadingTitle = null,
            string? ztmArrivalsLoadingDesc = null,
            string? ztmArrivalsDownloadErrorTitle = null,
            string? ztmArrivalsDownloadErrorDesc = null,
            string? ztmArrivalsTitle = null,
            string? ztmArrivalsCancelButton = null,
            string? ztmArrivalStopHeader = null,
            string? ztmArrivalDepartureHeader = null,
            string? ztmArrivalTripTimeHeader = null,
            string? ztmArrivalTripDistanceHeader = null)
        {
            ZtmArrivalsLoadingTitle = SetLanguageValue(ztmArrivalsLoadingTitle, "Pobieranie danych");
            ZtmArrivalsLoadingDesc = SetLanguageValue(ztmArrivalsLoadingDesc, "Trwa pobieranie danych godzin przyjazdów. \r\nProszę czekać ...");
            ZtmArrivalsDownloadErrorTitle = SetLanguageValue(ztmArrivalsDownloadErrorTitle, "Błąd pobierania danych");
            ZtmArrivalsDownloadErrorDesc = SetLanguageValue(ztmArrivalsDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin przyjazdów. \r\nProszę spróbować ponownie za jakiś czas.");
            ZtmArrivalsTitle = SetLanguageValue(ztmArrivalsTitle, "Godziny przyjazdów");
            ZtmArrivalsCancelButton = SetLanguageValue(ztmArrivalsCancelButton, "Zamknij");
            ZtmArrivalStopHeader = SetLanguageValue(ztmArrivalStopHeader, "Nazwa przystanku");
            ZtmArrivalDepartureHeader = SetLanguageValue(ztmArrivalDepartureHeader, "Godz.");
            ZtmArrivalTripTimeHeader = SetLanguageValue(ztmArrivalTripTimeHeader, "Czas podróży");
            ZtmArrivalTripDistanceHeader = SetLanguageValue(ztmArrivalTripDistanceHeader, "Odległość");
        }

        #endregion CLASS METHODS

    }
}
