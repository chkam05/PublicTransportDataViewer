using chkam05.Tools.ControlsEx.InternalMessages;
using chkam05.Tools.ControlsEx.WindowsEx;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;
using PublicTransportDataViewer.Components;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.Data.Info;
using PublicTransportDataViewer.Data.MainMenu;
using PublicTransportDataViewer.Events;
using PublicTransportDataViewer.Pages;
using PublicTransportDataViewer.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PublicTransportDataViewer.Windows
{
    public partial class MainWindow : WindowEx, INotifyPropertyChanged
    {

        //  DEPENDENCY PROPERTIES

        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register(
            nameof(HeaderContent),
            typeof(object),
            typeof(MainWindow),
            new PropertyMetadata(null));


        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  GETTERS & SETTERS

        public object HeaderContent
        {
            get => GetValue(HeaderContentProperty);
            set
            {
                SetValue(HeaderContentProperty, value);
                OnPropertyChanged(nameof(HeaderContent));
            }
        }

        public InternalMessagesExContainer InternalMessagesContainer
        {
            get => imContainer;
        }

        public PagesController PagesController
        {
            get => pagesController;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MainWindow class constructor. </summary>
        public MainWindow()
        {
            //  Initialize user interface components.
            InitializeComponent();

            //  Setup variables.
            Title = AppInfoManager.Instance.AppTitle;
        }

        #endregion CLASS METHODS

        #region NAVIGATION MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after pressing back button. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BackButtonEx_Click(object sender, RoutedEventArgs e)
        {
            if (pagesController.CanGoBack)
                pagesController.GoBack();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Load main menu items defined in page method. </summary>
        /// <param name="page"> Page. </param>
        private void OnPageLoaded(BasePage page)
        {
            //  Content update.
            HeaderContent = page.HeaderContent;

            //  Main menu items update.
            if (page?.MainMenuItems != null)
            {
                mainMenu.ClearItems();

                if (page.MainMenuItems.Any())
                    mainMenu.AddMenuItems(page.MainMenuItems);
            }

            //  Title update.
            titlePackIcon.Kind = page?.IconKind ?? PackIconKind.None;
            titleTextBlock.Text = page?.Title ?? string.Empty;
        }

        #endregion NAVIGATION MANAGEMENT METHODS

        #region PAGES MANAGER INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after force updating page header. </summary>
        /// <param name="icon"> Page header icon. </param>
        /// <param name="title"> Page header title. </param>
        private void PagesController_OnHeaderPageForceUpdate(PackIconKind icon, string title)
        {
            titlePackIcon.Kind = icon;
            titleTextBlock.Text = title;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after force updating main menu items. </summary>
        /// <param name="mainMenuItems"> List of main menu items. </param>
        private void PagesManager_OnMainMenuItemsForceUpdate(List<MainMenuItem> mainMenuItems)
        {
            if (mainMenuItems?.Any() ?? false)
            {
                mainMenu.ClearItems();
                mainMenu.AddMenuItems(mainMenuItems);
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after navigating to previous page. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Page Loaded Event Arguments. </param>
        private void PagesManager_OnPageBack(object sender, OnPageLoadedEventArgs e)
        {
            OnPageLoaded(e.NowLoadedPage);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after loading page. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Page Loaded Event Arguments. </param>
        private void PagesManager_OnPageNavigated(object sender, OnPageLoadedEventArgs e)
        {
            OnPageLoaded(e.NowLoadedPage);
        }

        #endregion PAGES MANAGER INTERACTION METHODS

        #region WINDOW METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after loading window. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void WindowEx_Loaded(object sender, RoutedEventArgs e)
        {
            var configManager = ConfigManager.Instance;

            //  Load window size and position.
            Left = configManager.ConfigData.WindowPosition.X;
            Top = configManager.ConfigData.WindowPosition.Y;
            Height = configManager.ConfigData.WindowSize.Height;
            Width = configManager.ConfigData.WindowSize.Width;

            //  Fix position on screen.
            var screen = WindowsHelper.GetScreenWhereIsWindow(this);

            if (screen != null)
                WindowsHelper.AdjustWindowToScreen(this, screen);
            else
                WindowsHelper.AdjustWindowToPrimaryScreen(this);

            //  Load home page.
            PagesController.LoadPage(new StartPage(PagesController));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked before closing window. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Cancel Event Arguments. </param>
        private void WindowEx_Closed(object sender, EventArgs e)
        {
            var configManager = ConfigManager.Instance;

            //  Save window size and position.
            configManager.ConfigData.WindowPosition = new Point(Left, Top);
            configManager.ConfigData.WindowSize = new Size(Width, Height);
        }

        #endregion WINDOW METHODS

    }
}
