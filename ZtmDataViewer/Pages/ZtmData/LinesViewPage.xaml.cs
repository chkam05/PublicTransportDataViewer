using chkam05.Tools.ControlsEx.Data;
using chkam05.Tools.ControlsEx.Events;
using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
using System.Xml;
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.TimeTables;
using ZtmDataViewer.Components;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.MainMenu;
using ZtmDataViewer.Data.ZtmData;
using ZtmDataViewer.InternalMessages.ZtmData;
using ZtmDataViewer.Utilities;
using ZtmDataViewer.Windows;

namespace ZtmDataViewer.Pages.ZtmData
{
    public partial class LinesViewPage : BasePage
    {

        //  VARIABLES

        private ObservableCollection<LineGroupViewModel> _lineGroups;
        private bool _loaded = false;


        //  GETTERS & SETTERS

        public override List<MainMenuItem> MainMenuItems
        {
            get => new List<MainMenuItem>()
            {
                new MainMenuItem(ConfigManager.Instance.LangConfig.LinesViewPageMenuItem, PackIconKind.ChartTimelineVariant, OnLinesMenuItemSelect),
                new MainMenuItem(ConfigManager.Instance.LangConfig.StartPageSettingsMenuItem, PackIconKind.Gear, OnSettingsMenuItemSelect),
            };
        }

        public ObservableCollection<LineGroupViewModel> LineGroups
        {
            get => _lineGroups;
            private set
            {
                _lineGroups = value;
                _lineGroups.CollectionChanged += OnLineGroupsCollectionChanged;
                OnPropertyChanged(nameof(LineGroups));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LinesViewPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public LinesViewPage(PagesController pagesController) : base(pagesController)
        {
            //  Initialize user interface.
            InitializeComponent();
        }

        #endregion CLASS METHODS

        #region DATA MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Load lines data. </summary>
        private void LoadLinesData()
        {
            var onDataLoadedEventHandler = new Loader.LinesDataLoadedEventHandler((lineGroupsCollection) =>
            {
                LineGroups = lineGroupsCollection;
            });

            Loader.LoadLinesData(onDataLoadedEventHandler);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load line details data and open line details page. </summary>
        /// <param name="line"> Line data. </param>
        /// <param name="timeTable"> Time table data. </param>
        private void LoadLineDetails(Line line)
        {
            Loader.LoadLineDetailsData(line, _pagesController);
        }

        #endregion DATA MANAGEMETN METHODS

        #region HEADER INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking refresh button. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void RefreshButtonEx_Click(object sender, RoutedEventArgs e)
        {
            LoadLinesData();
        }

        #endregion HEADER INTERACTION METHODS

        #region LINES DATA INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after double clicking on lines list view ex. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Mouse Button Event Arguments. </param>
        private void LinesListViewEx_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement source)
            {
                if (source.DataContext is LineViewModel lineViewModel)
                {
                    LoadLineDetails(lineViewModel.Line);
                }
            }
        }

        #endregion LINES DATA INTERACTION METHODS

        #region MENU ITEMS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after selecting lines menu item. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Event Arguments. </param>
        private void OnLinesMenuItemSelect(object? sender, EventArgs e)
        {
            _pagesController?.LoadPage(new ZtmData.LinesViewPage(_pagesController));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after selecting settings menu item. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Event Arguments. </param>
        private void OnSettingsMenuItemSelect(object? sender, EventArgs e)
        {
            _pagesController?.LoadPage(new SettingsPage(_pagesController));
        }

        #endregion MENU ITEMS METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after line groups collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnLineGroupsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(LineGroups));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region PAGE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after loading page. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_loaded)
            {
                LoadLinesData();
                _loaded = true;
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after unloading page. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BasePage_Unloaded(object sender, RoutedEventArgs e)
        {
            //
        }

        #endregion PAGE METHODS

    }
}
