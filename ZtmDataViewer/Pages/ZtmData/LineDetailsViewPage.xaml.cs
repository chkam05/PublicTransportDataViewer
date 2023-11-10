using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Data.Lines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.TimeTables;
using PublicTransportDataViewer.Components;
using PublicTransportDataViewer.Converters.ZtmData;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.Data.MainMenu;
using PublicTransportDataViewer.Data.ZtmData;
using PublicTransportDataViewer.InternalMessages.ZtmData;
using PublicTransportDataViewer.Utilities;
using PublicTransportDataViewer.Windows;
using static System.Windows.Forms.LinkLabel;

namespace PublicTransportDataViewer.Pages.ZtmData
{
    public partial class LineDetailsViewPage : BasePage
    {

        //  VARIABLES

        private LineDetailsViewModel _lineDetailsViewModel;
        private LineDirectionViewModel _selectedLineDirectionViewModel;
        private LineStopViewModel _selectedLineStopViewModel;
        private ObservableCollection<LineDepartureGroupViewModel> _departures;
        private string _sourceUrl;


        //  GETTERS & SETTERS

        public override List<MainMenuItem> MainMenuItems
        {
            get => new List<MainMenuItem>()
            {
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemLines, PackIconKind.ChartTimelineVariant, OnLinesMenuItemSelect),
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemSettings, PackIconKind.Gear, OnSettingsMenuItemSelect),
            };
        }

        public LineDetailsViewModel LineDetailsViewModel
        {
            get => _lineDetailsViewModel;
            private set
            {
                _lineDetailsViewModel = value;
                OnPropertyChanged(nameof(LineDetailsViewModel));
            }
        }

        public LineDirectionViewModel SelectedLineDirectionViewModel
        {
            get => _selectedLineDirectionViewModel;
            set
            {
                _selectedLineDirectionViewModel = value;
                OnPropertyChanged(nameof(SelectedLineDirectionViewModel));
            }
        }

        public LineStopViewModel SelectedLineStopViewModel
        {
            get => _selectedLineStopViewModel;
            private set
            {
                _selectedLineStopViewModel = value;
                OnPropertyChanged(nameof(SelectedLineStopViewModel));
            }
        }

        public ObservableCollection<LineDepartureGroupViewModel> Departures
        {
            get => _departures;
            private set
            {
                _departures = value;
                _departures.CollectionChanged += OnDeparturesCollectionChanged;
                OnPropertyChanged(nameof(Departures));
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
        /// <summary> LineDetailsViewPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        /// <param name="lineDetailsViewModel"> Line details view model. </param>
        /// <param name="sourceUrl"> Source url. </param>
        public LineDetailsViewPage(PagesController pagesController, LineDetailsViewModel lineDetailsViewModel, string sourceUrl) : base(pagesController)
        {
            //  Initialize data.
            LineDetailsViewModel = lineDetailsViewModel;
            SourceUrl = sourceUrl;
            SelectedLineDirectionViewModel = lineDetailsViewModel.Directions.FirstOrDefault();

            //  Initialize user interface.
            InitializeComponent();

            //  Set additional values.
            UpdateIconKind();
        }

        #endregion CLASS METHODS

        #region DATA MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Load line details data. </summary>
        private void LoadLineDetails()
        {
            var onDataLoadedEventHandler = new Loader.LineDetailsDataLoadedEventHandler((lineDetailsViewModel, sourceUrl) =>
            {
                LineDetailsViewModel = lineDetailsViewModel;
                SourceUrl = sourceUrl ?? string.Empty;

                if (lineDetailsViewModel.Directions.Any())
                    SelectedLineDirectionViewModel = LineDetailsViewModel.Directions.First();
            });

            Loader.LoadLineDetailsData(LineDetailsViewModel.Line, _pagesController,
                onDataLoadedEventHandler, LineDetailsViewModel.TimeTableId, true);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load departures data. </summary>
        private void LoadDepartures()
        {
            var onDataLoadedEventHandler = new Loader.LineDearpturesDataLoadedEventHandler(
                (lineDepartureGroupViewModelCollection, sourceUrl) =>
            {
                Departures = lineDepartureGroupViewModelCollection;
                SourceUrl = sourceUrl ?? string.Empty;
            });

            Loader.LoadLineDepartures(
                LineDetailsViewModel.Line,
                SelectedLineStopViewModel.LineStop,
                onDataLoadedEventHandler);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load arrivals data. </summary>
        /// <param name="lineDepartureViewModel"> Line departure view model. </param>
        private void LoadArrivals(LineDepartureViewModel lineDepartureViewModel)
        {
            Loader.LoadLineArrivals(LineDetailsViewModel.Line, lineDepartureViewModel.Departure);
        }

        #endregion DATA MANAGEMENT METHODS

        #region DATA INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after double clicing on line stops list view ex item. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Mouse Button Event Arguments. </param>
        private void LineStopsListViewEx_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement source)
            {
                if (source.DataContext is LineStopViewModel lineStopViewModel)
                {
                    SelectedLineStopViewModel = lineStopViewModel;
                    LoadDepartures();
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after double clicing on departures list view ex item. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Mouse Button Event Arguments. </param>
        private void DeparturesListViewEx_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement source)
            {
                if (source.DataContext is LineDepartureViewModel lineDepartureViewModel)
                {
                    LoadArrivals(lineDepartureViewModel);
                }
            }
        }

        #endregion DATA INTERACTION METHODS

        #region HEADER INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking refresh button. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void RefresButtonEx_Click(object sender, RoutedEventArgs e)
        {
            LoadLineDetails();
            LoadDepartures();
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

            var imMessages = new MessagesInternalMessage(imContainer, LineDetailsViewModel);

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
        /// <summary> Method invoked after departures collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnDeparturesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Departures));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region PAGE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after loading page. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            //
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

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Update page icon. </summary>
        private void UpdateIconKind()
        {
            var converter = new TransportTypeIconKindConverter();
            var cultureInfo = CultureInfo.InvariantCulture;

            IconKind = (PackIconKind)converter.Convert(
                _lineDetailsViewModel.TransportType, typeof(PackIconKind), null, cultureInfo);
        }

        #endregion UTILITY METHODS

    }
}
