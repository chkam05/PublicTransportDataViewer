using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmLineDetailsViewLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _ztmLineDetailsViewPageMessages = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmLineDetailsViewPageMessages
        {
            get => _ztmLineDetailsViewPageMessages;
            set => SetStringProperty(ref _ztmLineDetailsViewPageMessages, nameof(ZtmLineDetailsViewPageMessages), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmLineDetailsViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmLineDetailsViewLangConfig(
            string? ztmLineDetailsViewPageMessages = null)
        {
            ZtmLineDetailsViewPageMessages = SetLanguageValue(ztmLineDetailsViewPageMessages, "Komunikaty: ");
        }

        #endregion CLASS METHODS

    }
}
