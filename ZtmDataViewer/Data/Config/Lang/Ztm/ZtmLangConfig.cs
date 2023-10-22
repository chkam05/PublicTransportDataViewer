using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private ZtmArrivalsIMLangConfig? _ztmArrivalsIMLangConfig = null;
        private ZtmLinesViewLangConfig? _ztmLinesViewLangConfig = null;
        private ZtmLineDetailsViewLangConfig? _ztmLineDetailsViewLangConfig = null;
        private ZtmTimeTableSelectorIMLangConfig? _ztmTimeTableSelectorIMLangConfig = null;
        private ZtmTransportTypesLangConfig? _ztmTransportTypesLangConfig = null;


        //  GETTERS & SETTERS

        public ZtmArrivalsIMLangConfig ZtmArrivalsIM
        {
            get
            {
                if (_ztmArrivalsIMLangConfig == null)
                {
                    _ztmArrivalsIMLangConfig = new ZtmArrivalsIMLangConfig();
                    OnPropertyChanged(nameof(ZtmArrivalsIM));
                }
                return _ztmArrivalsIMLangConfig;
            }
            set
            {
                _ztmArrivalsIMLangConfig = value;
                OnPropertyChanged(nameof(ZtmArrivalsIM));
            }
        }

        public ZtmLinesViewLangConfig ZtmLinesView
        {
            get
            {
                if (_ztmLinesViewLangConfig == null)
                {
                    _ztmLinesViewLangConfig = new ZtmLinesViewLangConfig();
                    OnPropertyChanged(nameof(ZtmLinesView));
                }
                return _ztmLinesViewLangConfig;
            }
            set
            {
                _ztmLinesViewLangConfig = value;
                OnPropertyChanged(nameof(ZtmLinesView));
            }
        }

        public ZtmLineDetailsViewLangConfig ZtmLineDetailsView
        {
            get
            {
                if (_ztmLineDetailsViewLangConfig == null)
                {
                    _ztmLineDetailsViewLangConfig = new ZtmLineDetailsViewLangConfig();
                    OnPropertyChanged(nameof(ZtmLineDetailsView));
                }
                return _ztmLineDetailsViewLangConfig;
            }
            set
            {
                _ztmLineDetailsViewLangConfig = value;
                OnPropertyChanged(nameof(ZtmLineDetailsView));
            }
        }

        public ZtmTimeTableSelectorIMLangConfig ZtmTimeTableSelectorIM
        {
            get
            {
                if (_ztmTimeTableSelectorIMLangConfig == null)
                {
                    _ztmTimeTableSelectorIMLangConfig = new ZtmTimeTableSelectorIMLangConfig();
                    OnPropertyChanged(nameof(ZtmTimeTableSelectorIM));
                }
                return _ztmTimeTableSelectorIMLangConfig;
            }
            set
            {
                _ztmTimeTableSelectorIMLangConfig = value;
                OnPropertyChanged(nameof(ZtmTimeTableSelectorIM));
            }
        }

        public ZtmTransportTypesLangConfig ZtmTransportTypes
        {
            get
            {
                if (_ztmTransportTypesLangConfig == null)
                {
                    _ztmTransportTypesLangConfig = new ZtmTransportTypesLangConfig();
                    OnPropertyChanged(nameof(ZtmTransportTypes));
                }
                return _ztmTransportTypesLangConfig;
            }
            set
            {
                _ztmTransportTypesLangConfig = value;
                OnPropertyChanged(nameof(ZtmTransportTypes));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmLangConfig(
            ZtmArrivalsIMLangConfig? ztmArrivalsIM = null,
            ZtmLinesViewLangConfig? ztmLinesView = null,
            ZtmLineDetailsViewLangConfig? ztmLineDetailsView = null,
            ZtmTimeTableSelectorIMLangConfig? ztmTimeTableSelectorIM = null,
            ZtmTransportTypesLangConfig? ztmTransportTypes = null)
        {
            ZtmArrivalsIM = ztmArrivalsIM ?? new ZtmArrivalsIMLangConfig();
            ZtmLinesView = ztmLinesView ?? new ZtmLinesViewLangConfig();
            ZtmLineDetailsView = ztmLineDetailsView ?? new ZtmLineDetailsViewLangConfig();
            ZtmTimeTableSelectorIM = ztmTimeTableSelectorIM ?? new ZtmTimeTableSelectorIMLangConfig();
            ZtmTransportTypes = ztmTransportTypes ?? new ZtmTransportTypesLangConfig();
        }

        #endregion CLASS METHODS

    }
}
