﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.Data.MainMenu;
using PublicTransportDataViewer.Events;
using PublicTransportDataViewer.Pages;
using static PublicTransportDataViewer.Data.Static.CustomEvents;
using static PublicTransportDataViewer.Events.EventsDefinitions;

namespace PublicTransportDataViewer.Components
{
    public partial class PagesController : UserControl, INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler? PropertyChanged;
        public event HeaderPageForceUpdateEventHandler? OnHeaderPageForceUpdate;
        public event MainMenuItemsForceUpdateEventHandler? OnMainMenuItemsForceUpdate;
        public event PagesManagerNavigatedEventHandler OnPageBack;
        public event PagesManagerNavigatedEventHandler OnPageNavigated;


        //  VARIABLES

        private List<BasePage> _loadedPages;


        //  GETTERS & SETTERS

        public bool CanGoBack
        {
            get => _loadedPages.Any() && _loadedPages.IndexOf(LoadedPage) > 0;
        }

        public BasePage? LoadedPage
        {
            get => _loadedPages?.LastOrDefault();
        }

        public int PagesCount
        {
            get => _loadedPages.Count;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> PagesController class constructor. </summary>
        public PagesController()
        {
            //  Setup data containers.
            _loadedPages = new List<BasePage>();

            //  Initialize user interface components.
            InitializeComponent();
        }

        #endregion CLASS METHODS

        #region CONTENT FRAME METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Remove all loaded pages from history method. </summary>
        private void ClearAllContent()
        {
            _loadedPages.Clear();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Remove currently loaded page from content frame method. </summary>
        private void ClearCurrentContent()
        {
            contentFrame.Content = null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Metod invoked after loading page in frame method. </summary>
        /// <param name="sender"> Object that invoked event. </param>
        /// <param name="e"> Navigated Event Arguments. </param>
        private void contentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //  Remove previous pages from content frame back entry.
            RemoveBackEntry();

            //  Update attributes
            OnPropertyChanged(nameof(CanGoBack));
            OnPropertyChanged(nameof(LoadedPage));
            OnPropertyChanged(nameof(PagesCount));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Remove previous pages from content fram back entry method. </summary>
        private void RemoveBackEntry()
        {
            //  Get previous pages from content frame navigation service.
            var backEntry = contentFrame.NavigationService.RemoveBackEntry();

            //  While previous pages are available - try to remove it.
            while (backEntry != null)
                backEntry = contentFrame.NavigationService.RemoveBackEntry();
        }

        #endregion CONTENT FRAME METHODS

        #region NAVIGATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Go to previous page/pages by certain number of steps method. </summary>
        /// <param name="backCount"> Number of steps back (default 1). </param>
        /// <param name="force"> Force back to previous page. </param>
        public void GoBack(int backCount = 1, bool force = false)
        {
            if (CanGoBack)
            {
                var currPage = LoadedPage;

                var currPageIndex = _loadedPages.IndexOf(currPage);
                var destPageIndex = Math.Max(0, currPageIndex - backCount);

                //  Get previous page from list to load into content frame.
                var destPage = _loadedPages[destPageIndex];

                if (currPage != null && !force && !currPage.OnGoBackFromPage(destPage))
                    return;

                //  Remove other pages loaded further.
                _loadedPages.RemoveRange(destPageIndex + 1, PagesCount - (destPageIndex + 1));

                //  Load previous page and update current page index.
                contentFrame.Navigate(destPage);

                //  Invoke external event.
                var args = new OnPageLoadedEventArgs(destPage, currPage);
                OnPageBack?.Invoke(this, args);

                OnPropertyChanged(nameof(CanGoBack));
                OnPropertyChanged(nameof(LoadedPage));
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Navigate to newly created page method. </summary>
        /// <param name="page"> Page to load. </param>
        /// <param name="preventLoadSamePage"> Prevent to load page with the same type. </param>
        /// <param name="force"> Force load page. </param>
        public void LoadPage(BasePage page, bool preventLoadSamePage = true, bool force = false)
        {
            var pageToLoad = page as Page;

            if (pageToLoad != null)
            {
                var currPage = LoadedPage;

                if (preventLoadSamePage && currPage != null && currPage.GetType() == page.GetType())
                    return;

                if (currPage != null && !force && !currPage.OnGoForwardFromPage(page))
                    return;

                //  Add page to history.
                _loadedPages.Add(page);

                //  Load page.
                contentFrame.Navigate(pageToLoad);

                //  Invoke external event.
                var args = new OnPageLoadedEventArgs(page, currPage);
                OnPageNavigated?.Invoke(this, args);

                OnPropertyChanged(nameof(CanGoBack));
                OnPropertyChanged(nameof(LoadedPage));
            }
        }

        #endregion NAVIGATION METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method for invoking PropertyChangedEventHandler external method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region PAGES MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Check if page is already loaded. </summary>
        /// <param name="page"> Page to check. </param>
        /// <returns> True - pages is already loaded; False - otherwise. </returns>
        public bool HasPage(BasePage page)
        {
            return _loadedPages.Contains(page);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Check if page with specified type is already loaded. </summary>
        /// <param name="pageType"> Page type to check. </param>
        /// <returns> True - page with this type is already loaded; False - otherwise. </returns>
        public bool HasPage(Type pageType)
        {
            return _loadedPages.Any(p => pageType.IsAssignableFrom(((BasePage)p).GetType()));
        }

        #endregion PAGES MANAGEMENT METHODS

        #region UPDATE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Force update current page state. </summary>
        public void ForceUpdate()
        {
            var args = new OnPageLoadedEventArgs(LoadedPage, LoadedPage);
            OnPageNavigated?.Invoke(this, args);
            OnPropertyChanged(nameof(LoadedPage));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Force update page header. </summary>
        /// <param name="icon"> Header icon. </param>
        /// <param name="title"> Header title. </param>
        public void ForceUpdateHeader(PackIconKind? icon = null, string? title = null)
        {
            if (icon.HasValue && !string.IsNullOrEmpty(title))
                OnHeaderPageForceUpdate?.Invoke(icon.Value, title);

            else if (LoadedPage != null)
                OnHeaderPageForceUpdate?.Invoke(LoadedPage.IconKind, LoadedPage.Title);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Force update main menu items. </summary>
        /// <param name="mainMenuItems"> List of main menu items. </param>
        public void ForceUpdateMainMenuItems(List<MainMenuItem> mainMenuItems)
        {
            OnMainMenuItemsForceUpdate?.Invoke(mainMenuItems);
        }

        #endregion UPDATE METHODS

    }
}
