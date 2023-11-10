using chkam05.Tools.ControlsEx.Data;
using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using ZtmDataDownloader.Data.Arrivals;
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.TimeTables;
using ZtmDataDownloader.Downloaders;
using PublicTransportDataViewer.Components;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.InternalMessages.ZtmData;
using PublicTransportDataViewer.Pages.ZtmData;
using PublicTransportDataViewer.Utilities;
using PublicTransportDataViewer.Windows;

namespace PublicTransportDataViewer.Data.ZtmData
{
    public static class Loader
    {

        //  DELEGATES

        public delegate void LinesDataLoadedEventHandler(
            ObservableCollection<LineGroupViewModel> lineGroupCollection, string? sourceUrl);

        public delegate void LineDetailsDataLoadedEventHandler(
            LineDetailsViewModel lineDetailsViewModel, string? sourceUrl);

        public delegate void LineDearpturesDataLoadedEventHandler(
            ObservableCollection<LineDepartureGroupViewModel> departureGroupViewModelCollection, string? sourceUrl);


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
                langConf.Messages.LinesDownloadDesc,
                PackIconKind.ChartTimelineVariant)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                var downloader = new LinesDownloader();
                var request = new LinesRequestModel();
                var response = downloader.DownloadData(request);

                List<LineGroupViewModel>? result = null;
                string? requestUrl = response.URL;

                if (response != null && response.HasData)
                {
                    result = response.Lines.Select(kvp => new LineGroupViewModel(kvp)).ToList();
                }

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                List<LineGroupViewModel>? result = null;
                string? requestUrl = null;

                if (GetListResult(we, out result, out requestUrl))
                {
                    var lineGroupsCollection = new ObservableCollection<LineGroupViewModel>(result);

                    onDataLoadedEventHandler?.Invoke(lineGroupsCollection, requestUrl);

                    imAwait.Close();
                }
                else
                {
                    imAwait.Close();

                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.LinesDownloadErrorDesc);
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
                langConf.Messages.LineDetailsDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                LineDetails? result = null;
                string? requestUrl = null;

                if (line != null)
                {
                    var downloader = new LineDetailsDownloader();
                    var request = new LineDetailsRequestModel(line.Value, line.Type, timeTableId);
                    var response = downloader.DownloadData(request);

                    requestUrl = response.URL;

                    if (response != null && response.HasData)
                    {
                        result = response.Line;
                    }
                }

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                LineDetails? result = null;
                string? requestUrl = null;

                if (GetResult(we, out result, out requestUrl))
                {
                    var lineDetailsViewModel = new LineDetailsViewModel(line, result, timeTableId);

                    imAwait.Close();

                    pagesController.LoadPage(
                        new LineDetailsViewPage(pagesController, lineDetailsViewModel, requestUrl));
                }
                else
                {
                    imAwait.Close();

                    LoadTimeTablesData(line, pagesController);
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
                langConf.Messages.TimeTablesDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                List<TimeTable>? result = null;
                string? requestUrl = null;

                if (line != null)
                {
                    var downloader = new TimeTablesDownloader();
                    var request = new TimeTableRequestModel(line.Value, line.Type);
                    var response = downloader.DownloadData(request);

                    requestUrl = response.URL;

                    if (response != null && response.HasData)
                    {
                        result = response.TimeTables;
                    }
                }

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                imAwait.Close();

                List<TimeTable>? timeTables = null;
                string? requestUrl = null;

                if (GetListResult(we, out timeTables, out requestUrl))
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
                        langConf.Messages.TimeTablesDownloadErrorDesc);
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
                langConf.Messages.DeparturesDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                Dictionary<DepartureGroup, List<Departure>>? result = null;
                string? requestUrl = null;

                if (line != null && lineStop != null)
                {
                    var downloader = new DeparturesDownloader();
                    var request = new DeparturesRequestModel(line.Value, line.Type, lineStop.DirectionID, lineStop.ID, lineStop.TimeTableID);
                    var response = downloader.DownloadData(request);

                    requestUrl = response.URL;

                    if (response != null && response.HasData)
                    {
                        result = response.Departures;
                    }
                }

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                Dictionary<DepartureGroup, List<Departure>>? result = null;
                string? requestUrl = null;

