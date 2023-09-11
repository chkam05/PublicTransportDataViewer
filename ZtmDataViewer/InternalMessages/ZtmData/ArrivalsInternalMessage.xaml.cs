using chkam05.Tools.ControlsEx;
using chkam05.Tools.ControlsEx.Data;
using chkam05.Tools.ControlsEx.InternalMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZtmDataDownloader.Data.TimeTables;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.ZtmData;

namespace ZtmDataViewer.InternalMessages.ZtmData
{
    public partial class ArrivalsInternalMessage : StandardInternalMessageEx
    {

        //  VARIABLES

        private DepartureDetailsViewModel _departureDetailsViewModel;


        //  GETTERS & SETTERS

        public DepartureDetailsViewModel DepartureDetailsViewModel
        {
            get => _departureDetailsViewModel;
            set
            {
                _departureDetailsViewModel = value;
                OnPropertyChanged(nameof(DepartureDetailsViewModel));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalsInternalMessage class constructor. </summary>
        /// <param name="parentContainer"> Parent InternalMessagesEx container. </param>
        /// <param name="timeTables"> Time tables list. </param>
        public ArrivalsInternalMessage(InternalMessagesExContainer parentContainer, DepartureDetailsViewModel departureDetailsViewModel)
            : base(parentContainer)
        {
            //  Data configuration.
            _departureDetailsViewModel = departureDetailsViewModel;

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
                cancelButton.Content = langConfig.ZtmArrivalsCancelButton;
        }

        #endregion TEMPLATE METHODS

    }
}
