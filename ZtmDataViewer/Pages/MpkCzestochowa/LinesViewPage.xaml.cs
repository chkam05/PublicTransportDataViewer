﻿using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Data.Lines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.TimeTables;
using PublicTransportDataViewer.Components;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.Data.MainMenu;
using PublicTransportDataViewer.Data.MpkCzestochowa;
using PublicTransportDataViewer.InternalMessages.MpkCzestochowa;
using PublicTransportDataViewer.Utilities;
using PublicTransportDataViewer.Windows;

namespace PublicTransportDataViewer.Pages.MpkCzestochowa
{
    public partial class LinesViewPage : BasePage
    {

        //  VARIABLES

        private ObservableCollection<LineGroupViewModel> _lineGroups;
        private ObservableCollection<MessageViewModel> _messages;
        private string _sourceUrl = string.Empty;
        private bool _loaded = false;


        //  GETTERS & SETTERS

        public override List<MainMenuItem> MainMenuItems
        {
            get => new List<MainMenuItem>()
            {
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemLines, PackIconKind.ChartTimelineVariant, OnLinesMenuItemSelect),
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemSettings, PackIconKind.Gear, OnSettingsMenuItemSelect),
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

        public string SourceUrl
        {
            get => _sourceUrl;
            set
            {
                _sourceUrl = value;
                OnPropertyChanged(nameof(SourceUrl));
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
            var onDataLoadedEventHandler = new Loader.LinesDataLoadedEventHandler((lineGroupCollection, messagesCollection, sourceUrl) =>
            {
                LineGroups = lineGroupCollection;
                Messages = messagesCollection;
                SourceUrl = sourceUrl ?? string.Empty;
            });

            Loader.LoadLinesData(onDataLoadedEventHandler);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load line details data and open line details page. </summary>
        /// <param name="line"> Line data. </param>
        /// <param name="pagesController"> Pages controller. </param>
        private void LoadLineDetails(Line line, PagesController pagesController)
        {
            var onDataLoadedEventHandler = new Loader.LineDetailsDataLoadedEventHandler((lineDetailsViewModel, line, isDataReload, sourceUrl) =>
            {
                pagesController?.LoadPage(
                    new LineDetailsViewPage(pagesController, line, lineDetailsViewModel, sourceUrl ?? string.Empty));
            });

            Loader.LoadLineDetails(line, onDataLoadedEventHandler);
        }

        #endregion DATA MANAGEMENT METHODS

        #region HEADER INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking refresh button. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void RefreshButtonEx_Click(object sender, RoutedEventArgs e)
        {
            LoadLinesData();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking refresh button. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void MessagesButtonEx_Click(object sender, RoutedEventArgs e)
        {
            //  Get basic data.
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;

            var imMessages = new MessagesInternalMessage(imContainer, Messages);

            imContainer.ShowMessage(imMessages);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking source text block. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void SourceTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2 && !string.IsNullOrEmpty(SourceUrl))
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = SourceUrl,
                    UseShellExecute = true
                };

                Process.Start(processStartInfo);
            }
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
                    LoadLineDetails(lineViewModel.Line, _pagesController);
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
            _pagesController?.LoadPage(new MpkCzestochowa.LinesViewPage(_pagesController));
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

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after messages collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnMessagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Messages));
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
