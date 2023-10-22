﻿using Newtonsoft.Json;
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

        private string _mpkCzestochowaLinesViewPageMessages = string.Empty;
        private string _mpkCzestochowaLinesViewPageMsgDateTitle = string.Empty;
        private string _mpkCzestochowaLinesViewPageMsgLinesTitle = string.Empty;


        //  GETTERS & SETTERS

        public string MpkCzestochowaLinesViewPageMessages
        {
            get => _mpkCzestochowaLinesViewPageMessages;
            set => SetStringProperty(ref _mpkCzestochowaLinesViewPageMessages, nameof(MpkCzestochowaLinesViewPageMessages), value);
        }

        public string MpkCzestochowaLinesViewPageMsgDateTitle
        {
            get => _mpkCzestochowaLinesViewPageMsgDateTitle;
            set => SetStringProperty(ref _mpkCzestochowaLinesViewPageMsgDateTitle, nameof(MpkCzestochowaLinesViewPageMsgDateTitle), value);
        }

        public string MpkCzestochowaLinesViewPageMsgLinesTitle
        {
            get => _mpkCzestochowaLinesViewPageMsgLinesTitle;
            set => SetStringProperty(ref _mpkCzestochowaLinesViewPageMsgLinesTitle, nameof(MpkCzestochowaLinesViewPageMsgLinesTitle), value);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MpkCzestochowaLinesViewLangConfig class constructor. </summary>
        [JsonConstructor]
        public MpkCzestochowaLinesViewLangConfig(
            string? mpkCzestochowaLinesViewPageMessages = null,
            string? mpkCzestochowaLinesViewPageMsgDateTitle = null,
            string? mpkCzestochowaLinesViewPageMsgLinesTitle = null)
        {
            MpkCzestochowaLinesViewPageMessages = SetLanguageValue(mpkCzestochowaLinesViewPageMessages, "Komunikaty: ");
            MpkCzestochowaLinesViewPageMsgDateTitle = SetLanguageValue(mpkCzestochowaLinesViewPageMsgDateTitle, "Data obowiązywania: ");
            MpkCzestochowaLinesViewPageMsgLinesTitle = SetLanguageValue(mpkCzestochowaLinesViewPageMsgLinesTitle, "Dotyczy linii: ");
        }

        #endregion CLASS METHODS

    }
}
