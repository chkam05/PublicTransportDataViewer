using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace chkam05.ZtmDataViewer
{
    public partial class App : Application
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Application class constructor. </summary>
        public App() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region APPLICATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Mathod invoked when application is starting up. </summary>
        /// <param name="e"> Application startup event arguments. </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //  Startup application.
            base.OnStartup(e);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked when application is shutting down. </summary>
        /// <param name="e"> Application exit event arguments. </param>
        protected override void OnExit(ExitEventArgs e)
        {
            //  Close application.
            base.OnExit(e);
        }

        #endregion APPLICATION METHODS

    }
}
