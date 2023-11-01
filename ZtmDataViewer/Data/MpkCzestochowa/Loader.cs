using chkam05.Tools.ControlsEx.InternalMessages;
using MaterialDesignThemes.Wpf;
using MpkCzestochowaDownloader.Data.Arrives;
using MpkCzestochowaDownloader.Data.Departures;
using MpkCzestochowaDownloader.Data.Line;
using MpkCzestochowaDownloader.Data.Lines;
using MpkCzestochowaDownloader.Downloaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ZtmDataViewer.Components;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.ZtmData;
using ZtmDataViewer.Events;
using ZtmDataViewer.InternalMessages.MpkCzestochowa;
using ZtmDataViewer.Utilities;
using ZtmDataViewer.Windows;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public static class Loader
    {

        //  DELEGATES

        public delegate void LinesDataLoadedEventHandler(
            ObservableCollection<LineGroupViewModel> lineGroupViewModelCollection,
            ObservableCollection<MessageViewModel> messageViewModelCollection,
            string? requestUrl);

        public delegate void LineDetailsDataLoadedEventHandler(
            LineDetailsViewModel lineDetailsViewModel,
            Line line,
            bool isDataReload,
            string? requestUrl);

        public delegate void LineDeparturesDataLoadedEventHandler(
            LineDeparturesViewModel lineDeparturesViewModel,
            string? requestUrl);


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
                var downloader = new MpkCzestochowaDownloader.Downloaders.LinesDownloader();
                var request = new MpkCzestochowaDownloader.Data.Lines.LinesRequestModel();
                var response = downloader.DownloadData(request);

                List<LineGroupViewModel>? result = null;
                List<MessageViewModel>? messages = null;
                string? requestUrl = response.URL;

                if (response.HasData && !response.HasErrors)
                {
                    result = response.Lines.Select(kvp => new LineGroupViewModel(kvp)).ToList();
                    messages = response.Messages.Select(m => new MessageViewModel(m)).ToList();
                }

                we.Result = new object?[] { result, requestUrl, messages };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                List<LineGroupViewModel>? result = null;
                string? requestUrl = null;

                if (GetResult(we, out result, out requestUrl))
                {
                    var objResult = we?.Result as object?[];

                    var lineGroups = new ObservableCollection<LineGroupViewModel>(result);
                    ObservableCollection<MessageViewModel>? messages = null;

                    if (objResult?.Length > 2 && objResult[2] is List<MessageViewModel> messagesResult)
                        messages = new ObservableCollection<MessageViewModel>(messagesResult);

                    onDataLoadedEventHandler?.Invoke(lineGroups, messages, requestUrl);
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
        /// <param name="onDataLoadedEventHandler"> On data loaded event handler. </param>
        /// <param name="dateTime"> Time table date. </param>
        /// <param name="route"> Route varaint identifiers. </param>
        /// <param name="isDataReload"> Reload data. </param>
        public static void LoadLineDetails(Line line, LineDetailsDataLoadedEventHandler onDataLoadedEventHandler,
            DateTime? dateTime = null, string? route = null, bool isDataReload = false)
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
                var downloader = new MpkCzestochowaDownloader.Downloaders.LineDetailsDownloader();
                var request = new MpkCzestochowaDownloader.Data.Line.LineDetailsRequestModel(
                    line.TransportType, line.Value, dateTime, route);
                var response = downloader.DownloadData(request);

                LineDetails? result = null;
                string? requestUrl = response.URL;

                if (response.HasData && !response.HasErrors)
                    result = response.LineDetails;

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                LineDetails? result = null;
                string? requestUrl = null;

                if (GetResult(we, out result, out requestUrl))
                {
                    imAwait.Close();

                    var lineDetailsViewModel = new LineDetailsViewModel(result);

                    onDataLoadedEventHandler?.Invoke(lineDetailsViewModel, line, isDataReload, requestUrl);
                }
                else
                {
                    imAwait.Close();

                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.LineDetailsDownloadErrorDesc);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load line departures data. </summary>
        /// <param name="lineStop"> Line stop data object. </param>
        /// <param name="onDataLoadedEventHandler"> On data loaded event handler. </param>
        public static void LoadLineDepartures(LineStop lineStop, LineDeparturesDataLoadedEventHandler onDataLoadedEventHandler)
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
                var downloader = new MpkCzestochowaDownloader.Downloaders.LineDeparturesDownloader();
                var request = new MpkCzestochowaDownloader.Data.Departures.LineDeparturesRequestModel(lineStop.URL);
                var response = downloader.DownloadData(request);

                LineDepartures? result = null;
                string? requestUrl = response.URL;

                if (response.HasData && !response.HasErrors)
                    result = response.LineDepartures;

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                LineDepartures? result = null;
                string? requestUrl = null;

                if (GetResult(we, out result, out requestUrl))
                {
                    imAwait.Close();

                    var lineDeparturesViewModel = new LineDeparturesViewModel(result);

                    onDataLoadedEventHandler.Invoke(lineDeparturesViewModel, requestUrl);
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
        /// <param name="tripId"> Trip identifier. </param>
        /// <param name="departureTime"> Departure time. </param>
        /// <param name="departureDate"> Departure date. </param>
        public static void LoadLineArrivals(string tripId, string departureTime, string departureDate)
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
                var downloader = new LineArrivalsDownloader();
                var request = new LineArrivalsRequestModel(tripId, departureTime, departureDate);
                var response = downloader.DownloadData(request);

                LineArrivalsViewModel? result = null;
                string? requestUrl = response.URL;

                if (!response.HasErrors && response.HasData)
                    result = new LineArrivalsViewModel(response.LineArrivals);

                we.Result = new object?[] { result, requestUrl };
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                LineArrivalsViewModel? result = null;
                string? requestUrl = null;

                if (GetResult(we, out result, out requestUrl))
                {
                    imAwait.Close();

                    var imArrivals = new ArrivalsInternalMessage(imContainer, result);

                    imContainer.ShowMessage(imArrivals);
                }
                else
                {
                    imAwait.Close();

                    ShowDownloadingErrorMessage(
                        langConf.Messages.DownloadErrorTitle,
                        langConf.Messages.ArrivalsDownloadErrorDesc);
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
        private static bool GetResult<T>(RunWorkerCompletedEventArgs data, out T? result, out string? requestUrl) where T : class
        {
            if (data.Error == null)
            {
                var resultData = data?.Result as object?[];

                if (resultData != null && resultData.Length >= 2)
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
        private static bool GetListResult<T>(RunWorkerCompletedEventArgs data, out List<T> result, out string? requestUrl) where T : class
        {
            if (data.Error == null)
            {
                var resultData = data?.Result as object?[];

                if (resultData != null && resultData.Length >= 2)
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
        private static bool GetDictResult<TKey, TValue>(RunWorkerCompletedEventArgs data, out Dictionary<TKey, TValue> result, out string? requestUrl)
            where TKey : class
            where TValue : class
        {
            if (data.Error == null)
            {
                var resultData = data?.Result as object?[];

                if (resultData != null && resultData.Length >= 2)
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
