using chkam05.Tools.ControlsEx;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
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
using PublicTransportDataViewer.Components;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.Data.Config.Lang;
using PublicTransportDataViewer.Data.MainMenu;
using PublicTransportDataViewer.Pages.Settings;

namespace PublicTransportDataViewer.Pages
{
    public partial class SettingsPage : BasePage
    {

        //  VARIABLES

        private ObservableCollection<MainMenuItem> _internalMenuItems;


        //  GETTERS & SETTERS

        public override List<MainMenuItem> MainMenuItems
        {
            get => new List<MainMenuItem>()
            {
                new MainMenuItem(ConfigManager.Instance.LangConfig.Settings.MenuItemAppearance, PackIconKind.Palette, OnAppearanceMenuItemSelect,
                    ConfigManager.Instance.LangConfig.Settings.MenuItemAppearanceDesc),
                new MainMenuItem(ConfigManager.Instance.LangConfig.Settings.MenuItemGeneral, PackIconKind.Gear, OnGeneralMenuItemSelect,
                    ConfigManager.Instance.LangConfig.Settings.MenuItemGeneralDesc),
                new MainMenuItem(ConfigManager.Instance.LangConfig.Settings.MenuItemInfo, PackIconKind.InfoCircleOutline, OnInfoMenuItemSelect,
                    ConfigManager.Instance.LangConfig.Settings.MenuItemInfoDesc),
            };
        }

        public ObservableCollection<MainMenuItem> InternalMenuItems
        {
            get => _internalMenuItems;
            set
            {
                _internalMenuItems = value;
                _internalMenuItems.CollectionChanged += OnInternalMenuItemsCollectionChanged;
                OnPropertyChanged(nameof(InternalMenuItems));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> SettingsPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public SettingsPage(PagesController pagesController) : base(pagesController)
        {
            //  Initialize user interface.
            InitializeComponent();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Provides a mechanism for releasing unmanaged resources. </summary>
        public override void Dispose()
        {
            ConfigManager.Instance.LanguageUpdated -= OnLanguageUpdated;
        }

        #endregion CLASS METHODS

        #region MENU ITEMS INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking appearance menu item. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Event Arguments. </param>
        private void OnAppearanceMenuItemSelect(object? sender, EventArgs e)
        {
            _pagesController?.LoadPage(new AppearanceSettingsPage(_pagesController));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking info menu item. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Event Arguments. </param>
        private void OnGeneralMenuItemSelect(object? sender, EventArgs e)
        {
            _pagesController?.LoadPage(new GeneralSettingsPage(_pagesController));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking info menu item. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Event Arguments. </param>
        private void OnInfoMenuItemSelect(object? sender, EventArgs e)
        {
            _pagesController?.LoadPage(new InfoSettingsPage(_pagesController));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after clicking settings button. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsButton = sender as SettingsButton;

            if (settingsButton != null)
            {
                var menuItem = settingsButton.DataContext as MainMenuItem;

                if (menuItem != null)
                    menuItem.InvokeAction();
            }
        }

        #endregion MENU ITEMS INTERACTION METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after internal menu items collection changed. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        protected void OnInternalMenuItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(InternalMenuItems));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after updating language. </summary>
        /// <param name="languageConfig"> Loaded language configuration. </param>
        private void OnLanguageUpdated(LangConfig languageConfig)
        {
            _pagesController.ForceUpdateMainMenuItems(MainMenuItems);
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region PAGE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after loading page. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!ConfigManager.Instance.HasLanguageUpdatedRegisteredMethod(OnLanguageUpdated))
                ConfigManager.Instance.LanguageUpdated += OnLanguageUpdated;

            //  Setup internal menu.
            InternalMenuItems = new ObservableCollection<MainMenuItem>(MainMenuItems);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after unloading page. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BasePage_Unloaded(object sender, RoutedEventArgs e)
        {
            //
        }

        #endregion PAGE METHODS

    }
}