                if (GetDictResult(we, out result, out requestUrl))
                {
                    var departureGroupViewModels = new ObservableCollection<LineDepartureGroupViewModel>(
                        result.Select(kvp => new LineDepartureGroupViewModel(kvp.Key, kvp.Value)));

                    onDataLoadedEventHandler?.Invoke(departureGroupViewModels, requestUrl);

                    imAwait.Close();
                }
                else
                {
                    imAwait.Close();

                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.DeparturesDownloadErrorDesc);
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
                langConf.Messages.ArrivalsDownloadDesc,
                PackIconKind.DepartureBoard)
            {
                AllowCancel = false,
                AllowHide = false
            };

            InternalMessagesHelper.ApplyVisualStyle(imAwait);

            //  Setup background worker methods.
            bgLoader.DoWork += (s, we) =>
            {
                DepartureDetailsViewModel? result = null;
                string? requestUrl = null;

                if (line != null && departure != null)
                {
                    var downloader = new ArrivalsDownloader();
                    var request = new ArrivalRequestModel(line.Value, line.Type, departure.DirectionID, departure.StopID, departure.TimeTableID, departure.ID, departure.DirectID);
                    var response = downloader.DownloadData(request);

                    requestUrl = response.URL;

                    if (response != null && response.HasData)
                    {
                        result = new DepartureDetailsViewModel(response.DepartureDetails);
                    }
                }

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                DepartureDetailsViewModel? result = null;
                string? requestUrl = null;

                if (GetResult(we, out result, out requestUrl))
                {
                    imAwait.Close();

                    var imArrivals = new ArrivalsInternalMessage(imContainer, result);

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
        /// <summary> Get background worker result. </summary>
        /// <typeparam name="T"> Result data type. </typeparam>
        /// <param name="data"> Background worker result. </param>
        /// <param name="result"> Result data. </param>
        /// <param name="requestUrl"> Request url. </param>
        /// <returns> True - data retrieved; False - otherwise. </returns>
        private static bool GetResult<T>(RunWorkerCompletedEventArgs data, out T? result, out string? requestUrl) where T: class
        {
            if (data.Error == null)
            {
                var resultData = data?.Result as object?[];

                if (resultData != null && resultData.Length == 2)
                {
                    result = resultData[0] as T;
                    requestUrl = resultData[1] as string;

                    if (result != null && !string.IsNullOrEmpty(requestUrl))
                        return true;
                }
            }

            result = null;
            requestUrl = null;
            return false;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get background worker result list. </summary>
        /// <typeparam name="T"> Result data type. </typeparam>
        /// <param name="data"> Background worker result. </param>
        /// <param name="result"> List of result data. </param>
        /// <param name="requestUrl"> Request url. </param>
        /// <returns> True - data retrieved; False - otherwise. </returns>
        private static bool GetListResult<T>(RunWorkerCompletedEventArgs data, out List<T> result, out string? requestUrl) where T: class
        {
            if (data.Error == null)
            {
                var resultData = data?.Result as object?[];

                if (resultData != null && resultData.Length == 2)
                {
                    var listResult = resultData[0] as List<T>;
                    requestUrl = resultData[1] as string;

                    if (listResult != null && !string.IsNullOrEmpty(requestUrl))
                    {
                        result = listResult;
                        return true;
                    }
                }
            }

            result = new List<T>();
            requestUrl = null;
            return false;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get background worker result dictionary. </summary>
        /// <typeparam name="TKey"> Result key data type. </typeparam>
        /// <typeparam name="TValue"> Result value data type. </typeparam>
        /// <param name="data"> Background worker result. </param>
        /// <param name="result"> Dictionary of result data. </param>
        /// <param name="requestUrl"> Request url. </param>
        /// <returns> True - data retrieved; False - otherwise. </returns>
        private static bool GetDictResult<TKey,TValue>(RunWorkerCompletedEventArgs data, out Dictionary<TKey, TValue> result, out string? requestUrl)
            where TKey : class
            where TValue : class
        {
            if (data.Error == null)
            {
                var resultData = data?.Result as object?[];

                if (resultData != null && resultData.Length == 2)
                {
                    var dictResult = resultData[0] as Dictionary<TKey, TValue>;
                    requestUrl = resultData[1] as string;

                    if (dictResult != null && !string.IsNullOrEmpty(requestUrl))
                    {
                        result = dictResult;
                        return true;
                    }
                }
            }

            result = new Dictionary<TKey, TValue>();
            requestUrl = null;
            return false;
        }

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
