using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
using ZtmDataViewer.Components;
using ZtmDataViewer.Converters.ZtmData;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.ZtmData;
using ZtmDataViewer.InternalMessages.ZtmData;
using ZtmDataViewer.Utilities;
using ZtmDataViewer.Windows;
using static System.Windows.Forms.LinkLabel;

namespace ZtmDataViewer.Pages.ZtmData
{
    public partial class LineDetailsViewPage : BasePage
    {

        //  VARIABLES

        private LineDetailsViewModel _lineDetailsViewModel;
        private LineDirectionViewModel _selectedLineDirectionViewModel;
        private LineStopViewModel _selectedLineStopViewModel;
        private ObservableCollection<LineDepartureGroupViewModel> _departures;


        //  GETTERS & SETTERS

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


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsViewPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public LineDetailsViewPage(PagesController pagesController, LineDetailsViewModel lineDetailsViewModel) : base(pagesController)
        {
            //  Initialize data.
            LineDetailsViewModel = lineDetailsViewModel;
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
            //  Get basic data.
            var langConf = ConfigManager.Instance.LangConfig;
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;
            var bgLoader = new BackgroundWorker();

            //  Create await internal message.
            var imAwait = new AwaitInternalMessageEx(imContainer,
                langConf.Messages.DownloadTitle,
                langConf.Messages.LineDetailsViewPageDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                var lineDetails = ZtmDataDownloader.SimpleDownloader.DownloadLineDetails(
                    LineDetailsViewModel.Line, LineDetailsViewModel.TimeTableId);

                if (lineDetails != null)
                {
                    we.Result = lineDetails;
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                if (we?.Result != null && we.Result is LineDetails lineDetails)
                {
                    var line = LineDetailsViewModel.Line;
                    var timeTableId = LineDetailsViewModel.TimeTableId;

                    imAwait.Close();

                    if (lineDetails != null)
                    {
                        LineDetailsViewModel = new LineDetailsViewModel(line, lineDetails, timeTableId);
                        SelectedLineDirectionViewModel = LineDetailsViewModel.Directions.FirstOrDefault();
                    }
                    else
                        ShowDownloadingErrorMessage(
                            langConf.Messages.DownloadErrorTitle,
                            langConf.Messages.LineDetailsViewPageDownloadErrorDesc);
                }
                else
                {
                    imAwait.Close();
                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.LineDetailsViewPageDownloadErrorDesc);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load departures data. </summary>
        private void LoadDepartures()
        {
            //  Get basic data.
            var langConf = ConfigManager.Instance.LangConfig;
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;
            var bgLoader = new BackgroundWorker();

            //  Create await internal message.
            var imAwait = new AwaitInternalMessageEx(imContainer,
                langConf.Messages.DownloadTitle,
                langConf.Messages.DeparturesViewPageDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                var departures = ZtmDataDownloader.SimpleDownloader.DownloadDepartures(
                    LineDetailsViewModel.Line,
                    SelectedLineStopViewModel.LineStop);

                if (departures?.Any() ?? false)
                {
                    we.Result = departures;
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                if (we?.Result != null && we.Result is Dictionary<DepartureGroup, List<Departure>> departures)
                {
                    Departures = new ObservableCollection<LineDepartureGroupViewModel>(
                        departures.Select(kvp => new LineDepartureGroupViewModel(kvp.Key, kvp.Value)));

                    imAwait.Close();
                }
                else
                {
                    Departures = new ObservableCollection<LineDepartureGroupViewModel>();

                    imAwait.Close();

                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.DeparturesViewPageDownloadErrorDesc);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load arrivals data. </summary>
        /// <param name="lineDepartureViewModel"> Line departure view model. </param>
        private void LoadArrivals(LineDepartureViewModel lineDepartureViewModel)
        {
            //  Get basic data.
            var langConf = ConfigManager.Instance.LangConfig;
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;
            var bgLoader = new BackgroundWorker();

            //  Create await internal message.
            var imAwait = new AwaitInternalMessageEx(imContainer,
                langConf.Messages.DownloadTitle,
                langConf.Messages.ArrivalsViewPageDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                var departureDetails = ZtmDataDownloader.SimpleDownloader.DownloadArrivalsData(
                    LineDetailsViewModel.Line,
                    lineDepartureViewModel.Departure);

                if (departureDetails != null)
                {
                    we.Result = new DepartureDetailsViewModel(departureDetails);
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                if (we?.Result != null && we.Result is DepartureDetailsViewModel departureDetalsViewModel)
                {
                    imAwait.Close();
                    LoadArrivalsInternalMessage(departureDetalsViewModel);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load and show arrivals internal message. </summary>
        /// <param name="departureDetailsViewModel"> Departure details view model. </param>
        private void LoadArrivalsInternalMessage(DepartureDetailsViewModel departureDetailsViewModel)
        {
            //  Get basic data.
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;

            var imArrivals = new ArrivalsInternalMessage(imContainer, departureDetailsViewModel);

            imContainer.ShowMessage(imArrivals);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Show download error internal message method. </summary>
        /// <param name="title"> Error message title. </param>
        /// <param name="message"> Error message. </param>
        private void ShowDownloadingErrorMessage(string title, string message)
        {
            //  Get basic data.
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;

            var imError = InternalMessageEx.CreateErrorMessage(
                imContainer, title, message);

            InternalMessagesHelper.ApplyVisualStyle(imError);

            imContainer.ShowMessage(imError);
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

        #endregion HEADER INTERACTION METHODS

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
