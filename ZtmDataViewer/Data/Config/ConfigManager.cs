﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZtmDataViewer.Data.Config.Lang;
using ZtmDataViewer.Data.Info;

namespace ZtmDataViewer.Data.Config
{
    public class ConfigManager : INotifyPropertyChanged, IDisposable
    {

        //  CONST

        private static readonly string CONFIG_FILE_NAME = "config.json";
        private static readonly string LANGS_CATALOG_NAME = "Languages";


        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private static ConfigManager? _instance = null;
        private static object _instanceLock = new object();

        private ConfigData _configData;
        private LangConfig _langConfig;


        //  GETTERS & SETTERS

        public static ConfigManager Instance
        {
            get => GetInstance();
        }

        public ConfigData ConfigData
        {
            get => _configData;
            set
            {
                _configData = value;
                _configData.PropertyChanged += OnConfigPropertyChanged;
                OnPropertyChanged(nameof(ConfigData));
                _configData.TriggerPropertiesUpdate();
            }
        }

        public LangConfig LangConfig
        {
            get => _langConfig;
            set
            {
                _langConfig = value;
                OnPropertyChanged(nameof(LangConfig));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ConfigManager private class constructor (for singleton instance). </summary>
        private ConfigManager()
        {
            //
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get ConfigManager class singleton instance. </summary>
        /// <returns> ConfigManager class singleton instance. </returns>
        private static ConfigManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigManager();
                        _instance.LoadConfigurationFromFile();
                        _instance.LoadLanguages();
                        _instance.LoadLanguage();
                    }
                }
            }

            return _instance;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Performs application-defined tasks associated with freeing, releasing, 
        /// and resetting unmanaged resources. </summary>
        public void Dispose()
        {
            SaveConfigurationToFile();
        }

        #endregion CLASS METHODS

        #region LANGUAGE MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Load languages. </summary>
        private void LoadLanguages()
        {
            var appInfo = AppInfoManager.Instance;
            var languagesPath = Path.Combine(appInfo.AppPath, LANGS_CATALOG_NAME);
            var result = new Dictionary<string, string>();

            if (!Directory.Exists(languagesPath))
                Directory.CreateDirectory(languagesPath);

            foreach (string filePath in Directory.GetFiles(languagesPath)
                .Where(f => f.EndsWith(".json")))
            {
                try
                {
                    var rawLanguageConfig = File.ReadAllText(filePath);
                    var languageConfig = JsonConvert.DeserializeObject<LangConfig>(rawLanguageConfig);

                    if (!string.IsNullOrEmpty(languageConfig?.Name) && !result.ContainsKey(languageConfig.Name))
                        result.Add(languageConfig.Name, Path.GetFileName(filePath));
                }
                catch (Exception)
                {
                    //  Nothing to do.
                }
            }

            if (!result.Any())
                result.Add("Polski", "LangPL.json");

            ConfigData.Languages = result;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get language file path. </summary>
        private string? GetLanguageFilePath()
        {
            string? langFileName = ConfigData.Languages.ContainsKey(ConfigData.Language)
                ? ConfigData.Languages[ConfigData.Language] : null;

            if (langFileName == null)
                return null;

            var appInfo = AppInfoManager.Instance;
            string langFilePath = Path.Combine(appInfo.AppPath, LANGS_CATALOG_NAME, langFileName);

            return File.Exists(langFilePath) ? langFilePath : null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load language. </summary>
        private void LoadLanguage()
        {
            string? languageFilePath = GetLanguageFilePath();

            if (!string.IsNullOrEmpty(languageFilePath))
            {
                var rawLanguageConfig = File.ReadAllText(languageFilePath);
                var languageConfig = JsonConvert.DeserializeObject<LangConfig>(rawLanguageConfig);

                if (languageConfig != null)
                {
                    LangConfig = languageConfig;
                    return;
                }
            }

            LangConfig = new LangConfig();
        }

        #endregion LANGUAGE MANAGEMENT METHODS

        #region LOAD AND SAVE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get configuration file path. </summary>
        /// <returns> Configuration file path. </returns>
        private string GetConfigurationFilePath()
        {
            var appInfo = AppInfoManager.Instance;

            string basePath = Environment.GetEnvironmentVariable("APPDATA") ?? appInfo.AppPath;
            string configPath = Path.Combine(basePath, appInfo.AppName);

            if (!Directory.Exists(configPath))
                Directory.CreateDirectory(configPath);

            return Path.Combine(configPath, CONFIG_FILE_NAME);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load configuration from file or default configuration. </summary>
        private void LoadConfigurationFromFile()
        {
            string configFilePath = GetConfigurationFilePath();

            if (File.Exists(configFilePath))
            {
                var rawConfigData = File.ReadAllText(configFilePath);
                var configData = JsonConvert.DeserializeObject<ConfigData>(rawConfigData);

                if (configData != null)
                {
                    ConfigData = configData;
                    return;
                }
            }

            ConfigData = new ConfigData();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Save configuration to file. </summary>
        public void SaveConfigurationToFile()
        {
            string configFilePath = GetConfigurationFilePath();

            var rawConfigData = JsonConvert.SerializeObject(ConfigData, Formatting.Indented);
            File.WriteAllText(configFilePath, rawConfigData);
        }

        #endregion LOAD AND SAVE METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after changing language. </summary>
        /// <param name="sender"> Object that invoked the mehtod. </param>
        /// <param name="e"> Property Changed Event Arguments. </param>
        private void OnConfigPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ConfigData.Language))
                LoadLanguage();
        }

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

    }
}
