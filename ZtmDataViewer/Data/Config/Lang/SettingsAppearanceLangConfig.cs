using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang
{
    public class SettingsAppearanceLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _colorsHistoryTitle = string.Empty;
        private string _colorsTitle = string.Empty;
        private string _themeDesc = string.Empty;
        private string _themeOptionDark = string.Empty;
        private string _themeOptionLight = string.Empty;
        private string _themeTitle = string.Empty;


        //  GETTERS & SETTERS

        public string ColorsHistoryTitle
        {
            get => _colorsHistoryTitle;
            set => SetStringProperty(ref _colorsHistoryTitle, nameof(ColorsHistoryTitle), value);
        }

        public string ColorsTitle
        {
            get => _colorsTitle;
            set => SetStringProperty(ref _colorsTitle, nameof(ColorsTitle), value);
        }

        public string ThemeDesc
        {
            get => _themeDesc;
            set => SetStringProperty(ref _themeDesc, nameof(ThemeDesc), value);
        }

        public string ThemeOptionDark
        {
            get => _themeOptionDark;
            set => SetStringProperty(ref _themeOptionDark, nameof(ThemeOptionDark), value);
        }

        public string ThemeOptionLight
        {
            get => _themeOptionLight;
            set => SetStringProperty(ref _themeOptionLight, nameof(ThemeOptionLight), value);
        }

        public string ThemeTitle
        {
            get => _themeTitle;
            set => SetStringProperty(ref _themeTitle, nameof(ThemeTitle), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsAppearanceLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsAppearanceLangConfig(
            string? colorsHistoryTitle = null,
            string? colorsTitle = null,
            string? themeDesc = null,
            string? themeOptionDark = null,
            string? themeOptionLight = null,
            string? themeTitle = null)
        {
            ColorsHistoryTitle = SetLanguageValue(colorsHistoryTitle, "Ostatnio wybierane kolory: ");
            ColorsTitle = SetLanguageValue(colorsTitle, "Wybór koloru akcentu: ");
            ThemeDesc = SetLanguageValue(themeDesc, "Ustaw motyw aplikacji.");
            ThemeOptionDark = SetLanguageValue(themeOptionDark, "Ciemny");
            ThemeOptionLight = SetLanguageValue(themeOptionLight, "Jasny");
            ThemeTitle = SetLanguageValue(themeTitle, "Motyw");
        }

        #endregion CLASS METHODS

    }
}
