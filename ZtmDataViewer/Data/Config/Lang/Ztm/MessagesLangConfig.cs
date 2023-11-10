using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang.Ztm
{
    public class MessagesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _title = string.Empty;


        //  GETTERS & SETTERS

        public string Title
        {
            get => _title;
            set => SetStringProperty(ref _title, nameof(Title), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MessagesLangConfig class constructor. </summary>
        [JsonConstructor]
        public MessagesLangConfig(
            string? title = null)
        {
            Title = SetLanguageValue(title, "Komunikaty: ");
        }

        #endregion CLASS METHODS

    }
}
