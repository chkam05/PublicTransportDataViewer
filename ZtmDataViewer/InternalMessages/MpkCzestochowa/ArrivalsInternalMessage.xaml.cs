using chkam05.Tools.ControlsEx.Data;
using chkam05.Tools.ControlsEx;
using chkam05.Tools.ControlsEx.InternalMessages;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.MpkCzestochowa;

namespace ZtmDataViewer.InternalMessages.MpkCzestochowa
{
    public partial class ArrivalsInternalMessage : StandardInternalMessageEx
    {

        //  VARIABLES

        private LineArrivalsViewModel _lineArrivalsViewModel;


        //  GETTERS & SETTERS

        public LineArrivalsViewModel LineArrivalsViewModel
        {
            get => _lineArrivalsViewModel;
            set
            {
                _lineArrivalsViewModel = value;
                OnPropertyChanged(nameof(LineArrivalsViewModel));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalsInternalMessage class constructor. </summary>
        /// <param name="parentContainer"> Parent InternalMessagesEx container. </param>
        /// <param name="lineArrivalsViewModel"> Line arrivals view model. </param>
        public ArrivalsInternalMessage(InternalMessagesExContainer parentContainer, LineArrivalsViewModel lineArrivalsViewModel)
            : base(parentContainer)
        {
            //  Data configuration.
            LineArrivalsViewModel = lineArrivalsViewModel;

            //  Initialize user interface.
            InitializeComponent();

            //  Interface configuration.
            Buttons = new InternalMessageButtons[]
            {
                InternalMessageButtons.CancelButton
            };
        }

        #endregion CLASS METHODS

        #region TEMPLATE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> When overridden in a derived class,cis invoked whenever 
        /// application code or internal processes call ApplyTemplate. </summary>
        public override void OnApplyTemplate()
        {
            var langConfig = ConfigManager.Instance.LangConfig;

            base.OnApplyTemplate();
            ButtonEx cancelButton = GetButtonEx("cancelButton");

            if (cancelButton != null)
                cancelButton.Content = langConfig.CancelButton;
        }

        #endregion TEMPLATE METHODS

    }
}
