using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class SettingsAppearanceLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _settingsAppearancePageTitle = string.Empty;
        private string _settingsAppearanceTheme = string.Empty;
        private string _settingsAppearanceThemeDesc = string.Empty;
        private string _settingsAppearanceThemeDark = string.Empty;
        private string _settingsAppearanceThemeLight = string.Empty;
        private string _settingsAppearnaceAccentColors = string.Empty;
        private string _settingsAppearanceAccentColorsHistory = string.Empty;


        //  GETTERS & SETTERS

        public string SettingsAppearancePageTitle
        {
            get => _settingsAppearancePageTitle;
            set => SetStringProperty(ref _settingsAppearancePageTitle, nameof(SettingsAppearancePageTitle), value);
        }

        public string SettingsAppearanceTheme
        {
            get => _settingsAppearanceTheme;
            set => SetStringProperty(ref _settingsAppearanceTheme, nameof(SettingsAppearanceTheme), value);
        }

        public string SettingsAppearanceThemeDesc
        {
            get => _settingsAppearanceThemeDesc;
            set => SetStringProperty(ref _settingsAppearanceThemeDesc, nameof(SettingsAppearanceThemeDesc), value);
        }

        public string SettingsAppearanceThemeDark
        {
            get => _settingsAppearanceThemeDark;
            set => SetStringProperty(ref _settingsAppearanceThemeDark, nameof(SettingsAppearanceThemeDark), value);
        }

        public string SettingsAppearanceThemeLight
        {
            get => _settingsAppearanceThemeLight;
            set => SetStringProperty(ref _settingsAppearanceThemeLight, nameof(SettingsAppearanceThemeLight), value);
        }

        public string SettingsAppearnaceAccentColors
        {
            get => _settingsAppearnaceAccentColors;
            set => SetStringProperty(ref _settingsAppearnaceAccentColors, nameof(SettingsAppearnaceAccentColors), value);
        }

        public string SettingsAppearanceAccentColorsHistory
        {
            get => _settingsAppearanceAccentColorsHistory;
            set => SetStringProperty(ref _settingsAppearanceAccentColorsHistory, nameof(SettingsAppearanceAccentColorsHistory), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsAppearanceLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsAppearanceLangConfig(
            string? settingsAppearancePageTitle = null,
            string? settingsAppearanceTheme = null,
            string? settingsAppearanceThemeDesc = null,
            string? settingsAppearanceThemeDark = null,
            string? settingsAppearanceThemeLight = null,
            string? settingsAppearnaceAccentColors = null,
            string? settingsAppearanceAccentColorsHistory = null)
        {
            SettingsAppearancePageTitle = SetLanguageValue(settingsAppearancePageTitle, "Personalizacja");
            SettingsAppearanceTheme = SetLanguageValue(settingsAppearanceTheme, "Motyw");
            SettingsAppearanceThemeDesc = SetLanguageValue(settingsAppearanceThemeDesc, "Ustaw motyw aplikacji.");
            SettingsAppearanceThemeDark = SetLanguageValue(settingsAppearanceThemeDark, "Ciemny");
            SettingsAppearanceThemeLight = SetLanguageValue(settingsAppearanceThemeLight, "Jasny");
            SettingsAppearnaceAccentColors = SetLanguageValue(settingsAppearnaceAccentColors, "Wybór koloru akcentu:");
            SettingsAppearanceAccentColorsHistory = SetLanguageValue(settingsAppearanceAccentColorsHistory, "Ostatnio wybierane kolory:");
        }

    }
}
