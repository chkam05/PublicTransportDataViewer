using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class MessagesViewLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _date = string.Empty;
        private string _lines = string.Empty;
        private string _title = string.Empty;


        //  GETTERS & SETTERS

        public string Date
        {
            get => _date;
            set => SetStringProperty(ref _date, nameof(Date), value);
        }

        public string Lines
        {
            get => _lines;
            set => SetStringProperty(ref _lines, nameof(Lines), value);
        }

        public string Title
        {
            get => _title;
            set => SetStringProperty(ref _title, nameof(Title), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MessagesViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public MessagesViewLangConfig(
            string? date = null,
            string? lines = null,
            string? title = null)
        {
            Date = SetLanguageValue(date, "Data obowiązywania: ");
            Lines = SetLanguageValue(lines, "Dotyczy linii: ");
            Title = SetLanguageValue(title, "Komunikaty: ");
        }

        #endregion CLASS METHODS

    }
}
