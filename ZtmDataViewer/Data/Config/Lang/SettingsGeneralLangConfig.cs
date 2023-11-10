using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang
{
    public class SettingsGeneralLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _langDesc = string.Empty;
        private string _langTitle = string.Empty;


        //  GETTERS & SETTERS

        public string LangDesc
        {
            get => _langDesc;
            set => SetStringProperty(ref _langDesc, nameof(LangDesc), value);
        }

        public string LangTitle
        {
            get => _langTitle;
            set => SetStringProperty(ref _langTitle, nameof(LangTitle), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsGeneralLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsGeneralLangConfig(
            string? langDesc = null,
            string? langTitle = null)
        {
            LangDesc = SetLanguageValue(langDesc, "Wybór języka aplikacji.");
            LangTitle = SetLanguageValue(langTitle, "Język");
        }

        #endregion CLASS METHODS

    }
}
