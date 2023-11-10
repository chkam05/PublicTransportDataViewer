using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data.Config.Lang.MpkCzestochowa
{
    public class MpkCzestochowaLangConfig : BaseLangConfig
    {

        //  VARIABLES

        private ArrivalsViewLangConfig? _arrivals = null;
        private LineDetailsViewLangConfig? _lineDetails = null;
        private LinesViewLangConfig? _lines = null;
        private MessagesViewLangConfig? _messages = null;
        private TransportTypesLangConfig? _transportTypes = null;


        //  GETTERS & SETTERS

        public ArrivalsViewLangConfig Arrivals
        {
            get
            {
                if (_arrivals == null)
                    _arrivals = new ArrivalsViewLangConfig();

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

        public MessagesViewLangConfig Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new MessagesViewLangConfig();

                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
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
        /// <summary> MpkCzestochowaLangConfig class constructor. </summary>
        [JsonConstructor]
        public MpkCzestochowaLangConfig(
            LineDetailsViewLangConfig? mpkCzestochowaLineDetailsViewLangConfig = null,
            LinesViewLangConfig? mpkCzestochowaLinesView = null,
            MessagesViewLangConfig? mpkCzestochowaMessagesIM = null,
            TransportTypesLangConfig? mpkCzestochowaTransportTypes = null)
        {
            LineDetails = mpkCzestochowaLineDetailsViewLangConfig ?? new LineDetailsViewLangConfig();
            Lines = mpkCzestochowaLinesView ?? new LinesViewLangConfig();
            Messages = mpkCzestochowaMessagesIM ?? new MessagesViewLangConfig();
            TransportTypes = mpkCzestochowaTransportTypes ?? new TransportTypesLangConfig();
        }

        #endregion CLASS METHODS

    }
}
