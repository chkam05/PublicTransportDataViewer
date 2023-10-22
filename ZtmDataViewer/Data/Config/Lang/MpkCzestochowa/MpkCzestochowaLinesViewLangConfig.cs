using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class MpkCzestochowaLinesViewLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private string _mpkCzestochowaMessageDateTitle = string.Empty;
        private string _mpkCzestochowaMessageLinesTitle = string.Empty;


        //  GETTERS & SETTERS

        public string MpkCzestochowaMessageDateTitle
        {
            get => _mpkCzestochowaMessageDateTitle;
            set => SetStringProperty(ref _mpkCzestochowaMessageDateTitle, nameof(MpkCzestochowaMessageDateTitle), value);
        }

        public string MpkCzestochowaMessageLinesTitle
        {
            get => _mpkCzestochowaMessageLinesTitle;
            set => SetStringProperty(ref _mpkCzestochowaMessageLinesTitle, nameof(MpkCzestochowaMessageLinesTitle), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MpkCzestochowaLinesViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public MpkCzestochowaLinesViewLangConfig(
            string? mpkCzestochowaMessageDateTitle = null,
            string? mpkCzestochowaMessageLinesTitle = null)
        {
            MpkCzestochowaMessageDateTitle = SetLanguageValue(mpkCzestochowaMessageDateTitle, "Data obowiązywania: ");
            MpkCzestochowaMessageLinesTitle = SetLanguageValue(mpkCzestochowaMessageLinesTitle, "Dotyczy linii: ");
        }

        #endregion CLASS METHODS

    }
}
