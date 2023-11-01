using chkam05.Tools.ControlsEx;
using chkam05.Tools.ControlsEx.Data;
using chkam05.Tools.ControlsEx.InternalMessages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
using ZtmDataDownloader.Data.Lines;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.ZtmData;
using ZtmDataViewer.Utilities;

namespace ZtmDataViewer.InternalMessages.ZtmData
{
    public partial class TimeTableSelectorInternalMessage : StandardInternalMessageEx
    {

        //  VARIABLES

        private Button _okButton;
        private Line _line;
        private TimeTableViewModel _selectedTimeTable;
        private ObservableCollection<TimeTableViewModel> _timeTables;


        //  GETTERS & SETTERS

        public Line Line
        {
            get => _line;
            set
            {
                _line = value;
                OnPropertyChanged(nameof(Line));
            }
        }

        public TimeTableViewModel SelectedTimeTable
        {
            get => _selectedTimeTable;
            set
            {
                _selectedTimeTable = value;
                OnPropertyChanged(nameof(SelectedTimeTable));
            }
        }

        public ObservableCollection<TimeTableViewModel> TimeTables
        {
            get => _timeTables;
            set
            {
                _timeTables = value;
                _timeTables.CollectionChanged += OnTimeTablesCollectionChanged;
                OnPropertyChanged(nameof(TimeTables));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> TimeTableSelectorInternalMessage class constructor. </summary>
        /// <param name="parentContainer"> Parent InternalMessagesEx container. </param>
        /// <param name="line"> Line data. </param>
        /// <param name="timeTables"> Time tables data. </param>
        public TimeTableSelectorInternalMessage(InternalMessagesExContainer parentContainer, Line line, List<TimeTableViewModel> timeTables)
            : base(parentContainer)
        {
            //  Data configuration.
            Line = line;
            TimeTables = new ObservableCollection<TimeTableViewModel>(
                (timeTables?.Any() ?? false) ? timeTables : new List<TimeTableViewModel>());

            //  Initialize user interface.
            InitializeComponent();

            //  Interface configuration.
            Buttons = new InternalMessageButtons[]
            {
                InternalMessageButtons.OkButton,
                InternalMessageButtons.CancelButton
            };
        }

        #endregion CLASS METHODS

        #region DATA INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after selecting time table in time tables list view ex. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Selection Changed Event Arguments. </param>
        private void TimeTablesListViewEx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListViewEx listViewEx)
            {
                bool selection = listViewEx.SelectedItem is TimeTableViewModel timeTableViewModel;

                if (_okButton != null)
                    _okButton.IsEnabled = selection;
            }
        }

        #endregion DATA INTERACTION METHODS

        #region INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking ok button. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        protected override void OnOkClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTimeTable == null)
                return;

            base.OnOkClick(sender, e);
        }

        #endregion INTERACTION METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after time tables collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnTimeTablesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TimeTables));
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
            ButtonEx okButton = GetButtonEx("okButton");
            ButtonEx cancelButton = GetButtonEx("cancelButton");

            if (okButton != null)
            {
                okButton.Content = langConfig.ButtonSelect;
                okButton.IsEnabled = false;
                _okButton = okButton;
            }

            if (cancelButton != null)
                cancelButton.Content = langConfig.ButtonCancel;
        }

        #endregion TEMPLATE METHODS

    }
}
