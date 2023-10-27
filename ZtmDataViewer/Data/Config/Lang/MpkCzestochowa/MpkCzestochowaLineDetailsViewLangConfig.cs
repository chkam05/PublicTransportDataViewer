using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class MpkCzestochowaLineDetailsViewLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _mpkCzestochowaLineDetailsViewPageOtherLines = string.Empty;


        //  GETTERS & SETTERS

        public string MpkCzestochowaLineDetailsViewPageOtherLines
        {
            get => _mpkCzestochowaLineDetailsViewPageOtherLines;
            set => SetStringProperty(ref _mpkCzestochowaLineDetailsViewPageOtherLines, nameof(MpkCzestochowaLineDetailsViewPageOtherLines), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MpkCzestochowaLineDetailsViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public MpkCzestochowaLineDetailsViewLangConfig(
            string? mpkCzestochowaLineDetailsViewPageOtherLines = null)
        {
            MpkCzestochowaLineDetailsViewPageOtherLines = SetLanguageValue(mpkCzestochowaLineDetailsViewPageOtherLines, "Inne linie odjeżdżające z tego przystanku: ");
        }

        #endregion CLASS METHODS

    }
}
