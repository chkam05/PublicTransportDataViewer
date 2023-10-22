using chkam05.Tools.ControlsEx;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using ZtmDataViewer.Data.Config.Lang;
using ZtmDataViewer.Data.MainMenu;
using ZtmDataViewer.Pages.Settings;

namespace ZtmDataViewer.Pages
{
    public partial class SettingsPage : BasePage
    {

        //  VARIABLES

        private LangConfig _langConfig = ConfigManager.Instance.LangConfig;


        //  GETTERS & SETTERS

        public override List<MainMenuItem> MainMenuItems
        {
            get => new List<MainMenuItem>()
            {
                new MainMenuItem(_langConfig.Settings.SettingsPageAppearanceMenuItem, PackIconKind.Palette, OnAppearanceMenuItemSelect, _langConfig.Settings.SettingsPageAppearanceMenuItemDesc),
                new MainMenuItem(_langConfig.Settings.SettingsPageGeneralMenuItem, PackIconKind.Gear, OnGeneralMenuItemSelect, _langConfig.Settings.SettingsPageGeneralMenuItemDesc),
                new MainMenuItem(_langConfig.Settings.SettingsPageInfoMenuItem, PackIconKind.InfoCircleOutline, OnInfoMenuItemSelect, _langConfig.Settings.SettingsPageInfoMenuItemDesc),
            };
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

    }
}
