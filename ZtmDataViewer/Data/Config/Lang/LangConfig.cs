using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataViewer.Data.Config.Lang.MpkCzestochowa;
using ZtmDataViewer.Data.Config.Lang.Ztm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class LangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _name = string.Empty;

        //  Components
        private MessagesLangConfig? _messagesLangConfig = null;
        private MpkCzestochowaLangConfig? _mpkCzestochowaLangConfig = null;
        private SettingsLangConfig? _settingsLangConfig = null;
        private ZtmLangConfig? _ztmLangConfig = null;

        //  Main Menu
        private string _mainMenuItem = string.Empty;
        private string _startPageMpkCzestochowaItem = string.Empty;
        private string _startPageZtmMenuItem = string.Empty;
        private string _startPageSettingsMenuItem = string.Empty;

        //  Buttons
        private string _cancelButton = string.Empty;
        private string _closeButton = string.Empty;
        private string _selectButton = string.Empty;

        //  Start Page
        private string _startPageTitle = string.Empty;

        //  Line Arrivals IM
        private string _lineArrivalsTitle = string.Empty;
        private string _lineArrivalStopHeader = string.Empty;
        private string _lineArrivalDepartureHeader = string.Empty;

        //  Lines View Page
        private string _linesViewPageTitle = string.Empty;
        private string _linesViewPageMenuItem = string.Empty;
        private string _linesViewPageRefreshButton = string.Empty;

        //  Line Details View Page
        private string _lineDetailsViewPageDepartures = string.Empty;
        private string _lineDetailsViewPageDirectionSelection = string.Empty;

        //  Time Tables View Page
        private string _timeTablesViewPageTitle = string.Empty;


        //  GETTERS & SETTERS

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        #region Components

        public MessagesLangConfig Messages
        {
            get
            {
                if (_messagesLangConfig == null)
                {
                    _messagesLangConfig = new MessagesLangConfig();
                    OnPropertyChanged(nameof(Messages));
                }
                return _messagesLangConfig;
            }
            set
            {
                _messagesLangConfig = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public MpkCzestochowaLangConfig MpkCzestochowa
        {
            get
            {
                if (_mpkCzestochowaLangConfig == null)
                {
                    _mpkCzestochowaLangConfig = new MpkCzestochowaLangConfig();
                    OnPropertyChanged(nameof(MpkCzestochowa));
                }
                return _mpkCzestochowaLangConfig;
            }
            set
            {
                _mpkCzestochowaLangConfig = value;
                OnPropertyChanged(nameof(MpkCzestochowa));
            }
        }

        public SettingsLangConfig Settings
        {
            get
            {
                if (_settingsLangConfig == null)
                {
                    _settingsLangConfig = new SettingsLangConfig();
                    OnPropertyChanged(nameof(Settings));
                }
                return _settingsLangConfig;
            }
            set
            {
                _settingsLangConfig = value;
                OnPropertyChanged(nameof(Settings));
            }
        }

        public ZtmLangConfig Ztm
        {
            get
            {
                if (_ztmLangConfig == null)
                {
                    _ztmLangConfig = new ZtmLangConfig();
                    OnPropertyChanged(nameof(Ztm));
                }
                return _ztmLangConfig;
            }
            set
            {
                _ztmLangConfig = value;
                OnPropertyChanged(nameof(Ztm));
            }
        }

        #endregion Components

        #region Main Menu

        public string MainMenuItem
        {
            get => _mainMenuItem;
            set => SetStringProperty(ref _mainMenuItem, nameof(MainMenuItem), value);
        }

        public string StartPageMpkCzestochowaItem
        {
            get => _startPageMpkCzestochowaItem;
            set => SetStringProperty(ref _startPageMpkCzestochowaItem, nameof(StartPageMpkCzestochowaItem), value);
        }

        public string StartPageZtmMenuItem
        {
            get => _startPageZtmMenuItem;
            set => SetStringProperty(ref _startPageZtmMenuItem, nameof(StartPageZtmMenuItem), value);
        }

        public string StartPageSettingsMenuItem
        {
            get => _startPageSettingsMenuItem;
            set => SetStringProperty(ref _startPageSettingsMenuItem, nameof(StartPageSettingsMenuItem), value);
        }

        #endregion MainMenu

        #region Buttons

        public string CancelButton
        {
            get => _cancelButton;
            set => SetStringProperty(ref _cancelButton, nameof(CancelButton), value);
        }

        public string CloseButton
        {
            get => _closeButton;
            set => SetStringProperty(ref _closeButton, nameof(CloseButton), value);
        }

        public string SelectButton
        {
            get => _selectButton;
            set => SetStringProperty(ref _selectButton, nameof(SelectButton), value);
        }

        #endregion Buttons

        #region Start Page

        public string StartPageTitle
        {
            get => _startPageTitle;
            set => SetStringProperty(ref _startPageTitle, nameof(StartPageTitle), value);
        }

        #endregion Start Page

        #region  Line Arrivals IM

        public string LineArrivalsTitle
        {
            get => _lineArrivalsTitle;
            set => SetStringProperty(ref _lineArrivalsTitle, nameof(LineArrivalsTitle), value);
        }

        public string LineArrivalStopHeader
        {
            get => _lineArrivalStopHeader;
            set => SetStringProperty(ref _lineArrivalStopHeader, nameof(LineArrivalStopHeader), value);
        }

        public string LineArrivalDepartureHeader
        {
            get => _lineArrivalDepartureHeader;
            set => SetStringProperty(ref _lineArrivalDepartureHeader, nameof(LineArrivalDepartureHeader), value);
        }

        #endregion  Line Arrivals IM

        #region Lines View Page

        public string LinesViewPageTitle
        {
            get => _linesViewPageTitle;
            set => SetStringProperty(ref _linesViewPageTitle, nameof(LinesViewPageTitle), value);
        }

        public string LinesViewPageMenuItem
        {
            get => _linesViewPageMenuItem;
            set => SetStringProperty(ref _linesViewPageMenuItem, nameof(LinesViewPageMenuItem), value);
        }

        public string LinesViewPageRefreshButton
        {
            get => _linesViewPageRefreshButton;
            set => SetStringProperty(ref _linesViewPageRefreshButton, nameof(LinesViewPageRefreshButton), value);
        }

        #endregion Lines View Page

        #region Line Details View Page

        public string LineDetailsViewPageDepartures
        {
            get => _lineDetailsViewPageDepartures;
            set => SetStringProperty(ref _lineDetailsViewPageDepartures, nameof(LineDetailsViewPageDepartures), value);
        }

        public string LineDetailsViewPageDirectionSelection
        {
            get => _lineDetailsViewPageDirectionSelection;
            set => SetStringProperty(ref _lineDetailsViewPageDirectionSelection, nameof(LineDetailsViewPageDirectionSelection), value);
        }

        #endregion Line Details View Page

        #region Time Tables View Page

        public string TimeTablesViewPageTitle
        {
            get => _timeTablesViewPageTitle;
            set => SetStringProperty(ref _timeTablesViewPageTitle, nameof(TimeTablesViewPageTitle), value);
        }

        #endregion Time Tables View Page


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LangConfig class constructor. </summary>
        [JsonConstructor]
        public LangConfig(
            string? name = null,

            //  Components
            MessagesLangConfig? messages = null,
            MpkCzestochowaLangConfig? mpkCzestochowa = null,
            SettingsLangConfig? settings = null,
            ZtmLangConfig? ztm = null,

            //  Main Menu
            string? mainMenuItem = null,
            string? startPageMpkCzestochowaItem = null,
            string? startPageZtmMenuItem = null,
            string? startPageSettingsMenuItem = null,

            //  Buttons
            string? cancelButton = null,
            string? closeButton = null,
            string? selectButton = null,

            //  Start Page
            string? startPageTitle = null,

            //  Line Arrivals IM
            string ? lineArrivalsTitle = null,
            string ? lineArrivalStopHeader = null,
            string ? lineArrivalDepartureHeader = null,

            //  Lines View Page
            string? linesViewPageTitle = null,
            string? linesViewPageMenuItem = null,
            string? linesViewPageRefreshButton = null,

            //  Line Details View Page
            string? lineDetailsViewPageDepartures = null,
            string? lineDetailsViewPageDirectionSelection = null,

            //  Time Tables View Page
            string? timeTablesViewPageTitle = null)
        {
            Name = SetLanguageValue(name, "Polski");

            //  Components
            Messages = messages ?? new MessagesLangConfig();
            MpkCzestochowa = mpkCzestochowa ?? new MpkCzestochowaLangConfig();
            Settings = settings ?? new SettingsLangConfig();
            Ztm = ztm ?? new ZtmLangConfig();

            //  Main Menu
            MainMenuItem = SetLanguageValue(mainMenuItem, "Menu główne");
            StartPageMpkCzestochowaItem = SetLanguageValue(startPageMpkCzestochowaItem, "MPK Częstochowa");
            StartPageZtmMenuItem = SetLanguageValue(startPageZtmMenuItem, "Zarząd Transportu Metropolitalnego");
            StartPageSettingsMenuItem = SetLanguageValue(startPageSettingsMenuItem, "Ustawienia");

            //  Buttons
            CancelButton = SetLanguageValue(cancelButton, "Anuluj");
            CloseButton = SetLanguageValue(closeButton, "Zamknij");
            SelectButton = SetLanguageValue(selectButton, "Wybierz");

            //  Start Page
            StartPageTitle = SetLanguageValue(startPageTitle, "Wybór miasta (przedsiębiorstwa komunikacyjnego)");

            //  Line Arrivals IM
            LineArrivalsTitle = SetLanguageValue(lineArrivalsTitle, "Godziny przyjazdów");
            LineArrivalStopHeader = SetLanguageValue(lineArrivalStopHeader, "Nazwa przystanku");
            LineArrivalDepartureHeader = SetLanguageValue(lineArrivalDepartureHeader, "Godz.");

            //  Lines View Page
            LinesViewPageTitle = SetLanguageValue(linesViewPageTitle, "Wybór linii");
            LinesViewPageMenuItem = SetLanguageValue(linesViewPageMenuItem, "Wybór linii");
            LinesViewPageRefreshButton = SetLanguageValue(linesViewPageRefreshButton, "Odśwież");

            //  Line Details View Page
            LineDetailsViewPageDepartures = SetLanguageValue(lineDetailsViewPageDepartures, "Godziny odjazdów: ");
            LineDetailsViewPageDirectionSelection = SetLanguageValue(lineDetailsViewPageDirectionSelection, "Wybór kierunku: ");

            //  Time Tables View Page
            TimeTablesViewPageTitle = SetLanguageValue(timeTablesViewPageTitle, "Wybór rozkładu jazdy");
        }

        #endregion CLASS METHODS

    }
}
