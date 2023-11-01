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

        private MessagesLangConfig? _messages = null;
        private MpkCzestochowaLangConfig? _mpkCzestochowa = null;
        private SettingsLangConfig? _settings = null;
        private ZtmLangConfig? _ztm = null;

        private string _arrivalsDeparture = string.Empty;
        private string _arrivalsStop = string.Empty;
        private string _arrivalsTitle = string.Empty;

        private string _buttonCancel = string.Empty;
        private string _buttonClose = string.Empty;
        private string _buttonMessages = string.Empty;
        private string _buttonRefresh = string.Empty;
        private string _buttonSelect = string.Empty;

        private string _lineDetailsDepartures = string.Empty;
        private string _lineDetailsDirection = string.Empty;

        private string _linesTitle = string.Empty;

        private string _menuItemLines = string.Empty;
        private string _menuItemMain = string.Empty;
        private string _menuItemMpkCzestochowa = string.Empty;
        private string _menuItemSettings = string.Empty;
        private string _menuItemZtm = string.Empty;

        private string _startPageTitle = string.Empty;

        private string _timeTablesTitle = string.Empty;


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


        public MessagesLangConfig Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new MessagesLangConfig();

                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        
        public MpkCzestochowaLangConfig MpkCzestochowa
        {
            get
            {
                if (_mpkCzestochowa == null)
                    _mpkCzestochowa = new MpkCzestochowaLangConfig();

                return _mpkCzestochowa;
            }
            set
            {
                _mpkCzestochowa = value;
                OnPropertyChanged(nameof(MpkCzestochowa));
            }
        }

        public SettingsLangConfig Settings
        {
            get
            {
                if (_settings == null)
                    _settings = new SettingsLangConfig();

                return _settings;
            }
            set
            {
                _settings = value;
                OnPropertyChanged(nameof(Settings));
            }
        }

        public ZtmLangConfig Ztm
        {
            get
            {
                if (_ztm == null)
                    _ztm = new ZtmLangConfig();

                return _ztm;
            }
            set
            {
                _ztm = value;
                OnPropertyChanged(nameof(Ztm));
            }
        }


        public string ArrivalsDeparture
        {
            get => _arrivalsDeparture;
            set => SetStringProperty(ref _arrivalsDeparture, nameof(ArrivalsDeparture), value);
        }

        public string ArrivalsStop
        {
            get => _arrivalsStop;
            set => SetStringProperty(ref _arrivalsStop, nameof(ArrivalsStop), value);
        }

        public string ArrivalsTitle
        {
            get => _arrivalsTitle;
            set => SetStringProperty(ref _arrivalsTitle, nameof(ArrivalsTitle), value);
        }

        public string ButtonCancel
        {
            get => _buttonCancel;
            set => SetStringProperty(ref _buttonCancel, nameof(ButtonCancel), value);
        }

        public string ButtonClose
        {
            get => _buttonClose;
            set => SetStringProperty(ref _buttonClose, nameof(ButtonClose), value);
        }

        public string ButtonMessages
        {
            get => _buttonMessages;
            set => SetStringProperty(ref _buttonMessages, nameof(ButtonMessages), value);
        }

        public string ButtonRefresh
        {
            get => _buttonRefresh;
            set => SetStringProperty(ref _buttonRefresh, nameof(ButtonRefresh), value);
        }

        public string ButtonSelect
        {
            get => _buttonSelect;
            set => SetStringProperty(ref _buttonSelect, nameof(ButtonSelect), value);
        }

        public string LineDetailsDepartures
        {
            get => _lineDetailsDepartures;
            set => SetStringProperty(ref _lineDetailsDepartures, nameof(LineDetailsDepartures), value);
        }

        public string LineDetailsDirection
        {
            get => _lineDetailsDirection;
            set => SetStringProperty(ref _lineDetailsDirection, nameof(LineDetailsDirection), value);
        }

        public string LinesTitle
        {
            get => _linesTitle;
            set => SetStringProperty(ref _linesTitle, nameof(LinesTitle), value);
        }

        public string MenuItemLines
        {
            get => _menuItemLines;
            set => SetStringProperty(ref _menuItemLines, nameof(MenuItemLines), value);
        }

        public string MenuItemMain
        {
            get => _menuItemMain;
            set => SetStringProperty(ref _menuItemMain, nameof(MenuItemMain), value);
        }

        public string MenuItemMpkCzestochowa
        {
            get => _menuItemMpkCzestochowa;
            set => SetStringProperty(ref _menuItemMpkCzestochowa, nameof(MenuItemMpkCzestochowa), value);
        }

        public string MenuItemSettings
        {
            get => _menuItemSettings;
            set => SetStringProperty(ref _menuItemSettings, nameof(MenuItemSettings), value);
        }

        public string MenuItemZtm
        {
            get => _menuItemZtm;
            set => SetStringProperty(ref _menuItemZtm, nameof(MenuItemZtm), value);
        }

        public string StartPageTitle
        {
            get => _startPageTitle;
            set => SetStringProperty(ref _startPageTitle, nameof(StartPageTitle), value);
        }

        public string TimeTablesTitle
        {
            get => _timeTablesTitle;
            set => SetStringProperty(ref _timeTablesTitle, nameof(TimeTablesTitle), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LangConfig class constructor. </summary>
        [JsonConstructor]
        public LangConfig(
            string? name = null,

            MessagesLangConfig? messages = null,
            MpkCzestochowaLangConfig? mpkCzestochowa = null,
            SettingsLangConfig? settings = null,
            ZtmLangConfig? ztm = null,

            string? arrivalsDeparture = null,
            string? arrivalsStop = null,
            string? arrivalsTitle = null,
            string? buttonCancel = null,
            string? buttonClose = null,
            string? buttonMessages = null,
            string? buttonRefresh = null,
            string? buttonSelect = null,
            string? lineDetailsDepartures = null,
            string? lineDetailsDirection = null,
            string? linesTitle = null,
            string? menuItemLines = null,
            string? menuItemMain = null,
            string? menuItemMpkCzestochowa = null,
            string? menuItemSettings = null,
            string? menuItemZtm = null,
            string? startPageTitle = null,
            string? timeTablesTitle = null)
        {
            Name = SetLanguageValue(name, "Polski");

            Messages = messages ?? new MessagesLangConfig();
            MpkCzestochowa = mpkCzestochowa ?? new MpkCzestochowaLangConfig();
            Settings = settings ?? new SettingsLangConfig();
            Ztm = ztm ?? new ZtmLangConfig();

            ArrivalsDeparture = SetLanguageValue(arrivalsDeparture, "Godz.");
            ArrivalsStop = SetLanguageValue(arrivalsStop, "Nazwa przystanku");
            ArrivalsTitle = SetLanguageValue(arrivalsTitle, "Godziny przyjazdów");
            ButtonCancel = SetLanguageValue(buttonCancel, "Anuluj");
            ButtonClose = SetLanguageValue(buttonClose, "Zamknij");
            ButtonMessages = SetLanguageValue(buttonMessages, "Komunikaty");
            ButtonRefresh = SetLanguageValue(buttonRefresh, "Odśwież");
            ButtonSelect = SetLanguageValue(buttonSelect, "Wybierz");
            LineDetailsDepartures = SetLanguageValue(lineDetailsDepartures, "Godziny odjazdów: ");
            LineDetailsDirection = SetLanguageValue(lineDetailsDirection, "Wybór kierunku: ");
            LinesTitle = SetLanguageValue(linesTitle, "Wybór linii");
            MenuItemLines = SetLanguageValue(menuItemLines, "Wybór linii");
            MenuItemMain = SetLanguageValue(menuItemMain, "Menu główne");
            MenuItemMpkCzestochowa = SetLanguageValue(menuItemMpkCzestochowa, "MPK Częstochowa");
            MenuItemSettings = SetLanguageValue(menuItemSettings, "Ustawienia");
            MenuItemZtm = SetLanguageValue(menuItemZtm, "Zarząd Transportu Metropolitalnego");
            StartPageTitle = SetLanguageValue(startPageTitle, "Wybór miasta (przedsiębiorstwa komunikacyjnego)");
            TimeTablesTitle = SetLanguageValue(timeTablesTitle, "Wybór rozkładu jazdy");
        }

        #endregion CLASS METHODS

    }
}
