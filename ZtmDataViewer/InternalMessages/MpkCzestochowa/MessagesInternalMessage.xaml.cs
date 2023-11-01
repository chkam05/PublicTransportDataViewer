using chkam05.Tools.ControlsEx.Data;
using chkam05.Tools.ControlsEx.InternalMessages;
using chkam05.Tools.ControlsEx;
using System;
using System.Collections.Generic;
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
using ZtmDataViewer.Data.Config;
using System.Collections.ObjectModel;
using ZtmDataViewer.Data.MpkCzestochowa;
using System.Collections.Specialized;

namespace ZtmDataViewer.InternalMessages.MpkCzestochowa
{
    public partial class MessagesInternalMessage : StandardInternalMessageEx
    {

        //  VARIABLES

        private ObservableCollection<MessageViewModel> _messages;


        //  GETTERS & SETTERS

        public ObservableCollection<MessageViewModel> Messages
        {
            get => _messages;
            private set
            {
                _messages = value;
                _messages.CollectionChanged += OnMessagesCollectionChanged;
                OnPropertyChanged(nameof(Messages));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MessagesInternalMessage class constructor. </summary>
        /// <param name="parentContainer"> Parent InternalMessagesEx container. </param>
        /// <param name="messagesViewModelCollection"> Messages view model collection. </param>
        public MessagesInternalMessage(InternalMessagesExContainer parentContainer, ObservableCollection<MessageViewModel> messagesViewModelCollection)
            : base(parentContainer)
        {
            //  Data configuration.
            Messages = messagesViewModelCollection;

            //  Initialize user interface.
            InitializeComponent();

            //  Interface configuration.
            Buttons = new InternalMessageButtons[]
            {
                InternalMessageButtons.CancelButton
            };
        }

        #endregion CLASS METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after messages collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnMessagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Messages));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

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
                cancelButton.Content = langConfig.ButtonClose;
        }

        #endregion TEMPLATE METHODS

    }
}
