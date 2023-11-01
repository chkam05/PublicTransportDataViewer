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

        private string _downloadTitle = string.Empty;
        private string _downloadErrorTitle = string.Empty;
        private string _arrivalsDownloadDesc = string.Empty;
        private string _arrivalsDownloadErrorDesc = string.Empty;
        private string _departuresDownloadDesc = string.Empty;
        private string _departuresDownloadErrorDesc = string.Empty;
        private string _lineDetailsDownloadDesc = string.Empty;
        private string _lineDetailsDownloadErrorDesc = string.Empty;
        private string _linesDownloadDesc = string.Empty;
        private string _linesDownloadErrorDesc = string.Empty;
        private string _timeTablesDownloadDesc = string.Empty;
        private string _timeTablesDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

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

        public string ArrivalsDownloadDesc
        {
            get => _arrivalsDownloadDesc;
            set => SetStringProperty(ref _arrivalsDownloadDesc, nameof(ArrivalsDownloadDesc), value);
        }

        public string ArrivalsDownloadErrorDesc
        {
            get => _arrivalsDownloadErrorDesc;
            set => SetStringProperty(ref _arrivalsDownloadErrorDesc, nameof(ArrivalsDownloadErrorDesc), value);
        }

        public string DeparturesDownloadDesc
        {
            get => _departuresDownloadDesc;
            set => SetStringProperty(ref _departuresDownloadDesc, nameof(DeparturesDownloadDesc), value);
        }

        public string DeparturesDownloadErrorDesc
        {
            get => _departuresDownloadErrorDesc;
            set => SetStringProperty(ref _departuresDownloadErrorDesc, nameof(DeparturesDownloadErrorDesc), value);
        }

        public string LineDetailsDownloadDesc
        {
            get => _lineDetailsDownloadDesc;
            set => SetStringProperty(ref _lineDetailsDownloadDesc, nameof(LineDetailsDownloadDesc), value);
        }

        public string LineDetailsDownloadErrorDesc
        {
            get => _lineDetailsDownloadErrorDesc;
            set => SetStringProperty(ref _lineDetailsDownloadErrorDesc, nameof(LineDetailsDownloadErrorDesc), value);
        }

        public string LinesDownloadDesc
        {
            get => _linesDownloadDesc;
            set => SetStringProperty(ref _linesDownloadDesc, nameof(LinesDownloadDesc), value);
        }

        public string LinesDownloadErrorDesc
        {
            get => _linesDownloadErrorDesc;
            set => SetStringProperty(ref _linesDownloadErrorDesc, nameof(LinesDownloadErrorDesc), value);
        }

        public string TimeTablesDownloadDesc
        {
            get => _timeTablesDownloadDesc;
            set => SetStringProperty(ref _timeTablesDownloadDesc, nameof(TimeTablesDownloadDesc), value);
        }

        public string TimeTablesDownloadErrorDesc
        {
            get => _timeTablesDownloadErrorDesc;
            set => SetStringProperty(ref _timeTablesDownloadErrorDesc, nameof(TimeTablesDownloadErrorDesc), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MessagesLangConfig class constructor. </summary>
        [JsonConstructor]
        public MessagesLangConfig(
            string? downloadTitle = null,
            string? downloadErrorTitle = null,
            string? arrivalsDownloadDesc = null,
            string? arrivalsDownloadErrorDesc = null,
            string? departuresDownloadDesc = null,
            string? departuresDownloadErrorDesc = null,
            string? lineDetailsDownloadDesc = null,
            string? lineDetailsDownloadErrorDesc = null,
            string? linesDownloadDesc = null,
            string? linesDownloadErrorDesc = null,
            string? timeTablesDownloadDesc = null,
            string? timeTablesDownloadErrorDesc = null)
        {
            DownloadTitle = SetLanguageValue(downloadTitle, "Pobieranie danych");
            DownloadErrorTitle = SetLanguageValue(downloadErrorTitle, "Błąd pobierania danych");
            ArrivalsDownloadDesc = SetLanguageValue(arrivalsDownloadDesc, "Trwa pobieranie danych godzin przyjazdów. \r\nProszę czekać ...");
            ArrivalsDownloadErrorDesc = SetLanguageValue(arrivalsDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin przyjazdów. \r\nProszę spróbować ponownie za jakiś czas.");
            DeparturesDownloadDesc = SetLanguageValue(departuresDownloadDesc, "Trwa pobieranie danych godzin odjazdów. \r\nProszę czekać ...");
            DeparturesDownloadErrorDesc = SetLanguageValue(departuresDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin odjazdów. \r\nProszę spróbować ponownie za jakiś czas.");
            LineDetailsDownloadDesc = SetLanguageValue(lineDetailsDownloadDesc, "Trwa pobieranie szczegółowych danych linii. \r\nProszę czekać ...");
            LineDetailsDownloadErrorDesc = SetLanguageValue(lineDetailsDownloadErrorDesc, "Wystąpił problem podczas pobierania szczegółowych danych linii. \r\nProszę spróbować ponownie za jakiś czas.");
            LinesDownloadDesc = SetLanguageValue(linesDownloadDesc, "Trwa pobieranie danych linii. \r\nProszę czekać...");
            LinesDownloadErrorDesc = SetLanguageValue(linesDownloadErrorDesc, "Wystąpił problem podczas pobierania danych linii. \r\nProszę spróbować ponownie za jakiś czas.");
            TimeTablesDownloadDesc = SetLanguageValue(timeTablesDownloadDesc, "Trwa pobieranie danych. \r\nProszę czekać...");
            TimeTablesDownloadErrorDesc = SetLanguageValue(timeTablesDownloadErrorDesc, "Wystąpił problem podczas pobierania danych rozkładów jazdy. \r\nProszę spróbować ponownie za jakiś czas.");
        }

        #endregion CLASS METHODS

    }
}
