using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PublicTransportDataViewer.Pages.Settings
{
    public partial class InfoSettingsPage : BasePage
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> InfoSettingsPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public InfoSettingsPage(PagesController pagesController) : base(pagesController)
        {
            //  Initialize user interface.
            InitializeComponent();
        }

        #endregion CLASS METHODS

        #region DATA INTERNACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after double clicing on sources list. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Mouse Button Event Arguments. </param>
        private void SourcesListViewEx_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement source)
            {
                if (source.DataContext is KeyValuePair<string,string> sourceData)
                {
                    var processStartInfo = new ProcessStartInfo()
                    {
                        FileName = sourceData.Value,
                        UseShellExecute = true
                    };

                    Process.Start(processStartInfo);
                }
            }
        }

        #endregion DATA INTERACTION METHODS

        #region PAGE METHODS

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
