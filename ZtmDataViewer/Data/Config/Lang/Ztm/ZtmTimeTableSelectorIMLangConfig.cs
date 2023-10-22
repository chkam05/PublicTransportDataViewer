using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmTimeTableSelectorIMLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmTimeTablesSelectorFromToTitle = string.Empty;
        private string _ztmTimeTablesSelectorFrom = string.Empty;
        private string _ztmTimeTablesSelectorTo = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmTimeTablesSelectorFromToTitle
        {
            get => _ztmTimeTablesSelectorFromToTitle;
            set => SetStringProperty(ref _ztmTimeTablesSelectorFromToTitle, nameof(ZtmTimeTablesSelectorFromToTitle), value);
        }

        public string ZtmTimeTablesSelectorFrom
        {
            get => _ztmTimeTablesSelectorFrom;
            set => SetStringProperty(ref _ztmTimeTablesSelectorFrom, nameof(ZtmTimeTablesSelectorFrom), value);
        }

        public string ZtmTimeTablesSelectorTo
        {
            get => _ztmTimeTablesSelectorTo;
            set => SetStringProperty(ref _ztmTimeTablesSelectorTo, nameof(ZtmTimeTablesSelectorTo), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmTimeTableSelectorIMLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmTimeTableSelectorIMLangConfig(
            string? ztmTimeTablesSelectorFromToTitle = null,
            string? ztmTimeTablesSelectorFrom = null,
            string? ztmTimeTablesSelectorTo = null)
        {
            ZtmTimeTablesSelectorFromToTitle = SetLanguageValue(ztmTimeTablesSelectorFromToTitle, "Rozkład ważny");
            ZtmTimeTablesSelectorFrom = SetLanguageValue(ztmTimeTablesSelectorFrom, "od");
            ZtmTimeTablesSelectorTo = SetLanguageValue(ztmTimeTablesSelectorTo, "do");
        }

        #endregion CLASS METHODS

    }
}
