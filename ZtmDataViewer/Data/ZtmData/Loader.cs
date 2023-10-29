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
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.TimeTables;
using ZtmDataViewer.Components;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.MainMenu;
using ZtmDataViewer.Data.MpkCzestochowa;
using ZtmDataViewer.Data.ZtmData;
using ZtmDataViewer.InternalMessages.ZtmData;
using ZtmDataViewer.Pages.ZtmData;
using ZtmDataViewer.Utilities;
using ZtmDataViewer.Windows;

namespace ZtmDataViewer.Data.ZtmData
{
    public static class Loader
    {

        //  DELEGATES

        public delegate void LinesDataLoadedEventHandler(
            ObservableCollection<LineGroupViewModel> lineGroupCollection);

        public delegate void LineDetailsDataLoadedEventHandler(
            LineDetailsViewModel lineDetailsViewModel);

        public delegate void LineDearpturesDataLoadedEventHandler(
            ObservableCollection<LineDepartureGroupViewModel> departureGroupViewModelCollection);


        //  METHODS

        #region LOADER METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Load lines data. </summary>
        /// <param name="onDataLoadedEventHandler"> On data loaded event handler. </param>
        public static void LoadLinesData(LinesDataLoadedEventHandler onDataLoadedEventHandler)
        {
            //  Get basic data.
            var langConf = ConfigManager.Instance.LangConfig;
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;
            var bgLoader = new BackgroundWorker();

            //  Create await internal message.
            var imAwait = new AwaitInternalMessageEx(imContainer,
                langConf.Messages.DownloadTitle,
                langConf.Messages.LinesViewPageDownloadDesc,
                PackIconKind.ChartTimelineVariant)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                var linesDict = ZtmDataDownloader.SimpleDownloader.DownloadLines();

                if (linesDict?.Any() ?? false)
                {
                    var lineGroups = linesDict.Select(kvp => new LineGroupViewModel(kvp)).ToList();
                    we.Result = lineGroups;
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                if (we?.Result != null)
                {
                    var lineGroups = (List<LineGroupViewModel>)we.Result;
                    var lineGroupsCollection = new ObservableCollection<LineGroupViewModel>(lineGroups);

                    onDataLoadedEventHandler?.Invoke(lineGroupsCollection);
                    imAwait.Close();
                }
                else
                {
                    var lineGroupsCollection = new ObservableCollection<LineGroupViewModel>();

                    onDataLoadedEventHandler?.Invoke(lineGroupsCollection);
                    imAwait.Close();

                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.LinesViewPageDownloadErrorDesc);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load line details data. </summary>
        /// <param name="line"> Line data object. </param>
        /// <param name="pagesController"> Pages controller. </param>
        /// <param name="onDataLoadedEventHandler"> On data loaded event handler. </param>
        /// <param name="timeTable"> Optional time table data object. </param>
        /// <param name="isDataReload"> Reload data. </param>
        public static void LoadLineDetailsData(Line line, PagesController pagesController,
            LineDetailsDataLoadedEventHandler? onDataLoadedEventHandler = null,
            string? timeTableId = null, bool isDataReload = false)
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
                var lineDetails = ZtmDataDownloader.SimpleDownloader.DownloadLineDetails(line, timeTableId);

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
                    var lineDetailsViewModel = new LineDetailsViewModel(line, lineDetails, timeTableId);

                    if (isDataReload)
                    {
                        onDataLoadedEventHandler?.Invoke(lineDetailsViewModel);
                    }
                    else
                    {
                        var lineDetailsViewPage = new LineDetailsViewPage(pagesController, lineDetailsViewModel);
                        pagesController?.LoadPage(lineDetailsViewPage);
                    }

                    imAwait.Close();
                }
                else if (!string.IsNullOrEmpty(timeTableId))
                {
                    imAwait.Close();
                    LoadTimeTablesData(line, pagesController);
                }
                else
                {
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
        /// <summary> Load time tables data. </summary>
        /// <param name="line"> Line data object. </param>
        /// <param name="pagesController"> Pages controller. </param>
        public static void LoadTimeTablesData(Line line, PagesController pagesController)
        {
            //  Get basic data.
            var langConf = ConfigManager.Instance.LangConfig;
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;
            var bgLoader = new BackgroundWorker();

            //  Create await internal message.
            var imAwait = new AwaitInternalMessageEx(imContainer,
                langConf.Messages.DownloadTitle,
                langConf.Messages.TimeTablesViewPageDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                var timeTables = ZtmDataDownloader.SimpleDownloader.DownloadTimeTables(line);

                if (timeTables != null)
                {
                    we.Result = timeTables;
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                imAwait.Close();

                if (we?.Result != null && we.Result is List<TimeTable> timeTables)
                {
                    var timeTableViewModels = timeTables.Select(t => new TimeTableViewModel(t)).ToList();

                    var imTimeTableSelector = new TimeTableSelectorInternalMessage(
                        imContainer, line, timeTableViewModels);

                    imTimeTableSelector.OnClose += (sender, e) =>
                    {
                        if (e.Result == InternalMessageResult.Ok)
                        {
                            var internalMessage = sender as TimeTableSelectorInternalMessage;
                            var line = internalMessage?.Line;
                            var timeTable = internalMessage?.SelectedTimeTable?.TimeTable;

                            if (line != null && timeTable != null)
                                LoadLineDetailsData(line, pagesController, timeTableId: timeTable.ID);
                        }
                    };

                    imContainer.ShowMessage(imTimeTableSelector);
                }
                else
                {
                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.TimeTablesViewPageDownloadErrorDesc);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load line departures data. </summary>
        /// <param name="line"> Line data object. </param>
        /// <param name="lineStop"> Line stop object. </param>
        /// <param name="onDataLoadedEventHandler"> On data loaded event handler. </param>
        public static void LoadLineDepartures(Line line, LineStop lineStop,
            LineDearpturesDataLoadedEventHandler onDataLoadedEventHandler)
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
                    line, lineStop);

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
                    var departureGroupViewModels = new ObservableCollection<LineDepartureGroupViewModel>(
                        departures.Select(kvp => new LineDepartureGroupViewModel(kvp.Key, kvp.Value)));

                    onDataLoadedEventHandler?.Invoke(departureGroupViewModels);

                    imAwait.Close();
                }
                else
                {
                    var departureGroupViewModels = new ObservableCollection<LineDepartureGroupViewModel>();

                    onDataLoadedEventHandler?.Invoke(departureGroupViewModels);

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
        /// <summary> Load line arrivals data. </summary>
        /// <param name="line"> Line data object. </param>
        /// <param name="departure"> Line departure data object. </param>
        public static void LoadLineArrivals(Line line, Departure departure)
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
                    line, departure);

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
                if (we?.Result != null && we.Result is DepartureDetailsViewModel departureDetailsViewModel)
                {
                    imAwait.Close();

                    var imArrivals = new ArrivalsInternalMessage(imContainer, departureDetailsViewModel);

                    imContainer.ShowMessage(imArrivals);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        #endregion LOADER METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Show download error internal message method. </summary>
        /// <param name="title"> Error message title. </param>
        /// <param name="message"> Error message. </param>
        private static void ShowDownloadingErrorMessage(string title, string message)
        {
            //  Get basic data.
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var imContainer = mainWindow.InternalMessagesContainer;

            var imError = InternalMessageEx.CreateErrorMessage(
                imContainer, title, message);

            InternalMessagesHelper.ApplyVisualStyle(imError);

            imContainer.ShowMessage(imError);
        }

        #endregion UTILITY METHODS

    }
}
