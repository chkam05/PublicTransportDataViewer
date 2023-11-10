using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang.Ztm
{
    public class ZtmLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private ArrivalsLangConfig? _arrivals = null;
        private LineDetailsViewLangConfig? _lineDetails = null;
        private LinesViewLangConfig? _lines = null;
        private MessagesLangConfig? _messages = null;
        private TimeTablesLangConfig? _timeTables = null;
        private TransportTypesLangConfig? _transportTypes = null;


        //  GETTERS & SETTERS

        public ArrivalsLangConfig Arrivals
        {
            get
            {
                if (_arrivals == null)
                    _arrivals = new ArrivalsLangConfig();

                return _arrivals;
            }
            set
            {
                _arrivals = value;
                OnPropertyChanged(nameof(Arrivals));
            }
        }

        public LineDetailsViewLangConfig LineDetails
        {
            get
            {
                if (_lineDetails == null)
                    _lineDetails = new LineDetailsViewLangConfig();

                return _lineDetails;
            }
            set
            {
                _lineDetails = value;
                OnPropertyChanged(nameof(LineDetails));
            }
        }

        public LinesViewLangConfig Lines
        {
            get
            {
                if (_lines == null)
                    _lines = new LinesViewLangConfig();

                return _lines;
            }
            set
            {
                _lines = value;
                OnPropertyChanged(nameof(Lines));
            }
        }

        public MessagesLangConfig Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new MessagesLangConfig();

                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        public TimeTablesLangConfig TimeTables
        {
            get
            {
                if (_timeTables == null)
                    _timeTables = new TimeTablesLangConfig();

                return _timeTables;
            }
            set
            {
                _timeTables = value;
                OnPropertyChanged(nameof(TimeTables));
            }
        }

        public TransportTypesLangConfig TransportTypes
        {
            get
            {
                if (_transportTypes == null)
                    _transportTypes = new TransportTypesLangConfig();

                return _transportTypes;
            }
            set
            {
                _transportTypes = value;
                OnPropertyChanged(nameof(TransportTypes));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ZtmLangConfig class constructor. </summary>
        [JsonConstructor]
        public ZtmLangConfig(
            ArrivalsLangConfig? arrivals = null,
            LineDetailsViewLangConfig? lineDetails = null,
            LinesViewLangConfig? lines = null,
            MessagesLangConfig? messages = null,
            TimeTablesLangConfig? timeTables = null,
            TransportTypesLangConfig? transportTypes = null)
        {
            Arrivals = arrivals ?? new ArrivalsLangConfig();
            Lines = lines ?? new LinesViewLangConfig();
            LineDetails = lineDetails ?? new LineDetailsViewLangConfig();
            Messages = messages ?? new MessagesLangConfig();
            TimeTables = timeTables ?? new TimeTablesLangConfig();
            TransportTypes = transportTypes ?? new TransportTypesLangConfig();
        }

        #endregion CLASS METHODS

    }
}
