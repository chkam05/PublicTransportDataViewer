using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class SettingsLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private SettingsAppearanceLangConfig? _settingsAppearanceLangConfig = null;
        private SettingsGeneralLangConfig? _settingsGeneralLangConfig = null;
        private SettingsInfoLangConfig? _settingsInfoLangConfig = null;

        private string _settingsPageTitle = string.Empty;
        private string _settingsPageAppearanceMenuItem = string.Empty;
        private string _settingsPageAppearanceMenuItemDesc = string.Empty;
        private string _settingsPageGeneralMenuItem = string.Empty;
        private string _settingsPageGeneralMenuItemDesc = string.Empty;
        private string _settingsPageInfoMenuItem = string.Empty;
        private string _settingsPageInfoMenuItemDesc = string.Empty;


        //  GETTERS & SETTERS

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

        public string SettingsPageTitle
        {
            get => _settingsPageTitle;
            set => SetStringProperty(ref _settingsPageTitle, nameof(SettingsPageTitle), value);
        }

        public string SettingsPageAppearanceMenuItem
        {
            get => _settingsPageAppearanceMenuItem;
            set => SetStringProperty(ref _settingsPageAppearanceMenuItem, nameof(SettingsPageAppearanceMenuItem), value);
        }

        public string SettingsPageAppearanceMenuItemDesc
        {
            get => _settingsPageAppearanceMenuItemDesc;
            set => SetStringProperty(ref _settingsPageAppearanceMenuItemDesc, nameof(SettingsPageAppearanceMenuItemDesc), value);
        }

        public string SettingsPageGeneralMenuItem
        {
            get => _settingsPageGeneralMenuItem;
            set => SetStringProperty(ref _settingsPageGeneralMenuItem, nameof(SettingsPageGeneralMenuItem), value);
        }

        public string SettingsPageGeneralMenuItemDesc
        {
            get => _settingsPageGeneralMenuItemDesc;
            set => SetStringProperty(ref _settingsPageGeneralMenuItemDesc, nameof(SettingsPageGeneralMenuItemDesc), value);
        }

        public string SettingsPageInfoMenuItem
        {
            get => _settingsPageInfoMenuItem;
            set => SetStringProperty(ref _settingsPageInfoMenuItem, nameof(SettingsPageInfoMenuItem), value);
        }

        public string SettingsPageInfoMenuItemDesc
        {
            get => _settingsPageInfoMenuItemDesc;
            set => SetStringProperty(ref _settingsPageInfoMenuItemDesc, nameof(SettingsPageAppearanceMenuItemDesc), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsLangConfig(
            SettingsAppearanceLangConfig? settingsAppearance = null,
            SettingsGeneralLangConfig? settingsGeneral = null,
            SettingsInfoLangConfig? settingsInfo = null,

            string? settingsPageTitle = null,
            string? settingsPageAppearanceMenuItem = null,
            string? settingsPageAppearanceMenuItemDesc = null,
            string? settingsPageGeneralMenuItem = null,
            string? settingsPageGeneralMenuItemDesc = null,
            string? settingsPageInfoMenuItem = null,
            string? settingsPageInfoMenuItemDesc = null)
        {
            SettingsAppearance = settingsAppearance ?? new SettingsAppearanceLangConfig();
            SettingsGeneral = settingsGeneral ?? new SettingsGeneralLangConfig();
            SettingsInfo = settingsInfo ?? new SettingsInfoLangConfig();

            SettingsPageTitle = SetLanguageValue(settingsPageTitle, "Ustawienia");
            SettingsPageAppearanceMenuItem = SetLanguageValue(settingsPageAppearanceMenuItem, "Personalizacja");
            SettingsPageAppearanceMenuItemDesc = SetLanguageValue(settingsPageAppearanceMenuItemDesc, "Konfiguracja motywu i wyglądu aplikacji.");
            SettingsPageGeneralMenuItem = SetLanguageValue(settingsPageGeneralMenuItem, "Ogólne");
            SettingsPageGeneralMenuItemDesc = SetLanguageValue(settingsPageGeneralMenuItemDesc, "Ogólne opcje konfiguracji.");
            SettingsPageInfoMenuItem = SetLanguageValue(settingsPageInfoMenuItem, "Informacje");
            SettingsPageInfoMenuItemDesc = SetLanguageValue(settingsPageInfoMenuItemDesc, "Informacje o aplikacji.");
        }

        #endregion CLASS METHODS

    }
}
