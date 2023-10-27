using chkam05.Tools.ControlsEx;
using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Data.Lines;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZtmDataViewer.Components;
using ZtmDataViewer.Converters.MpkCzestochowa;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.MpkCzestochowa;
using ZtmDataViewer.Data.Static;
using ZtmDataViewer.Utilities;
using ZtmDataViewer.Windows;

namespace ZtmDataViewer.Pages.MpkCzestochowa
{
    /// <summary>
    /// Logika interakcji dla klasy LineDetailsViewPage.xaml
    /// </summary>
    public partial class LineDetailsViewPage : BasePage
    {

        //  VARIABLES

        private Line _line;
        private LineDetailsViewModel _lineDetailsViewModel;
        private LineStopViewModel _lineStopViewModel;


        //  GETTERS & SETTERS

        public LineDetailsViewModel LineDetailsViewModel
        {
            get => _lineDetailsViewModel;
            set => SetLineDetailsViewModel(value);
        }

        public LineStopViewModel LineStopViewModel
        {
            get => _lineStopViewModel;
            set
            {
                _lineStopViewModel = value;
                OnPropertyChanged(nameof(LineStopViewModel));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsViewPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        /// <param name="line"> Line data object. </param>
        /// <param name="lineDetailsViewModel"> Line details view model. </param>
        public LineDetailsViewPage(PagesController pagesController, Line line, LineDetailsViewModel lineDetailsViewModel)
            : base(pagesController)
        {
            //  Initialize user interface.
            InitializeComponent();

            //  Initialize data.
            _line = line;
            LineDetailsViewModel = lineDetailsViewModel;

            //  Set additional values.
            UpdateIconKind();
        }

        #endregion CLASS METHODS

        #region DATA MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Load line details data. </summary>
        private void LoadLineDetails(DateTime? dateTime = null, string? route = null)
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
                var downloader = new MpkCzestochowaDownloader.Downloaders.LineDetailsDownloader();
                var request = new MpkCzestochowaDownloader.Data.Line.LineDetailsRequestModel(
                    _line.TransportType, _line.Value, dateTime, route);
                var response = downloader.DownloadData(request);

                if (response.HasData && !response.HasErrors)
                {
                    var lineDetails = response.LineDetails;

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
                    imAwait.Close();

                    if (lineDetails != null)
                    {
                        LineDetailsViewModel = new LineDetailsViewModel(lineDetails);
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
        /// <summary> Set line details view model. </summary>
        /// <param name="lineDetailsViewModel"> Line details view model. </param>
        private void SetLineDetailsViewModel(LineDetailsViewModel lineDetailsViewModel)
        {
            if (_lineDetailsViewModel != null)
            {
                _lineDetailsViewModel.PropertyChanged -= OnLineDetailsViewModelPropertyChanged;
                SetupComboBox<RouteVariantViewModel>(routeVariantsComboBox, UpdateMode.Clear, RouteVariantsComboBoxSelectionChanged);
                SetupComboBox<TimeTableDateViewModel>(timeTableDatesComboBox, UpdateMode.Clear, TimeTableDatesComboBoxSelectionChanged);
            }

            _lineDetailsViewModel = lineDetailsViewModel;
            _lineDetailsViewModel.PropertyChanged += OnLineDetailsViewModelPropertyChanged;

            OnPropertyChanged(nameof(LineDetailsViewModel));

            SetupComboBox(routeVariantsComboBox, UpdateMode.Set, RouteVariantsComboBoxSelectionChanged,
                _lineDetailsViewModel.RouteVariants, _lineDetailsViewModel.SelectedRouteVariant);

            SetupComboBox(timeTableDatesComboBox, UpdateMode.Set, TimeTableDatesComboBoxSelectionChanged,
                _lineDetailsViewModel.Dates, _lineDetailsViewModel.SelectedDate);
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
        /// <summary> Method invoked after changing selection in timeTableDatesComboBox. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Selection Changed Event Arguments. </param>
        private void TimeTableDatesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after changing selection in routeVariantsComboBox. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Selection Changed Event Arguments. </param>
        private void RouteVariantsComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

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
                    //SelectedLineStopViewModel = lineStopViewModel;
                    //LoadDepartures();
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after double clicing on departures list view ex item. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Mouse Button Event Arguments. </param>
        private void DeparturesListViewEx_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*if (e.OriginalSource is FrameworkElement source)
            {
                if (source.DataContext is LineDepartureViewModel lineDepartureViewModel)
                {
                    LoadArrivals(lineDepartureViewModel);
                }
            }*/
        }

        #endregion DATA INTERACTION METHODS

        #region HEADER INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking refresh button. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void RefresButtonEx_Click(object sender, RoutedEventArgs e)
        {
            var dateTime = _lineDetailsViewModel?.SelectedDate?.TimeTableDate.Date;
            var routeVariant = _lineDetailsViewModel?.SelectedRouteVariant?.RouteVariant.Variant;

            LoadLineDetails(dateTime, routeVariant);
        }

        #endregion HEADER INTERACTION METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method for invoking PropertyChangedEventHandler external method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnLineDetailsViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LineDetailsViewModel.SelectedDate))
            {
                //
            }
            
            if (e.PropertyName == nameof(LineDetailsViewModel.SelectedRouteVariant))
            {
                //
            }
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
        /// <summary> Setup combobox component. </summary>
        /// <typeparam name="T"> Type of items that will be added or removed. </typeparam>
        /// <param name="comboBox"> ComboBox component. </param>
        /// <param name="mode"> Update mode: Clear/Set. </param>
        /// <param name="selectionChangedMethod"> Selection changed method. </param>
        /// <param name="items"> Items to add. </param>
        /// <param name="selectedItem"> Item that will be set as selected. </param>
        private void SetupComboBox<T>(ComboBox comboBox, UpdateMode mode,
            SelectionChangedEventHandler selectionChangedMethod,
            ObservableCollection<T>? items = null, T? selectedItem = null) where T : class, INotifyPropertyChanged
        {
            if (comboBox != null)
            {
                switch (mode)
                {
                    case UpdateMode.Clear:
                        comboBox.SelectionChanged -= selectionChangedMethod;
                        comboBox.SelectedItem = null;
                        comboBox.Items.Clear();
                        break;

                    case UpdateMode.Set:
                        if (items != null)
                            foreach (var item in items)
                                comboBox.Items.Add(item);

                        if (selectedItem != null)
                            comboBox.SelectedItem = selectedItem;

                        comboBox.SelectionChanged += selectionChangedMethod;
                        break;

                    case UpdateMode.None:
                        break;
                }
            }
        }

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
