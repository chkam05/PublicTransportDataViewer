using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class LineDetailsViewLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _otherLines = string.Empty;


        //  GETTERS & SETTERS

        public string OtherLines
        {
            get => _otherLines;
            set => SetStringProperty(ref _otherLines, nameof(OtherLines), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public LineDetailsViewLangConfig(
            string? otherLines = null)
        {
            OtherLines = SetLanguageValue(otherLines, "Inne linie odjeżdżające z tego przystanku: ");
        }

        #endregion CLASS METHODS

    }
}
