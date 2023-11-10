using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang.Ztm
{
    public class TimeTablesLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _fromTo = string.Empty;
        private string _from = string.Empty;
        private string _to = string.Empty;


        //  GETTERS & SETTERS

        public string FromTo
        {
            get => _fromTo;
            set => SetStringProperty(ref _fromTo, nameof(FromTo), value);
        }

        public string From
        {
            get => _from;
            set => SetStringProperty(ref _from, nameof(From), value);
        }

        public string To
        {
            get => _to;
            set => SetStringProperty(ref _to, nameof(To), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TimeTablesLangConfig class constructor. </summary>
        [JsonConstructor]
        public TimeTablesLangConfig(
            string? fromTo = null,
            string? from = null,
            string? to = null)
        {
            FromTo = SetLanguageValue(fromTo, "Rozkład ważny");
            From = SetLanguageValue(from, "od");
            To = SetLanguageValue(to, "do");
        }

        #endregion CLASS METHODS

    }
}
