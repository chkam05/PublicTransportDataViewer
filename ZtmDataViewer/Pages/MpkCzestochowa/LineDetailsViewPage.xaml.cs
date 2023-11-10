using chkam05.Tools.ControlsEx;
using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using MpkCzestochowaDownloader.Data.Departures;
using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Data.Static;
using MpkCzestochowaDownloader.Mappers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZtmDataDownloader.Data.Line;
using PublicTransportDataViewer.Components;
using PublicTransportDataViewer.Converters.MpkCzestochowa;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.Data.MainMenu;
using PublicTransportDataViewer.Data.MpkCzestochowa;
using PublicTransportDataViewer.Data.Static;
using PublicTransportDataViewer.Utilities;
using PublicTransportDataViewer.Windows;

namespace PublicTransportDataViewer.Pages.MpkCzestochowa
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
        private LineDeparturesViewModel _lineDeparturesViewModel;
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

        public Line Line
        {
            get => _line;
            set
            {
                _line = value;
                OnPropertyChanged(nameof(Line));
            }
        }

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

        public LineDeparturesViewModel LineDeparturesViewModel
        {
            get => _lineDeparturesViewModel;
            set
            {
                _lineDeparturesViewModel = value;
                OnPropertyChanged(nameof(LineDeparturesViewModel));
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
        /// <param name="line"> Line data object. </param>
        /// <param name="lineDetailsViewModel"> Line details view model. </param>
        public LineDetailsViewPage(PagesController pagesController, Line line, LineDetailsViewModel lineDetailsViewModel, string sourceUrl)
            : base(pagesController)
        {
            //  Initialize user interface.
            InitializeComponent();

            //  Initialize data.
            Line = line;
            LineDetailsViewModel = lineDetailsViewModel;
            SourceUrl = sourceUrl;

            //  Set additional values.
            UpdateIconKind();
        }

        #endregion CLASS METHODS

        #region DATA MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Load line details data. </summary>
        /// <param name="line"> Line data object. </param>
        /// <param name="dateTime"> Time table date. </param>
        /// <param name="route"> Route variant identifier. </param>
        /// <param name="isDataReload"> Reload data. </param>
        private void LoadLineDetails(Line line, DateTime? dateTime = null, string? route = null, bool isDataReload = true)
        {
            var onDataLoadedEventHandler = new Loader.LineDetailsDataLoadedEventHandler((lineDetailsViewModel, line, isDataReloaded, sourceUrl) =>
            {
                if (!isDataReload)
                {
                    _pagesController?.LoadPage(
                        new LineDetailsViewPage(_pagesController, line, lineDetailsViewModel, sourceUrl ?? string.Empty),
                        false, true);
                    return;
                }

                Line = line;
                LineDetailsViewModel = lineDetailsViewModel;
                SourceUrl = sourceUrl ?? string.Empty;

                UpdateIconKind();
                _pagesController?.ForceUpdate();
            });

            Loader.LoadLineDetails(line, onDataLoadedEventHandler, dateTime, route, isDataReload);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load line stop departures data. </summary>
        /// <param name="lineStop"> Line stop data. </param>
        private void LoadLineDepartures(LineStop lineStop)
        {
            var onDataLoadedEventHandler = new Loader.LineDeparturesDataLoadedEventHandler((lineDeparturesViewModel, sourceUrl) =>
            {
                LineDeparturesViewModel = lineDeparturesViewModel;
                SourceUrl = sourceUrl ?? string.Empty;
            });

            Loader.LoadLineDepartures(lineStop, onDataLoadedEventHandler);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load line arrivals data. </summary>
        /// <param name="departureViewModel"> Departure view model. </param>
        private void LoadLineArrivals(DepartureViewModel departureViewModel)
        {
            var tripId = departureViewModel.Departure.DataTrip;
            var time = departureViewModel.Departure.Time?.ToString("HH:mm:ss");
            var date = (LineDeparturesViewModel.LineDepartures?.Dates.FirstOrDefault(d => d.Selected)?.Date ?? DateTime.Now)
                .ToString("yyyy-MM-dd");

            Loader.LoadLineArrivals(tripId, time, date);
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

        #endregion DATA MANAGEMENT METHODS

        #region DATA INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after changing selection in timeTableDatesComboBox. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Selection Changed Event Arguments. </param>
        private void TimeTableDatesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is TimeTableDateViewModel timeTableDateViewModel)
                {
                    LineDetailsViewModel.SelectedDate = timeTableDateViewModel;
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after changing selection in routeVariantsComboBox. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Selection Changed Event Arguments. </param>
        private void RouteVariantsComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is RouteVariantViewModel routeVariantViewModel)
                {
                    LineDetailsViewModel.SelectedRouteVariant = routeVariantViewModel;
                }
            }
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
                    LineStopViewModel = lineStopViewModel;
                    LoadLineDepartures(LineStopViewModel.LineStop);
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after double clicing on other lines list view ex item. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Mouse Button Event Arguments. </param>
        private void OtherLinesListViewEx_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement source)
            {
                if (source.DataContext is OtherLineViewModel otherLineViewModel)
                {
                    var dateTime = _lineDetailsViewModel?.SelectedDate?.TimeTableDate.Date;
                    var value = otherLineViewModel.OtherLine.Value;
                    var transportType = TransportTypesMapper.MapFromAttributes(otherLineViewModel.OtherLine.Attributes);

                    if (transportType.HasValue)
                    {
                        var line = new Line()
                        {
                            TransportType = transportType.Value,
                            Value = value
                        };

                        LoadLineDetails(line, dateTime, null, false);
                    }
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
                if (source.DataContext is DepartureViewModel departureViewModel)
                {
                    LoadLineArrivals(departureViewModel);
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
            var dateTime = _lineDetailsViewModel?.SelectedDate?.TimeTableDate.Date;
            var routeVariant = _lineDetailsViewModel?.SelectedRouteVariant?.RouteVariant.Variant;

            LoadLineDetails(_line, dateTime, routeVariant);
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
        /// <summary> Method for invoking PropertyChangedEventHandler external method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnLineDetailsViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LineDetailsViewModel.SelectedDate)
                || e.PropertyName == nameof(LineDetailsViewModel.SelectedRouteVariant))
            {
                var dateTime = _lineDetailsViewModel?.SelectedDate?.TimeTableDate.Date;
                var routeVariant = _lineDetailsViewModel?.SelectedRouteVariant?.RouteVariant.Variant;

                LineDeparturesViewModel = null;
                LineStopViewModel = null;
                LoadLineDetails(_line, dateTime, routeVariant);
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
