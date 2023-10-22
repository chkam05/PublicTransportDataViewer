using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataViewer.Data.Config.Lang.MpkCzestochowa;
using ZtmDataViewer.Data.Config.Lang.Ztm;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class LangConfig : BaseLangConfig
    {

        //  ATTRIBUTES

        private string _name = string.Empty;


        //  VARIABLES

        private MessagesLangConfig? _messagesLangConfig = null;
        private SettingsAppearanceLangConfig? _settingsAppearanceLangConfig = null;
        private SettingsGeneralLangConfig? _settingsGeneralLangConfig = null;
        private SettingsInfoLangConfig? _settingsInfoLangConfig = null;
        private SettingsLangConfig? _settingsLangConfig = null;
        private MpkCzestochowaLinesViewLangConfig? _mpkCzestochowaLinesViewLangConfig = null;
        private MpkCzestochowaTransportTypesLangConfig? _mpkCzestochowaTransportTypesLangConfig = null;
        private ZtmArrivalsIMLangConfig? _ztmArrivalsIMLangConfig = null;
        private ZtmDeparturesLangConfig? _ztmDeparturesLangConfig = null;
        private ZtmLineDetailsViewLangConfig? _ztmLineDetailsViewLangConfig = null;
        private ZtmTimeTableSelectorIMLangConfig? _ztmTimeTableSelectorIMLangConfig = null;
        private ZtmTransportTypesLangConfig? _ztmTransportTypesLangConfig = null;

        //  Main Menu
        private string _mainMenuItem = string.Empty;
        private string _startPageMpkCzestochowaItem = string.Empty;
        private string _startPageZtmMenuItem = string.Empty;
        private string _startPageSettingsMenuItem = string.Empty;

        //  Start Page
        private string _startPageTitle = string.Empty;


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

        public SettingsAppearanceLangConfig SettingsAppearance
        {
            get
            {
                if (_settingsAppearanceLangConfig == null)
                {
                    _settingsAppearanceLangConfig = new SettingsAppearanceLangConfig();
                    OnPropertyChanged(nameof(SettingsAppearance));
                }
                return _settingsAppearanceLangConfig;
            }
            set
            {
                _settingsAppearanceLangConfig = value;
                OnPropertyChanged(nameof(SettingsAppearance));
            }
        }

        public SettingsGeneralLangConfig SettingsGeneral
        {
            get
            {
                if (_settingsGeneralLangConfig == null)
                {
                    _settingsGeneralLangConfig = new SettingsGeneralLangConfig();
                    OnPropertyChanged(nameof(SettingsGeneral));
                }
                return _settingsGeneralLangConfig;
            }
            set
            {
                _settingsGeneralLangConfig = value;
                OnPropertyChanged(nameof(SettingsGeneral));
            }
        }

        public SettingsInfoLangConfig SettingsInfo
        {
            get
            {
                if (_settingsInfoLangConfig == null)
                {
                    _settingsInfoLangConfig = new SettingsInfoLangConfig();
                    OnPropertyChanged(nameof(SettingsInfo));
                }
                return _settingsInfoLangConfig;
            }
            set
            {
                _settingsInfoLangConfig = value;
                OnPropertyChanged(nameof(SettingsInfo));
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

        public MpkCzestochowaLinesViewLangConfig MpkCzestochowaLinesView
        {
            get
            {
                if (_mpkCzestochowaLinesViewLangConfig == null)
                {
                    _mpkCzestochowaLinesViewLangConfig = new MpkCzestochowaLinesViewLangConfig();
                    OnPropertyChanged(nameof(MpkCzestochowaLinesView));
                }
                return _mpkCzestochowaLinesViewLangConfig;
            }
            set
            {
                _mpkCzestochowaLinesViewLangConfig = value;
                OnPropertyChanged(nameof(MpkCzestochowaLinesView));
            }
        }

        public MpkCzestochowaTransportTypesLangConfig MpkCzestochowaTransportTypes
        {
            get
            {
                if (_mpkCzestochowaTransportTypesLangConfig == null)
                {
                    _mpkCzestochowaTransportTypesLangConfig = new MpkCzestochowaTransportTypesLangConfig();
                    OnPropertyChanged(nameof(MpkCzestochowaTransportTypes));
                }
                return _mpkCzestochowaTransportTypesLangConfig;
            }
            set
            {
                _mpkCzestochowaTransportTypesLangConfig = value;
                OnPropertyChanged(nameof(MpkCzestochowaTransportTypes));
            }
        }

        public ZtmArrivalsIMLangConfig ZtmArrivalsIM
        {
            get
            {
                if (_ztmArrivalsIMLangConfig == null)
                {
                    _ztmArrivalsIMLangConfig = new ZtmArrivalsIMLangConfig();
                    OnPropertyChanged(nameof(ZtmArrivalsIM));
                }
                return _ztmArrivalsIMLangConfig;
            }
            set
            {
                _ztmArrivalsIMLangConfig = value;
                OnPropertyChanged(nameof(ZtmArrivalsIM));
            }
        }

        public ZtmDeparturesLangConfig ZtmDepartures
        {
            get
            {
                if (_ztmDeparturesLangConfig == null)
                {
                    _ztmDeparturesLangConfig = new ZtmDeparturesLangConfig();
                    OnPropertyChanged(nameof(ZtmDepartures));
                }
                return _ztmDeparturesLangConfig;
            }
            set
            {
                _ztmDeparturesLangConfig = value;
                OnPropertyChanged(nameof(ZtmDepartures));
            }
        }

        public ZtmLineDetailsViewLangConfig ZtmLineDetailsView
        {
            get
            {
                if (_ztmLineDetailsViewLangConfig == null)
                {
                    _ztmLineDetailsViewLangConfig = new ZtmLineDetailsViewLangConfig();
                    OnPropertyChanged(nameof(ZtmLineDetailsView));
                }
                return _ztmLineDetailsViewLangConfig;
            }
            set
            {
                _ztmLineDetailsViewLangConfig = value;
                OnPropertyChanged(nameof(ZtmLineDetailsView));
            }
        }

        public ZtmTimeTableSelectorIMLangConfig ZtmTimeTableSelectorIM
        {
            get
            {
                if (_ztmTimeTableSelectorIMLangConfig == null)
                {
                    _ztmTimeTableSelectorIMLangConfig = new ZtmTimeTableSelectorIMLangConfig();
                    OnPropertyChanged(nameof(ZtmTimeTableSelectorIM));
                }
                return _ztmTimeTableSelectorIMLangConfig;
            }
            set
            {
                _ztmTimeTableSelectorIMLangConfig = value;
                OnPropertyChanged(nameof(ZtmTimeTableSelectorIM));
            }
        }

        public ZtmTransportTypesLangConfig ZtmTransportTypes
        {
            get
            {
                if (_ztmTransportTypesLangConfig == null)
                {
                    _ztmTransportTypesLangConfig = new ZtmTransportTypesLangConfig();
                    OnPropertyChanged(nameof(ZtmTransportTypes));
                }
                return _ztmTransportTypesLangConfig;
            }
            set
            {
                _ztmTransportTypesLangConfig = value;
                OnPropertyChanged(nameof(ZtmTransportTypes));
            }
        }


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

        #region Start Page

        public string StartPageTitle
        {
            get => _startPageTitle;
            set => SetStringProperty(ref _startPageTitle, nameof(StartPageTitle), value);
        }

        #endregion Start Page


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LangConfig class constructor. </summary>
        [JsonConstructor]
        public LangConfig(
            string? name = null,

            //  Main Menu
            string? mainMenuItem = null,
            string? startPageMpkCzestochowaItem = null,
            string? startPageZtmMenuItem = null,

            //  Start Page
            string? startPageSettingsMenuItem = null,

            //  Start Page
            string? startPageTitle = null)
        {
            Name = SetLanguageValue(name, "Polski");
            MainMenuItem = SetLanguageValue(mainMenuItem, "Menu główne");
            StartPageMpkCzestochowaItem = SetLanguageValue(startPageMpkCzestochowaItem, "MPK Czestochowa");
            StartPageZtmMenuItem = SetLanguageValue(startPageZtmMenuItem, "Zarząd Transportu Metropolitalnego");
            StartPageSettingsMenuItem = SetLanguageValue(startPageSettingsMenuItem, "Ustawienia");
            StartPageTitle = SetLanguageValue(startPageTitle, "Wybór miasta (przedsiębiorstwa komunikacyjnego)");
        }

        #endregion CLASS METHODS

    }
}
