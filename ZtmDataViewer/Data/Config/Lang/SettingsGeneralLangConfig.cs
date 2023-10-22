using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class SettingsGeneralLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _settingsGeneralPageLanguage = string.Empty;
        private string _settingsGeneralPageLanguageDesc = string.Empty;


        //  GETTERS & SETTERS

        public string SettingsGeneralPageLanguage
        {
            get => _settingsGeneralPageLanguage;
            set => SetStringProperty(ref _settingsGeneralPageLanguage, nameof(SettingsGeneralPageLanguage), value);
        }

        public string SettingsGeneralPageLanguageDesc
        {
            get => _settingsGeneralPageLanguageDesc;
            set => SetStringProperty(ref _settingsGeneralPageLanguageDesc, nameof(SettingsGeneralPageLanguageDesc), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsGeneralLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsGeneralLangConfig(
            string? settingsGeneralPageLanguage = null,
            string? settingsGeneralPageLanguageDesc = null)
        {
            SettingsGeneralPageLanguage = SetLanguageValue(settingsGeneralPageLanguage, "Język");
            SettingsGeneralPageLanguageDesc = SetLanguageValue(settingsGeneralPageLanguageDesc, "Wybór języka aplikacji.");
        }

        #endregion CLASS METHODS

    }
}
