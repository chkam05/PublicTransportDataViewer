using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PublicTransportDataViewer.Data.Config;

namespace PublicTransportDataViewer.Data.Info
{
    public class AppInfoManager
    {

        //  VARIABLES

        private static AppInfoManager? _instance = null;
        private static object _instanceLock = new object();

        private string? _appCompany;
        private string? _appCopyright;
        private string? _appDescription;
        private string? _appLocation;
        private string? _appName;
        private string? _appPath;
        private string? _appTitle;
        private string? _appVersion;

        private Dictionary<string, string> _dataSources;


        //  GETTERS

        public static AppInfoManager Instance
        {
            get => GetInstance();
        }

        public string AppCompany
        {
            get
            {
                if (string.IsNullOrEmpty(_appCompany))
                    _appCompany = GetAppCompany();
                return _appCompany;
            }
        }

        public string AppCopyright
        {
            get
            {
                if (string.IsNullOrEmpty(_appCopyright))
                    _appCopyright = GetAppCopyright();
                return _appCopyright;
            }
        }

        public string AppDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_appDescription))
                    _appDescription = GetAppDescription();
                return _appDescription;
            }
        }

        public string AppLocation
        {
            get
            {
                if (string.IsNullOrEmpty(_appLocation))
                    _appLocation = GetAppLocation();
                return _appLocation;
            }
        }

        public string AppName
        {
            get
            {
                if (string.IsNullOrEmpty(_appName))
                    _appName = GetAppName();
                return _appName;
            }
        }

        public string AppPath
        {
            get
            {
                if (string.IsNullOrEmpty(_appPath))
                    _appPath = GetAppPath();
                return _appPath;
            }
        }

        public string AppTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_appTitle))
                    _appTitle = GetAppTitle();
                return _appTitle;
            }
        }

        public string AppVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_appVersion))
                    _appVersion = GetAppVersion();
                return _appVersion;
            }
        }

        public Dictionary<string, string> DataSources
        {
            get
            {
                if (_dataSources == null)
                    _dataSources = GetDataSources();
                return _dataSources;
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> AppInfoManager class constructor. </summary>
        public AppInfoManager()
        {
            //
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get AppInfoManager class singleton instance. </summary>
        /// <returns> AppInfoManager class singleton instance. </returns>
        private static AppInfoManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppInfoManager();
                    }
                }
            }

            return _instance;
        }

        #endregion CLASS METHODS

        #region INFORMATION GETTERS

        //  --------------------------------------------------------------------------------
        /// <summary> Get application assembly company. </summary>
        /// <returns> Application assembly company. </returns>
        private static string GetAppCompany()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attributes = (AssemblyCompanyAttribute[]) assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);

            return attributes.Length > 0
                ? attributes.FirstOrDefault()?.Company ?? string.Empty
                : string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get application assembly copyright. </summary>
        /// <returns> Application assembly copyright. </returns>
        private static string GetAppCopyright()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attributes = (AssemblyCopyrightAttribute[]) assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);

            return attributes.Length > 0
                ? attributes.FirstOrDefault()?.Copyright ?? string.Empty
                : string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get application assembly name. </summary>
        /// <returns> Application assembly name. </returns>
        private static string GetAppDescription()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attributes = (AssemblyDescriptionAttribute[]) assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);

            return attributes.Length > 0
                ? attributes.FirstOrDefault()?.Description ?? string.Empty
                : string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get application executable file location path. </summary>
        /// <returns> Application executable file path. </returns>
        private static string GetAppLocation()
        {
            return Assembly.GetEntryAssembly()?.Location ?? string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get application assembly name. </summary>
        /// <returns> Application assembly name. </returns>
        private string GetAppName()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            return assemblyName?.Name ?? string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get application executable file directory path. </summary>
        /// <returns> Application executable file location path. </returns>
        private static string GetAppPath()
        {
            var location = GetAppLocation();

            return !string.IsNullOrEmpty(location)
                ? Path.GetDirectoryName(location) ?? string.Empty
                : string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get application assembly title. </summary>
        /// <returns> Application assembly title. </returns>
        private static string GetAppTitle()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attributes = (AssemblyTitleAttribute[]) assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);

            return attributes.Length > 0
                ? attributes.FirstOrDefault()?.Title ?? string.Empty
                : string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get application verion. </summary>
        /// <returns> Application version. </returns>
        private static string GetAppVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();

            return assemblyName?.Version?.ToString() ?? string.Empty;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get data sources. </summary>
        /// <returns> Data sources dictionary. </returns>
        private static Dictionary<string, string> GetDataSources()
        {
            return new Dictionary<string, string>()
            {
                { "Mpk Częstochowa: ", MpkCzestochowaDownloader.Data.Static.StaticConfig.LinesURL },
                { "Zarząd Transportu Metropolitalnego: ", ZtmDataDownloader.Data.Static.StaticConfig.ZtmLinesURL },
            };
        }

        #endregion INFORMATION GETTERS

    }
}
