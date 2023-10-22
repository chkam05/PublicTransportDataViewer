using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class MessagesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        //  Global
        private string _downloadTitle = string.Empty;
        private string _downloadErrorTitle = string.Empty;

        //  Arrivals View Page
        private string _arrivalsViewPageDownloadDesc = string.Empty;
        private string _arrivalsViewPageDownloadErrorDesc = string.Empty;

        //  Departures View Page
        private string _departuresViewPageDownloadDesc = string.Empty;
        private string _departuresViewPageDownloadErrorDesc = string.Empty;

        //  Lines View Page
        private string _linesViewPageDownloadDesc = string.Empty;
        private string _linesViewPageDownloadErrorDesc = string.Empty;

        //  Line Details View Page
        private string _lineDetailsViewPageDownloadDesc = string.Empty;
        private string _lineDetailsViewPageDownloadErrorDesc = string.Empty;

        //  Time Tables View Page
        private string _timeTablesViewPageDownloadDesc = string.Empty;
        private string _timeTablesViewPageDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

        #region Global

        public string DownloadTitle
        {
            get => _downloadTitle;
            set => SetStringProperty(ref _downloadTitle, nameof(DownloadTitle), value);
        }

        public string DownloadErrorTitle
        {
            get => _downloadErrorTitle;
            set => SetStringProperty(ref _downloadErrorTitle, nameof(DownloadErrorTitle), value);
        }

        #endregion Global

        #region Arrivals View Page

        public string ArrivalsViewPageDownloadDesc
        {
            get => _arrivalsViewPageDownloadDesc;
            set => SetStringProperty(ref _arrivalsViewPageDownloadDesc, nameof(ArrivalsViewPageDownloadDesc), value);
        }

        public string ArrivalsViewPageDownloadErrorDesc
        {
            get => _arrivalsViewPageDownloadErrorDesc;
            set => SetStringProperty(ref _arrivalsViewPageDownloadErrorDesc, nameof(ArrivalsViewPageDownloadErrorDesc), value);
        }

        #endregion Arrivals View Page

        #region Departures View Page

        public string DeparturesViewPageDownloadDesc
        {
            get => _departuresViewPageDownloadDesc;
            set => SetStringProperty(ref _departuresViewPageDownloadDesc, nameof(DeparturesViewPageDownloadDesc), value);
        }

        public string DeparturesViewPageDownloadErrorDesc
        {
            get => _departuresViewPageDownloadErrorDesc;
            set => SetStringProperty(ref _departuresViewPageDownloadErrorDesc, nameof(DeparturesViewPageDownloadErrorDesc), value);
        }

        #endregion Departures View Page

        #region Lines View Page

        public string LinesViewPageDownloadDesc
        {
            get => _linesViewPageDownloadDesc;
            set => SetStringProperty(ref _linesViewPageDownloadDesc, nameof(LinesViewPageDownloadDesc), value);
        }

        public string LinesViewPageDownloadErrorDesc
        {
            get => _linesViewPageDownloadErrorDesc;
            set => SetStringProperty(ref _linesViewPageDownloadErrorDesc, nameof(LinesViewPageDownloadErrorDesc), value);
        }

        #endregion Lines View Page

        #region Line Details View Page

        public string LineDetailsViewPageDownloadDesc
        {
            get => _lineDetailsViewPageDownloadDesc;
            set => SetStringProperty(ref _lineDetailsViewPageDownloadDesc, nameof(LineDetailsViewPageDownloadDesc), value);
        }

        public string LineDetailsViewPageDownloadErrorDesc
        {
            get => _lineDetailsViewPageDownloadErrorDesc;
            set => SetStringProperty(ref _lineDetailsViewPageDownloadErrorDesc, nameof(LineDetailsViewPageDownloadErrorDesc), value);
        }

        #endregion Line Details View Page

        #region Time Tables View Page

        public string TimeTablesViewPageDownloadDesc
        {
            get => _timeTablesViewPageDownloadDesc;
            set => SetStringProperty(ref _timeTablesViewPageDownloadDesc, nameof(TimeTablesViewPageDownloadDesc), value);
        }

        public string TimeTablesViewPageDownloadErrorDesc
        {
            get => _timeTablesViewPageDownloadErrorDesc;
            set => SetStringProperty(ref _timeTablesViewPageDownloadErrorDesc, nameof(TimeTablesViewPageDownloadErrorDesc), value);
        }

        #endregion Time Tables View Page


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MessagesLangConfig class constructor. </summary>
        [JsonConstructor]
        public MessagesLangConfig(
            //  Global
            string? downloadTitle = null,
            string? downloadErrorTitle = null,

            //  Arrivals View Page
            string? arrivalsViewPageDownloadDesc = null,
            string? arrivalsViewPageDownloadErrorDesc = null,

            //  Departures View Page
            string? departuresViewPageDownloadDesc = null,
            string? departuresViewPageDownloadErrorDesc = null,

            //  Lines View Page
            string? linesViewPageDownloadDesc = null,
            string? linesViewPageDownloadErrorDesc = null,

            //  Line Details View Page
            string? lineDetailsViewPageDownloadDesc = null,
            string? lineDetailsViewPageDownloadErrorDesc = null,

            //  Time Tables View Page
            string? timeTablesViewPageDownloadDesc = null,
            string? timeTablesViewPageDownloadErrorDesc = null)
        {
            //  Global
            DownloadTitle = SetLanguageValue(downloadTitle, "Pobieranie danych");
            DownloadErrorTitle = SetLanguageValue(downloadErrorTitle, "Błąd pobierania danych");

            //  Arrivals View Page
            ArrivalsViewPageDownloadDesc = SetLanguageValue(arrivalsViewPageDownloadDesc, "Trwa pobieranie danych godzin przyjazdów. \r\nProszę czekać ...");
            ArrivalsViewPageDownloadErrorDesc = SetLanguageValue(arrivalsViewPageDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin przyjazdów. \r\nProszę spróbować ponownie za jakiś czas.");

            //  Departures View Page
            DeparturesViewPageDownloadDesc = SetLanguageValue(departuresViewPageDownloadDesc, "Trwa pobieranie danych godzin odjazdów. \r\nProszę czekać ...");
            DeparturesViewPageDownloadErrorDesc = SetLanguageValue(departuresViewPageDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin odjazdów. \r\nProszę spróbować ponownie za jakiś czas.");

            //  Lines View Page
            LinesViewPageDownloadDesc = SetLanguageValue(linesViewPageDownloadDesc, "Trwa pobieranie danych linii. \r\nProszę czekać...");
            LinesViewPageDownloadErrorDesc = SetLanguageValue(linesViewPageDownloadErrorDesc, "Wystąpił problem podczas pobierania danych linii. \r\nProszę spróbować ponownie za jakiś czas.");

            //  Line Details View Page
            LineDetailsViewPageDownloadDesc = SetLanguageValue(lineDetailsViewPageDownloadDesc, "Trwa pobieranie szczegółowych danych linii. \r\nProszę czekać ...");
            LineDetailsViewPageDownloadErrorDesc = SetLanguageValue(lineDetailsViewPageDownloadErrorDesc, "Wystąpił problem podczas pobierania szczegółowych danych linii. \r\nProszę spróbować ponownie za jakiś czas.");

            //  Time Tables View Page
            TimeTablesViewPageDownloadDesc = SetLanguageValue(timeTablesViewPageDownloadDesc, "Błąd pobierania danych");
            TimeTablesViewPageDownloadErrorDesc = SetLanguageValue(timeTablesViewPageDownloadErrorDesc, "Wystąpił problem podczas pobierania danych rozkładów jazdy. \r\nProszę spróbować ponownie za jakiś czas.");
        }

        #endregion CLASS METHODS

    }
}
