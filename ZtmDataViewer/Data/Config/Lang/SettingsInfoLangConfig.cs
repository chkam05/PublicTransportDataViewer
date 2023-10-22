using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class SettingsInfoLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _settingsInfoPageTitle = string.Empty;
        private string _settingsInfoPageDescription = string.Empty;
        private string _settingsInfoPageAuthor = string.Empty;
        private string _settingsInfoPageCopyright = string.Empty;
        private string _settingsInfoPageLocation = string.Empty;


        //  GETTERS & SETTERS

        public string SettingsInfoPageTitle
        {
            get => _settingsInfoPageTitle;
            set => SetStringProperty(ref _settingsInfoPageTitle, nameof(SettingsInfoPageTitle), value);
        }

        public string SettingsInfoPageDescription
        {
            get => _settingsInfoPageDescription;
            set => SetStringProperty(ref _settingsInfoPageDescription, nameof(SettingsInfoPageDescription), value);
        }

        public string SettingsInfoPageAuthor
        {
            get => _settingsInfoPageAuthor;
            set => SetStringProperty(ref _settingsInfoPageAuthor, nameof(SettingsInfoPageAuthor), value);
        }

        public string SettingsInfoPageCopyright
        {
            get => _settingsInfoPageCopyright;
            set => SetStringProperty(ref _settingsInfoPageCopyright, nameof(SettingsInfoPageCopyright), value);
        }

        public string SettingsInfoPageLocation
        {
            get => _settingsInfoPageLocation;
            set => SetStringProperty(ref _settingsInfoPageLocation, nameof(SettingsInfoPageLocation), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsInfoLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsInfoLangConfig(
            string? settingsInfoPageTitle = null,
            string? settingsInfoPageDescription = null,
            string? settingsInfoPageAuthor = null,
            string? settingsInfoPageCopyright = null,
            string? settingsInfoPageLocation = null)
        {
            SettingsInfoPageTitle = SetLanguageValue(settingsInfoPageTitle, "Informacje");
            SettingsInfoPageDescription = SetLanguageValue(settingsInfoPageDescription, "Opis:");
            SettingsInfoPageAuthor = SetLanguageValue(settingsInfoPageAuthor, "Autor:");
            SettingsInfoPageCopyright = SetLanguageValue(settingsInfoPageCopyright, "Prawa autorskie:");
            SettingsInfoPageLocation = SetLanguageValue(settingsInfoPageLocation, "Lokalizacja:");
        }

        #endregion CLASS METHODS

    }
}
