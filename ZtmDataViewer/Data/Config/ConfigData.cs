using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZtmDataViewer.Data.Config
{
    public class ConfigData : INotifyPropertyChanged
    {

        //  CONST

        private static readonly string DEFAULT_LANG_FILE = "LangPL.json";
        private static readonly string DEFAULT_LANG_NAME = "Polski";


        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private AppearanceConfig _appearanceConfig;
        private Dictionary<string, string> _languages;
        private string _language;
        private Point _windowPosition;
        private Size _windowSize;


        //  GETTERS & SETTERS

        public AppearanceConfig AppearanceConfig
        {
            get => _appearanceConfig;
            set
            {
                _appearanceConfig = value;
                OnPropertyChanged(nameof(AppearanceConfig));
            }
        }

        [JsonIgnore]
        public Dictionary<string, string> Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                OnPropertyChanged(nameof(Languages));
                OnPropertyChanged(nameof(LanguageKeys));
            }
        }

        [JsonIgnore]
        public ObservableCollection<string> LanguageKeys
        {
            get => new ObservableCollection<string>(Languages.Select(kvp => kvp.Key));
        }

        public string Language
        {
            get => _language;
            set
            {
                _language = Languages.ContainsKey(value) ? value : DEFAULT_LANG_NAME;
                OnPropertyChanged(nameof(Language));
            }
        }

        public Point WindowPosition
        {
            get => _windowPosition;
            set
            {
                _windowPosition = value;
                OnPropertyChanged(nameof(WindowPosition));
            }
        }

        public Size WindowSize
        {
            get => _windowSize;
            set
            {
                _windowSize = value;
                OnPropertyChanged(nameof(WindowSize));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ConfigData class constructor. </summary>
        [JsonConstructor]
        public ConfigData(
            AppearanceConfig? appearanceConfig = null,
            Dictionary<string, string>? languages = null,
            string? language = null)
        {
            AppearanceConfig = appearanceConfig != null ? appearanceConfig : new AppearanceConfig();
            Languages = languages != null ? languages : new Dictionary<string, string>()
            {
                { "Polski", "LangPL.json" },
            };
            Language = !string.IsNullOrEmpty(language) ? language : DEFAULT_LANG_NAME;
        }

        #endregion CLASS METHODS

        #region LANGUAGE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get selected language file name method. </summary>
        /// <returns> Selected language file name. </returns>
        public string GetLanguageFileName()
        {
            if (Languages.ContainsKey(Language))
                return Languages[Language];
            else
                return DEFAULT_LANG_FILE;
        }

        #endregion LANGUAGE METHODSs

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Invoke PropertyChangedEventHandler event method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region UPDATE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Trigger properties update. </summary>
        public void TriggerPropertiesUpdate()
        {
            //  Components update.
            OnPropertyChanged(nameof(AppearanceConfig));


            //  Components triggered updates.
            AppearanceConfig.TriggerPropertiesUpdate();
        }

        #endregion UPDATE METHODS

    }
}
