using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang
{
    public class SettingsInfoLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _author = string.Empty;
        private string _copyright = string.Empty;
        private string _dataSources = string.Empty;
        private string _description = string.Empty;


        //  GETTERS & SETTERS

        public string Author
        {
            get => _author;
            set => SetStringProperty(ref _author, nameof(Author), value);
        }

        public string Copyright
        {
            get => _copyright;
            set => SetStringProperty(ref _copyright, nameof(Copyright), value);
        }

        public string DataSources
        {
            get => _dataSources;
            set => SetStringProperty(ref _dataSources, nameof(DataSources), value);
        }

        public string Description
        {
            get => _description;
            set => SetStringProperty(ref _description, nameof(Description), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsInfoLangConfig class constructor. </summary>
        [JsonConstructor]
        public SettingsInfoLangConfig(
            string? author = null,
            string? copyright = null,
            string? dataSources = null,
            string? description = null)
        {
            Author = SetLanguageValue(author, "Autor: ");
            Copyright = SetLanguageValue(copyright, "Prawa autorskie: ");
            DataSources = SetLanguageValue(dataSources, "Źródła danych: ");
            Description = SetLanguageValue(description, "Opis: ");
        }

        #endregion CLASS METHODS

    }
}
