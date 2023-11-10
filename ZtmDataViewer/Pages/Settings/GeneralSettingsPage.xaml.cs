using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace PublicTransportDataViewer.Pages.Settings
{
    public partial class GeneralSettingsPage : BasePage
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> GeneralSettingsPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public GeneralSettingsPage(PagesController pagesController) : base(pagesController)
        {
            //  Initialize user interface.
            InitializeComponent();
        }

        #endregion CLASS METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after updating language. </summary>
        /// <param name="languageConfig"> Loaded language configuration. </param>
        private void OnLanguageUpdated(LangConfig languageConfig)
        {
            _pagesController.ForceUpdateHeader(this.IconKind, this.Title);
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
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after unloading page. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BasePage_Unloaded(object sender, RoutedEventArgs e)
        {
            ConfigManager.Instance.LanguageUpdated -= OnLanguageUpdated;
            ConfigManager.Instance.SaveConfigurationToFile();
        }

        #endregion PAGE METHODS

    }
}
