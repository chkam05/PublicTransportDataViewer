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

        private SettingsAppearanceLangConfig? _appearance = null;
        private SettingsGeneralLangConfig? _general = null;
        private SettingsInfoLangConfig? _info = null;

        private string _menuItemAppearance = string.Empty;
        private string _menuItemAppearanceDesc = string.Empty;
        private string _menuItemGeneral = string.Empty;
        private string _menuItemGeneralDesc = string.Empty;
        private string _menuItemInfo = string.Empty;
        private string _menuItemInfoDesc = string.Empty;
        private string _title = string.Empty;


        //  GETTERS & SETTERS

        public SettingsAppearanceLangConfig Appearance
        {
            get
            {
                if (_appearance == null)
                    _appearance = new SettingsAppearanceLangConfig();

                return _appearance;
            }
            set
            {
                _appearance = value;
                OnPropertyChanged(nameof(Appearance));
            }
        }

        public SettingsGeneralLangConfig General
        {
            get
            {
                if (_general == null)
                    _general = new SettingsGeneralLangConfig();

                return _general;
            }
            set
            {
                _general = value;
                OnPropertyChanged(nameof(General));
            }
        }

        public SettingsInfoLangConfig Info
        {
            get
            {
                if (_info == null)
                    _info = new SettingsInfoLangConfig();

                return _info;
            }
            set
            {
                _info = value;
                OnPropertyChanged(nameof(Info));
            }
        }


        public string MenuItemAppearance
        {
            get => _menuItemAppearance;
            set => SetStringProperty(ref _menuItemAppearance, nameof(MenuItemAppearance), value);
        }

        public string MenuItemAppearanceDesc
        {
            get => _menuItemAppearanceDesc;
            set => SetStringProperty(ref _menuItemAppearanceDesc, nameof(MenuItemAppearanceDesc), value);
        }

        public string MenuItemGeneral
        {
            get => _menuItemGeneral;
            set => SetStringProperty(ref _menuItemGeneral, nameof(MenuItemGeneral), value);
        }

        public string MenuItemGeneralDesc
        {
            get => _menuItemGeneralDesc;
            set => SetStringProperty(ref _menuItemGeneralDesc, nameof(MenuItemGeneralDesc), value);
        }

        public string MenuItemInfo
        {
            get => _menuItemInfo;
            set => SetStringProperty(ref _menuItemInfo, nameof(MenuItemInfo), value);
        }

        public string MenuItemInfoDesc
        {
            get => _menuItemInfoDesc;
            set => SetStringProperty(ref _menuItemInfoDesc, nameof(MenuItemInfoDesc), value);
        }

        public string Title
        {
            get => _title;
            set => SetStringProperty(ref _title, nameof(Title), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsLangConfig(
            SettingsAppearanceLangConfig? appearance = null,
            SettingsGeneralLangConfig? general = null,
            SettingsInfoLangConfig? info = null,

            string? menuItemAppearance = null,
            string? menuItemAppearanceDesc = null,
            string? menuItemGeneral = null,
            string? menuItemGeneralDesc = null,
            string? menuItemInfo = null,
            string? menuItemInfoDesc = null,
            string? title = null)
        {
            Appearance = appearance ?? new SettingsAppearanceLangConfig();
            General = general ?? new SettingsGeneralLangConfig();
            Info = info ?? new SettingsInfoLangConfig();

            MenuItemAppearance = SetLanguageValue(menuItemAppearance, "Personalizacja");
            MenuItemAppearanceDesc = SetLanguageValue(menuItemAppearanceDesc, "Konfiguracja motywu i wyglądu aplikacji.");
            MenuItemGeneral = SetLanguageValue(menuItemGeneral, "Ogólne");
            MenuItemGeneralDesc = SetLanguageValue(menuItemGeneralDesc, "Ogólne opcje konfiguracji.");
            MenuItemInfo = SetLanguageValue(menuItemInfo, "Informacje");
            MenuItemInfoDesc = SetLanguageValue(menuItemInfoDesc, "Informacje o aplikacji.");
            Title = SetLanguageValue(title, "Ustawienia");
        }

        #endregion CLASS METHODS

    }
}
