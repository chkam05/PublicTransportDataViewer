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
            ObservableCollection<MessageViewModel> messageViewModelCollection);

        public delegate void LineDetailsDataLoadedEventHandler(
            LineDetailsViewModel lineDetailsViewModel,
            Line line,
            bool isDataReload);

        public delegate void LineDeparturesDataLoadedEventHandler(
            LineDeparturesViewModel lineDeparturesViewModel);


        //  METHODS

        #region LINES DATA

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
                var downloader = new MpkCzestochowaDownloader.Downloaders.LinesDownloader();
                var request = new MpkCzestochowaDownloader.Data.Lines.LinesRequestModel();
                var response = downloader.DownloadData(request);

                if (response.HasData && !response.HasErrors)
                {
                    var lineGroups = response.Lines.Select(kvp => new LineGroupViewModel(kvp)).ToList();
                    var messages = response.Messages.Select(m => new MessageViewModel(m)).ToList();

                    we.Result = new Tuple<List<LineGroupViewModel>, List<MessageViewModel>>(
                        lineGroups, messages);
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                if (we.Error == null && we?.Result != null)
                {
                    var tupleResult = (Tuple<List<LineGroupViewModel>, List<MessageViewModel>>)we.Result;
                    var lineGroups = new ObservableCollection<LineGroupViewModel>(tupleResult.Item1);
                    var messages = new ObservableCollection<MessageViewModel>(tupleResult.Item2);

                    onDataLoadedEventHandler?.Invoke(lineGroups, messages);
                    imAwait.Close();
                }
                else
                {
                    var lineGroups = new ObservableCollection<LineGroupViewModel>();
                    var messages = new ObservableCollection<MessageViewModel>();

                    onDataLoadedEventHandler?.Invoke(lineGroups, messages);
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
                    line.TransportType, line.Value, dateTime, route);
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
                if (we.Error == null && we?.Result != null && we.Result is LineDetails lineDetails)
                {
                    imAwait.Close();

                    if (lineDetails != null)
                    {
                        var lineDetailsViewModel = new LineDetailsViewModel(lineDetails);

                        onDataLoadedEventHandler?.Invoke(lineDetailsViewModel, line, isDataReload);
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
                if (string.IsNullOrEmpty(lineStop?.URL))
                {
                    we.Result = null;
                    return;
                }

                var downloader = new MpkCzestochowaDownloader.Downloaders.LineDeparturesDownloader();
                var request = new MpkCzestochowaDownloader.Data.Departures.LineDeparturesRequestModel(lineStop.URL);
                var response = downloader.DownloadData(request);

                if (response.HasData && !response.HasErrors)
                {
                    var lineDepartures = response.LineDepartures;

                    we.Result = lineDepartures;
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                if (we.Error == null && we?.Result != null && we.Result is LineDepartures lineDeaprtures)
                {
                    imAwait.Close();

                    if (lineDeaprtures != null)
                    {
                        var lineDeparturesViewModel = new LineDeparturesViewModel(lineDeaprtures);

                        onDataLoadedEventHandler.Invoke(lineDeparturesViewModel);
                    }
                    else
                        ShowDownloadingErrorMessage(
                            langConf.Messages.DownloadErrorTitle,
                            langConf.Messages.DeparturesViewPageDownloadErrorDesc);
                }
                else
                {
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
                var downloader = new LineArrivalsDownloader();
                var request = new LineArrivalsRequestModel(tripId, departureTime, departureDate);
                var response = downloader.DownloadData(request);

                if (!response.HasErrors && response.HasData)
                {
                    var lineArrivalsViewModel = new LineArrivalsViewModel(response.LineArrivals);

                    we.Result = lineArrivalsViewModel;
                }
                else
                {
                    we.Result = null;
                }
            };

            bgLoader.RunWorkerCompleted += (s, we) =>
            {
                if (we?.Result != null && we.Result is LineArrivalsViewModel lineArrivalsViewModel)
                {
                    imAwait.Close();

                    var imArrivals = new ArrivalsInternalMessage(imContainer, lineArrivalsViewModel);

                    imContainer.ShowMessage(imArrivals);
                }
            };

            //  Start downloading.
            imContainer.ShowMessage(imAwait);
            bgLoader.RunWorkerAsync();
        }

        #endregion LINES DATA

        #region UTILITIES

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

        #endregion UTILITIES

    }
}
