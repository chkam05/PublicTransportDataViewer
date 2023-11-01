using chkam05.Tools.ControlsEx;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
using ZtmDataViewer.Components;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Data.MainMenu;
using ZtmDataViewer.Pages;

namespace ZtmDataViewer.Pages
{
    public partial class StartPage : BasePage
    {

        //  VARIABLES

        private ObservableCollection<MainMenuItem> _internalMenuItems;


        //  GETTERS & SETTERS

        public override List<MainMenuItem> MainMenuItems
        {
            get => new List<MainMenuItem>()
            {
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemSettings, PackIconKind.Gear, OnSettingsMenuItemSelect),
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
        /// <summary> StartPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public StartPage(PagesController pagesController) : base(pagesController)
        {
            //  Setup data.
            InternalMenuSetup();

            //  Initialize user interface.
            InitializeComponent();
        }

        #endregion CLASS METHODS

        #region INTERNAL MENU ITEMS MANAGEMENT METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after selecting internal menu item. </summary>
        /// <param name="sender"> Object from which method has been invoked. </param>
        /// <param name="e"> Selection Changed Event Arguments. </param>
        private void InternalMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListViewEx)sender;
            var selectedItem = listView.SelectedItem;

            if (selectedItem != null)
            {
                var menuItem = (MainMenuItem)selectedItem;

                if (menuItem != null)
                    menuItem.InvokeAction();

                listView.SelectedItem = null;
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Internal menu setup method. </summary>
        private void InternalMenuSetup()
        {
            InternalMenuItems = new ObservableCollection<MainMenuItem>()
            {
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemMpkCzestochowa, PackIconKind.Transportation, OnMpkCzestochowaMenuItemSelect),
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemZtm, PackIconKind.Transportation, OnZtmMenuItemSelect),
                new MainMenuItem(ConfigManager.Instance.LangConfig.MenuItemSettings, PackIconKind.Gear, OnSettingsMenuItemSelect)
            };
        }

        #endregion INTERNAL MENU ITEMS MANAGEMENT METHODS

        #region MENU ITEMS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after selecting mpk czestochowa menu item. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Event Arguments. </param>
        private void OnMpkCzestochowaMenuItemSelect(object? sender, EventArgs e)
        {
            _pagesController?.LoadPage(new MpkCzestochowa.LinesViewPage(_pagesController));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after selecting ztm menu item. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Event Arguments. </param>
        private void OnZtmMenuItemSelect(object? sender, EventArgs e)
        {
            _pagesController?.LoadPage(new ZtmData.LinesViewPage(_pagesController));
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
        /// <summary> Method invoked after internal menu items collection changed. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        protected void OnInternalMenuItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(InternalMenuItems));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
