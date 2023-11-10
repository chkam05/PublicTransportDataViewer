using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PublicTransportDataViewer.Data.Config;
using PublicTransportDataViewer.Data.Info;

namespace PublicTransportDataViewer
{
    public partial class App : Application
    {

        //  VARIABLES

        public AppInfoManager AppInfoManager { get; private set; }
        public ConfigManager ConfigManager { get; private set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> App class constructor. </summary>
        public App() : base()
        {
            //  Initialize modules.
            AppInfoManager = AppInfoManager.Instance;
            ConfigManager = ConfigManager.Instance;
        }

        #endregion CLASS METHODS

        #region APPLICATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Windows application startup event. </summary>
        /// <param name="e"> Startup Event Arguments. </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Windows application exit event. </summary>
        /// <param name="e"> Exit Event Arguments. </param>
        protected override void OnExit(ExitEventArgs e)
        {
            //  Dispose modules.
            ConfigManager.Dispose();

            base.OnExit(e);
        }

        #endregion APPLICATION METHODS

    }
}
