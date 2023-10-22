﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class MpkCzestochowaLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private MpkCzestochowaLinesViewLangConfig? _mpkCzestochowaLinesViewLangConfig = null;
        private MpkCzestochowaTransportTypesLangConfig? _mpkCzestochowaTransportTypesLangConfig = null;


        //  GETTERS & SETTERS

        public MpkCzestochowaLinesViewLangConfig MpkCzestochowaLinesView
        {
            get
            {
                if (_mpkCzestochowaLinesViewLangConfig == null)
                {
                    _mpkCzestochowaLinesViewLangConfig = new MpkCzestochowaLinesViewLangConfig();
                    OnPropertyChanged(nameof(MpkCzestochowaLinesView));
                }
                return _mpkCzestochowaLinesViewLangConfig;
            }
            set
            {
                _mpkCzestochowaLinesViewLangConfig = value;
                OnPropertyChanged(nameof(MpkCzestochowaLinesView));
            }
        }

        public MpkCzestochowaTransportTypesLangConfig MpkCzestochowaTransportTypes
        {
            get
            {
                if (_mpkCzestochowaTransportTypesLangConfig == null)
                {
                    _mpkCzestochowaTransportTypesLangConfig = new MpkCzestochowaTransportTypesLangConfig();
                    OnPropertyChanged(nameof(MpkCzestochowaTransportTypes));
                }
                return _mpkCzestochowaTransportTypesLangConfig;
            }
            set
            {
                _mpkCzestochowaTransportTypesLangConfig = value;
                OnPropertyChanged(nameof(MpkCzestochowaTransportTypes));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MpkCzestochowaLangConfig class constructor. </summary>
        [JsonConstructor]
        public MpkCzestochowaLangConfig(
            MpkCzestochowaLinesViewLangConfig? mpkCzestochowaLinesView = null,
            MpkCzestochowaTransportTypesLangConfig? mpkCzestochowaTransportTypes = null)
        {
            MpkCzestochowaLinesView = mpkCzestochowaLinesView ?? new MpkCzestochowaLinesViewLangConfig();
            MpkCzestochowaTransportTypes = mpkCzestochowaTransportTypes ?? new MpkCzestochowaTransportTypesLangConfig();
        }

        #endregion CLASS METHODS

    }
}