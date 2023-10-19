using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config
{
    public class LangConfig : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  ATTRIBUTES

        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        #region MainMenu

        //  VARIABLES

        private string _mainMenuItem = string.Empty;


        //  GETTERS & SETTERS

        public string MainMenuItem
        {
            get => _mainMenuItem;
            set => SetStringProperty(ref _mainMenuItem, nameof(MainMenuItem), value);
        }

        #endregion MainMenu

        #region StartPage

        //  VARIABLES

        private string _startPageTitle = string.Empty;
        private string _startPageMpkCzestochowaItem = string.Empty;
        private string _startPageZtmMenuItem = string.Empty;
        private string _startPageSettingsMenuItem = string.Empty;


        //  GETTERS & SETTERS

        public string StartPageTitle
        {
            get => _startPageTitle;
            set => SetStringProperty(ref _startPageTitle, nameof(StartPageTitle), value);
        }
        
        public string StartPageMpkCzestochowaItem
        {
            get => _startPageMpkCzestochowaItem;
            set => SetStringProperty(ref _startPageMpkCzestochowaItem, nameof(StartPageMpkCzestochowaItem), value);
        }

        public string StartPageZtmMenuItem
        {
            get => _startPageZtmMenuItem;
            set => SetStringProperty(ref _startPageZtmMenuItem, nameof(StartPageZtmMenuItem), value);
        }

        public string StartPageSettingsMenuItem
        {
            get => _startPageSettingsMenuItem;
            set => SetStringProperty(ref _startPageSettingsMenuItem, nameof(StartPageSettingsMenuItem), value);
        }

        #endregion StartPage

        #region SettingsPage

        //  VARIABLES

        private string _settingsPageTitle = string.Empty;
        private string _settingsPageAppearanceMenuItem = string.Empty;
        private string _settingsPageAppearanceMenuItemDesc = string.Empty;
        private string _settingsPageGeneralMenuItem = string.Empty;
        private string _settingsPageGeneralMenuItemDesc = string.Empty;
        private string _settingsPageInfoMenuItem = string.Empty;
        private string _settingsPageInfoMenuItemDesc = string.Empty;


        //  GETTERS & SETTERS

        public string SettingsPageTitle
        {
            get => _settingsPageTitle;
            set => SetStringProperty(ref _settingsPageTitle, nameof(SettingsPageTitle), value);
        }

        public string SettingsPageAppearanceMenuItem
        {
            get => _settingsPageAppearanceMenuItem;
            set => SetStringProperty(ref _settingsPageAppearanceMenuItem, nameof(SettingsPageAppearanceMenuItem), value);
        }

        public string SettingsPageAppearanceMenuItemDesc
        {
            get => _settingsPageAppearanceMenuItemDesc;
            set => SetStringProperty(ref _settingsPageAppearanceMenuItemDesc, nameof(SettingsPageAppearanceMenuItemDesc), value);
        }

        public string SettingsPageGeneralMenuItem
        {
            get => _settingsPageGeneralMenuItem;
            set => SetStringProperty(ref _settingsPageGeneralMenuItem, nameof(SettingsPageGeneralMenuItem), value);
        }

        public string SettingsPageGeneralMenuItemDesc
        {
            get => _settingsPageGeneralMenuItemDesc;
            set => SetStringProperty(ref _settingsPageGeneralMenuItemDesc, nameof(SettingsPageGeneralMenuItemDesc), value);
        }

        public string SettingsPageInfoMenuItem
        {
            get => _settingsPageInfoMenuItem;
            set => SetStringProperty(ref _settingsPageInfoMenuItem, nameof(SettingsPageInfoMenuItem), value);
        }

        public string SettingsPageInfoMenuItemDesc
        {
            get => _settingsPageInfoMenuItemDesc;
            set => SetStringProperty(ref _settingsPageInfoMenuItemDesc, nameof(SettingsPageAppearanceMenuItemDesc), value);
        }

        #endregion SettingsPage

        #region AppearanceSettingsPage

        //  VARIABLES

        private string _settingsAppearancePageTitle = string.Empty;
        private string _settingsAppearanceTheme = string.Empty;
        private string _settingsAppearanceThemeDesc = string.Empty;
        private string _settingsAppearanceThemeDark = string.Empty;
        private string _settingsAppearanceThemeLight = string.Empty;
        private string _settingsAppearnaceAccentColors = string.Empty;
        private string _settingsAppearanceAccentColorsHistory = string.Empty;


        //  GETTERS & SETTERS

        public string SettingsAppearancePageTitle
        {
            get => _settingsAppearancePageTitle;
            set => SetStringProperty(ref _settingsAppearancePageTitle, nameof(SettingsAppearancePageTitle), value);
        }

        public string SettingsAppearanceTheme
        {
            get => _settingsAppearanceTheme;
            set => SetStringProperty(ref _settingsAppearanceTheme, nameof(SettingsAppearanceTheme), value);
        }

        public string SettingsAppearanceThemeDesc
        {
            get => _settingsAppearanceThemeDesc;
            set => SetStringProperty(ref _settingsAppearanceThemeDesc, nameof(SettingsAppearanceThemeDesc), value);
        }

        public string SettingsAppearanceThemeDark
        {
            get => _settingsAppearanceThemeDark;
            set => SetStringProperty(ref _settingsAppearanceThemeDark, nameof(SettingsAppearanceThemeDark), value);
        }

        public string SettingsAppearanceThemeLight
        {
            get => _settingsAppearanceThemeLight;
            set => SetStringProperty(ref _settingsAppearanceThemeLight, nameof(SettingsAppearanceThemeLight), value);
        }

        public string SettingsAppearnaceAccentColors
        {
            get => _settingsAppearnaceAccentColors;
            set => SetStringProperty(ref _settingsAppearnaceAccentColors, nameof(SettingsAppearnaceAccentColors), value);
        }

        public string SettingsAppearanceAccentColorsHistory
        {
            get => _settingsAppearanceAccentColorsHistory;
            set => SetStringProperty(ref _settingsAppearanceAccentColorsHistory, nameof(SettingsAppearanceAccentColorsHistory), value);
        }

        #endregion AppearanceSettingsPage

        #region GeneralSettingsPage

        //  VARIABLES

        private string _settingsGeneralPageTitle = string.Empty;
        private string _settingsGeneralPageLanguage = string.Empty;
        private string _settingsGeneralPageLanguageDesc = string.Empty;


        //  GETTERS & SETTERS

        public string SettingsGeneralPageTitle
        {
            get => _settingsGeneralPageTitle;
            set => SetStringProperty(ref _settingsGeneralPageTitle, nameof(SettingsGeneralPageTitle), value);
        }

        public string SettingsGeneralPageLanguage
        {
            get => _settingsGeneralPageLanguage;
            set => SetStringProperty(ref _settingsGeneralPageLanguage, nameof(SettingsGeneralPageLanguage), value);
        }

        public string SettingsGeneralPageLanguageDesc
        {
            get => _settingsGeneralPageLanguageDesc;
            set => SetStringProperty(ref _settingsGeneralPageLanguageDesc, nameof(SettingsGeneralPageLanguageDesc), value);
        }

        #endregion GeneralSettingsPage

        #region InfoSettingsPage

        //  VARIABLES

        private string _settingsInfoPageTitle = string.Empty;
        private string _settingsInfoPageDescription = string.Empty;
        private string _settingsInfoPageAuthor = string.Empty;
        private string _settingsInfoPageCopyright = string.Empty;
        private string _settingsInfoPageLocation = string.Empty;


        //  GETTERS & SETTERS

        public string SettingsInfoPageTitle
        {
            get => _settingsInfoPageTitle;
            set => SetStringProperty(ref _settingsInfoPageTitle, nameof(SettingsInfoPageTitle), value);
        }

        public string SettingsInfoPageDescription
        {
            get => _settingsInfoPageDescription;
            set => SetStringProperty(ref _settingsInfoPageDescription, nameof(SettingsInfoPageDescription), value);
        }

        public string SettingsInfoPageAuthor
        {
            get => _settingsInfoPageAuthor;
            set => SetStringProperty(ref _settingsInfoPageAuthor, nameof(SettingsInfoPageAuthor), value);
        }

        public string SettingsInfoPageCopyright
        {
            get => _settingsInfoPageCopyright;
            set => SetStringProperty(ref _settingsInfoPageCopyright, nameof(SettingsInfoPageCopyright), value);
        }

        public string SettingsInfoPageLocation
        {
            get => _settingsInfoPageLocation;
            set => SetStringProperty(ref _settingsInfoPageLocation, nameof(SettingsInfoPageLocation), value);
        }

        #endregion InfoSettingsPage

        #region MpkCzestochowaLinesViewPage

        //  VARIABLES

        private string _mpkCzestochowaMessageDateTitle = string.Empty;
        private string _mpkCzestochowaMessageLinesTitle = string.Empty;


        //  GETTERS & SETTERS

        public string MpkCzestochowaMessageDateTitle
        {
            get => _mpkCzestochowaMessageDateTitle;
            set => SetStringProperty(ref _mpkCzestochowaMessageDateTitle, nameof(MpkCzestochowaMessageDateTitle), value);
        }

        public string MpkCzestochowaMessageLinesTitle
        {
            get => _mpkCzestochowaMessageLinesTitle;
            set => SetStringProperty(ref _mpkCzestochowaMessageLinesTitle, nameof(MpkCzestochowaMessageLinesTitle), value);
        }

        #endregion MpkCzestochowaLinesViewPage

        #region MpkCzestochowaTransportTypes

        //  VARIABLES

        private string _mpkCzestochowaTransportTypeBus = string.Empty;
        private string _mpkCzestochowaTransportTypeBusNight = string.Empty;
        private string _mpkCzestochowaTransportTypeBusSuburban = string.Empty;
        private string _mpkCzestochowaTransportTypeTram = string.Empty;


        //  GETTERS & SETTERS

        public string MpkCzestochowaTransportTypeBus
        {
            get => _mpkCzestochowaTransportTypeBus;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeBus, nameof(MpkCzestochowaTransportTypeBus), value);
        }

        public string MpkCzestochowaTransportTypeBusNight
        {
            get => _mpkCzestochowaTransportTypeBusNight;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeBusNight, nameof(MpkCzestochowaTransportTypeBusNight), value);
        }

        public string MpkCzestochowaTransportTypeBusSuburban
        {
            get => _mpkCzestochowaTransportTypeBusSuburban;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeBusSuburban, nameof(MpkCzestochowaTransportTypeBusSuburban), value);
        }

        public string MpkCzestochowaTransportTypeTram
        {
            get => _mpkCzestochowaTransportTypeTram;
            set => SetStringProperty(ref _mpkCzestochowaTransportTypeTram, nameof(MpkCzestochowaTransportTypeTram), value);
        }

        #endregion MpkCzestochowaTransportTypes

        #region ZtmArrivalsIM

        //  VARIABLES

        private string _ztmArrivalsLoadingTitle = string.Empty;
        private string _ztmArrivalsLoadingDesc = string.Empty;
        private string _ztmArrivalsDownloadErrorTitle = string.Empty;
        private string _ztmArrivalsDownloadErrorDesc = string.Empty;
        private string _ztmArrivalsTitle = string.Empty;
        private string _ztmArrivalsCancelButton = string.Empty;
        private string _ztmArrivalStopHeader = string.Empty;
        private string _ztmArrivalDepartureHeader = string.Empty;
        private string _ztmArrivalTripTimeHeader = string.Empty;
        private string _ztmArrivalTripDistanceHeader = string.Empty;


        //  METHODS

        public string ZtmArrivalsLoadingTitle
        {
            get => _ztmArrivalsLoadingTitle;
            set => SetStringProperty(ref _ztmArrivalsLoadingTitle, nameof(ZtmArrivalsLoadingTitle), value);
        }

        public string ZtmArrivalsLoadingDesc
        {
            get => _ztmArrivalsLoadingDesc;
            set => SetStringProperty(ref _ztmArrivalsLoadingDesc, nameof(ZtmArrivalsLoadingDesc), value);
        }

        public string ZtmArrivalsDownloadErrorTitle
        {
            get => _ztmArrivalsDownloadErrorTitle;
            set => SetStringProperty(ref _ztmArrivalsDownloadErrorTitle, nameof(ZtmArrivalsDownloadErrorTitle), value);
        }

        public string ZtmArrivalsDownloadErrorDesc
        {
            get => _ztmArrivalsDownloadErrorDesc;
            set => SetStringProperty(ref _ztmArrivalsDownloadErrorDesc, nameof(ZtmArrivalsDownloadErrorDesc), value);
        }

        public string ZtmArrivalsTitle
        {
            get => _ztmArrivalsTitle;
            set => SetStringProperty(ref _ztmArrivalsTitle, nameof(ZtmArrivalsTitle), value);
        }

        public string ZtmArrivalsCancelButton
        {
            get => _ztmArrivalsCancelButton;
            set => SetStringProperty(ref _ztmArrivalsCancelButton, nameof(ZtmArrivalsCancelButton), value);
        }

        public string ZtmArrivalStopHeader
        {
            get => _ztmArrivalStopHeader;
            set => SetStringProperty(ref _ztmArrivalStopHeader, nameof(ZtmArrivalStopHeader), value);
        }

        public string ZtmArrivalDepartureHeader
        {
            get => _ztmArrivalDepartureHeader;
            set => SetStringProperty(ref _ztmArrivalDepartureHeader, nameof(ZtmArrivalDepartureHeader), value);
        }

        public string ZtmArrivalTripTimeHeader
        {
            get => _ztmArrivalTripTimeHeader;
            set => SetStringProperty(ref _ztmArrivalTripTimeHeader, nameof(ZtmArrivalTripTimeHeader), value);
        }

        public string ZtmArrivalTripDistanceHeader
        {
            get => _ztmArrivalTripDistanceHeader;
            set => SetStringProperty(ref _ztmArrivalTripDistanceHeader, nameof(ZtmArrivalTripDistanceHeader), value);
        }

        #endregion ZtmArrivalsIM

        #region ZtmDeparturesView

        //  VARIABLES

        private string _ztmDeparturesViewLoadingTitle = string.Empty;
        private string _ztmDeparturesViewLoadingDesc = string.Empty;
        private string _ztmDeparturesViewDownloadErrorTitle = string.Empty;
        private string _ztmDeparturesViewDownloadErrorDesc = string.Empty;


        //  METHODS

        public string ZtmDeparturesViewLoadingTitle
        {
            get => _ztmDeparturesViewLoadingTitle;
            set => SetStringProperty(ref _ztmDeparturesViewLoadingTitle, nameof(ZtmDeparturesViewLoadingTitle), value);
        }

        public string ZtmDeparturesViewLoadingDesc
        {
            get => _ztmDeparturesViewLoadingDesc;
            set => SetStringProperty(ref _ztmDeparturesViewLoadingDesc, nameof(_ztmDeparturesViewLoadingDesc), value);
        }

        public string ZtmDeparturesViewDownloadErrorTitle
        {
            get => _ztmDeparturesViewDownloadErrorTitle;
            set => SetStringProperty(ref _ztmDeparturesViewDownloadErrorTitle, nameof(ZtmDeparturesViewDownloadErrorTitle), value);
        }

        public string ZtmDeparturesViewDownloadErrorDesc
        {
            get => _ztmDeparturesViewDownloadErrorDesc;
            set => SetStringProperty(ref _ztmDeparturesViewDownloadErrorDesc, nameof(ZtmDeparturesViewDownloadErrorDesc), value);
        }

        #endregion ZtmDeparturesView

        #region ZtmLineDetailsViewPage

        //  VARIABLES

        private string _ztmLineDetailsViewLoadingTitle = string.Empty;
        private string _ztmLineDetailsViewLoadingDesc = string.Empty;
        private string _ztmLineDetailsViewPageDirection = string.Empty;
        private string _ztmLineDetailsViewPageMessages = string.Empty;
        private string _ztmLineDetailsViewPageDepartures = string.Empty;
        private string _ztmLineDetailsViewDownloadErrorTitle = string.Empty;
        private string _ztmLineDetailsViewDownloadErrorDesc = string.Empty;
        private string _ztmLineDetailsViewRefreshButton = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmLineDetailsViewLoadingTitle
        {
            get => _ztmLineDetailsViewLoadingTitle;
            set => SetStringProperty(ref _ztmLineDetailsViewLoadingTitle, nameof(ZtmLineDetailsViewLoadingTitle), value);
        }

        public string ZtmLineDetailsViewLoadingDesc
        {
            get => _ztmLineDetailsViewLoadingDesc;
            set => SetStringProperty(ref _ztmLineDetailsViewLoadingDesc, nameof(ZtmLineDetailsViewLoadingDesc), value);
        }

        public string ZtmLineDetailsViewPageDirection
        {
            get => _ztmLineDetailsViewPageDirection;
            set => SetStringProperty(ref _ztmLineDetailsViewPageDirection, nameof(ZtmLineDetailsViewPageDirection), value);
        }

        public string ZtmLineDetailsViewPageMessages
        {
            get => _ztmLineDetailsViewPageMessages;
            set => SetStringProperty(ref _ztmLineDetailsViewPageMessages, nameof(ZtmLineDetailsViewPageMessages), value);
        }

        public string ZtmLineDetailsViewPageDepartures
        {
            get => _ztmLineDetailsViewPageDepartures;
            set => SetStringProperty(ref _ztmLineDetailsViewPageDepartures, nameof(ZtmLineDetailsViewPageDepartures), value);
        }

        public string ZtmLineDetailsViewDownloadErrorTitle
        {
            get => _ztmLineDetailsViewDownloadErrorTitle;
            set => SetStringProperty(ref _ztmLineDetailsViewDownloadErrorTitle, nameof(ZtmLineDetailsViewDownloadErrorTitle), value);
        }

        public string ZtmLineDetailsViewDownloadErrorDesc
        {
            get => _ztmLineDetailsViewDownloadErrorDesc;
            set => SetStringProperty(ref _ztmLineDetailsViewDownloadErrorDesc, nameof(ZtmLineDetailsViewDownloadErrorDesc), value);
        }

        #endregion ZtmLineDetailsViewPage

        #region ZtmLinesViewPage

        //  VARIABLES

        private string _ztmLineViewPageTitle = string.Empty;
        private string _ztmLineViewLinesMenuItem = string.Empty;
        private string _ztmLineViewLoadingTitle = string.Empty;
        private string _ztmLineViewLoadingDesc = string.Empty;
        private string _ztmLineViewRefreshButton = string.Empty;
        private string _ztmLineViewDownloadErrorTitle = string.Empty;
        private string _ztmLineViewDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmLineViewPageTitle
        {
            get => _ztmLineViewPageTitle;
            set => SetStringProperty(ref _ztmLineViewPageTitle, nameof(ZtmLineViewPageTitle), value);
        }

        public string ZtmLineViewLinesMenuItem
        {
            get => _ztmLineViewLinesMenuItem;
            set => SetStringProperty(ref _ztmLineViewLinesMenuItem, nameof(ZtmLineViewLinesMenuItem), value);
        }

        public string ZtmLineViewLoadingTitle
        {
            get => _ztmLineViewLoadingTitle;
            set => SetStringProperty(ref _ztmLineViewLoadingTitle, nameof(ZtmLineViewLoadingTitle), value);
        }

        public string ZtmLineViewLoadingDesc
        {
            get => _ztmLineViewLoadingDesc;
            set => SetStringProperty(ref _ztmLineViewLoadingDesc, nameof(ZtmLineViewLoadingDesc), value);
        }

        public string ZtmLineViewRefreshButton
        {
            get => _ztmLineViewRefreshButton;
            set => SetStringProperty(ref _ztmLineViewRefreshButton, nameof(ZtmLineViewRefreshButton), value);
        }

        public string ZtmLineViewDownloadErrorTitle
        {
            get => _ztmLineViewDownloadErrorTitle;
            set => SetStringProperty(ref _ztmLineViewDownloadErrorTitle, nameof(ZtmLineViewDownloadErrorTitle), value);
        }

        public string ZtmLineViewDownloadErrorDesc
        {
            get => _ztmLineViewDownloadErrorDesc;
            set => SetStringProperty(ref _ztmLineViewDownloadErrorDesc, nameof(ZtmLineViewDownloadErrorDesc), value);
        }

        #endregion ZtmDataLinesViewPage

        #region ZtmTimeTableSelectionIM

        //  VARIABLES

        private string _ztmTimeTableSelectionLoadingTitle = string.Empty;
        private string _ztmTimeTableSelectionLoadingDesc = string.Empty;
        private string _ztmTimeTableSelectionTitle = string.Empty;
        private string _ztmTimeTableSelectionOkButton = string.Empty;
        private string _ztmTimeTableSelectionCancelButton = string.Empty;
        private string _ztmTimeTableSelectionFromToTitle = string.Empty;
        private string _ztmTimeTableSelectionFrom = string.Empty;
        private string _ztmTimeTableSelectionTo = string.Empty;
        private string _ztmTimeTableSelectionDownloadErrorTitle = string.Empty;
        private string _ztmTimeTableSelectionDownloadErrorDesc = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmTimeTableSelectionLoadingTitle
        {
            get => _ztmTimeTableSelectionLoadingTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionLoadingTitle, nameof(ZtmTimeTableSelectionLoadingTitle), value);
        }

        public string ZtmTimeTableSelectionLoadingDesc
        {
            get => _ztmTimeTableSelectionLoadingDesc;
            set => SetStringProperty(ref _ztmTimeTableSelectionLoadingDesc, nameof(ZtmTimeTableSelectionLoadingDesc), value);
        }

        public string ZtmTimeTableSelectionTitle
        {
            get => _ztmTimeTableSelectionTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionTitle, nameof(ZtmTimeTableSelectionTitle), value);
        }

        public string ZtmTimeTableSelectionOkButton
        {
            get => _ztmTimeTableSelectionOkButton;
            set => SetStringProperty(ref _ztmTimeTableSelectionOkButton, nameof(ZtmTimeTableSelectionOkButton), value);
        }

        public string ZtmTimeTableSelectionCancelButton
        {
            get => _ztmTimeTableSelectionCancelButton;
            set => SetStringProperty(ref _ztmTimeTableSelectionCancelButton, nameof(ZtmTimeTableSelectionCancelButton), value);
        }

        public string ZtmTimeTableSelectionFromToTitle
        {
            get => _ztmTimeTableSelectionFromToTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionFromToTitle, nameof(ZtmTimeTableSelectionFromToTitle), value);
        }

        public string ZtmTimeTableSelectionFrom
        {
            get => _ztmTimeTableSelectionFrom;
            set => SetStringProperty(ref _ztmTimeTableSelectionFrom, nameof(ZtmTimeTableSelectionFrom), value);
        }

        public string ZtmTimeTableSelectionTo
        {
            get => _ztmTimeTableSelectionTo;
            set => SetStringProperty(ref _ztmTimeTableSelectionTo, nameof(ZtmTimeTableSelectionTo), value);
        }

        public string ZtmTimeTableSelectionDownloadErrorTitle
        {
            get => _ztmTimeTableSelectionDownloadErrorTitle;
            set => SetStringProperty(ref _ztmTimeTableSelectionDownloadErrorTitle, nameof(ZtmTimeTableSelectionDownloadErrorTitle), value);
        }

        public string ZtmTimeTableSelectionDownloadErrorDesc
        {
            get => _ztmTimeTableSelectionDownloadErrorDesc;
            set => SetStringProperty(ref _ztmTimeTableSelectionDownloadErrorDesc, nameof(ZtmTimeTableSelectionDownloadErrorDesc), value);
        }

        #endregion ZtmTimeTableSelectionIM

        #region ZtmTransportTypes

        //  VARIABLES

        private string _ztmTransportTypeBus = string.Empty;
        private string _ztmTransportTypeBusAirport = string.Empty;
        private string _ztmTransportTypeBusMetropolitan = string.Empty;
        private string _ztmTransportTypeBusNight = string.Empty;
        private string _ztmTransportTypeBusReplacement = string.Empty;
        private string _ztmTransportTypeTram = string.Empty;
        private string _ztmTransportTypeTrolley = string.Empty;


        //  GETTERS & SETTERS

        public string ZtmTransportTypeBus
        {
            get => _ztmTransportTypeBus;
            set => SetStringProperty(ref _ztmTransportTypeBus, nameof(ZtmTransportTypeBus), value);
        }

        public string ZtmTransportTypeBusAirport
        {
            get => _ztmTransportTypeBusAirport;
            set => SetStringProperty(ref _ztmTransportTypeBusAirport, nameof(ZtmTransportTypeBusAirport), value);
        }

        public string ZtmTransportTypeBusMetropolitan
        {
            get => _ztmTransportTypeBusMetropolitan;
            set => SetStringProperty(ref _ztmTransportTypeBusMetropolitan, nameof(ZtmTransportTypeBusMetropolitan), value);
        }

        public string ZtmTransportTypeBusNight
        {
            get => _ztmTransportTypeBusNight;
            set => SetStringProperty(ref _ztmTransportTypeBusNight, nameof(ZtmTransportTypeBusNight), value);
        }

        public string ZtmTransportTypeBusReplacement
        {
            get => _ztmTransportTypeBusReplacement;
            set => SetStringProperty(ref _ztmTransportTypeBusReplacement, nameof(ZtmTransportTypeBusReplacement), value);
        }

        public string ZtmTransportTypeTram
        {
            get => _ztmTransportTypeTram;
            set => SetStringProperty(ref _ztmTransportTypeTram, nameof(ZtmTransportTypeTram), value);
        }

        public string ZtmTransportTypeTrolley
        {
            get => _ztmTransportTypeTrolley;
            set => SetStringProperty(ref _ztmTransportTypeTrolley, nameof(ZtmTransportTypeTrolley), value);
        }

        #endregion ZtmTransportTypes


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LangConfig class constructor. </summary>
        [JsonConstructor]
        public LangConfig(
            string? name = null,

            //  MainMenu
            string? mainMenuItem = null,

            //  StartPage
            string? startPageTitle = null,
            string? startPageMpkCzestochowaItem = null,
            string? startPageZtmMenuItem = null,
            string? startPageSettingsMenuItem = null,

            //  SettingsPage
            string? settingsPageTitle = null,
            string? settingsPageAppearanceMenuItem = null,
            string? settingsPageAppearanceMenuItemDesc = null,
            string? settingsPageGeneralMenuItem = null,
            string? settingsPageGeneralMenuItemDesc = null,
            string? settingsPageInfoMenuItem = null,
            string? settingsPageInfoMenuItemDesc = null,

            //  AppearanceSettingsPage
            string? settingsAppearancePageTitle = null,
            string? settingsAppearanceTheme = null,
            string? settingsAppearanceThemeDesc = null,
            string? settingsAppearanceThemeDark = null,
            string? settingsAppearanceThemeLight = null,
            string? settingsAppearnaceAccentColors = null,
            string? settingsAppearanceAccentColorsHistory = null,

            //  GeneralSettingsPage
            string? settingsGeneralPageTitle = null,
            string? settingsGeneralPageLanguage = null,
            string? settingsGeneralPageLanguageDesc = null,

            //  InfoSettingsPage
            string? settingsInfoPageTitle = null,
            string? settingsInfoPageDescription = null,
            string? settingsInfoPageAuthor = null,
            string? settingsInfoPageCopyright = null,
            string? settingsInfoPageLocation = null,

            //  MpkCzestochowaLinesView
            string? mpkCzestochowaMessageDateTitle = null,
            string? mpkCzestochowaMessageLinesTitle = null,

            //  MpkCzestochowaTransportTypes
            string? mpkCzestochowaTransportTypeBus = null,
            string? mpkCzestochowaTransportTypeBusNight = null,
            string? mpkCzestochowaTransportTypeBusSuburban = null,
            string? mpkCzestochowaTransportTypeTram = null,

            //  ZtmArrivalsIM
            string? ztmArrivalsLoadingTitle = null,
            string? ztmArrivalsLoadingDesc = null,
            string? ztmArrivalsDownloadErrorTitle = null,
            string? ztmArrivalsDownloadErrorDesc = null,
            string? ztmArrivalsTitle = null,
            string? ztmArrivalsCancelButton = null,
            string? ztmArrivalStopHeader = null,
            string? ztmArrivalDepartureHeader = null,
            string? ztmArrivalTripTimeHeader = null,
            string? ztmArrivalTripDistanceHeader = null,

            //  ZtmDeparturesView
            string? ztmDeparturesViewLoadingTitle = null,
            string? ztmDeparturesViewLoadingDesc = null,
            string? ztmDeparturesViewDownloadErrorTitle = null,
            string? ztmDeparturesViewDownloadErrorDesc = null,

            //  ZtmLineDetailsViewPage
            string? ztmLineDetailsViewLoadingTitle = null,
            string? ztmLineDetailsViewLoadingDesc = null,
            string? ztmLineDetailsViewPageDirection = null,
            string? ztmLineDetailsViewPageMessages = null,
            string? ztmLineDetailsViewPageDepartures = null,
            string? ztmLineDetailsViewDownloadErrorTitle = null,
            string? ztmLineDetailsViewDownloadErrorDesc = null,

            //  ZtmLinesViewPage
            string? ztmLineViewPageTitle = null,
            string? ztmLineViewLinesMenuItem = null,
            string? ztmLineViewLoadingTitle = null,
            string? ztmLineViewLoadingDesc = null,
            string? ztmLineViewRefreshButton = null,
            string? ztmLineViewDownloadErrorTitle = null,
            string? ztmLineViewDownloadErrorDesc = null,

            //  ZtmTimeTableSelectionIM
            string? ztmTimeTableSelectionLoadingTitle = null,
            string? ztmTimeTableSelectionLoadingDesc = null,
            string? ztmTimeTableSelectionTitle = null,
            string? ztmTimeTableSelectionOkButton = null,
            string? ztmTimeTableSelectionCancelButton = null,
            string? ztmTimeTableSelectionFromToTitle = null,
            string? ztmTimeTableSelectionFrom = null,
            string? ztmTimeTableSelectionTo = null,
            string? ztmTimeTableSelectionDownloadErrorTitle = null,
            string? ztmTimeTableSelectionDownloadErrorDesc = null,

            //  ZtmTransportTypes
            string? ztmTransportTypeBus = null,
            string? ztmTransportTypeBusAirport = null,
            string? ztmTransportTypeBusMetropolitan = null,
            string? ztmTransportTypeBusNight = null,
            string? ztmTransportTypeBusReplacement = null,
            string? ztmTransportTypeTram = null,
            string? ztmTransportTypeTrolley = null)
        {
            Name = GetLanguageValue(name, "Polski");

            #region MainMenu

            MainMenuItem = GetLanguageValue(mainMenuItem, "Menu główne");

            #endregion MainMenu

            #region StartPage

            StartPageTitle = GetLanguageValue(startPageTitle, "Wybór miasta (przedsiębiorstwa komunikacyjnego)");
            StartPageMpkCzestochowaItem = GetLanguageValue(startPageMpkCzestochowaItem, "MPK Czestochowa");
            StartPageZtmMenuItem = GetLanguageValue(startPageZtmMenuItem, "Zarząd Transportu Metropolitalnego");
            StartPageSettingsMenuItem = GetLanguageValue(startPageSettingsMenuItem, "Ustawienia");

            #endregion StartPage

            #region SettingsPage

            SettingsPageTitle = GetLanguageValue(settingsPageTitle, "Ustawienia");
            SettingsPageAppearanceMenuItem = GetLanguageValue(settingsPageAppearanceMenuItem, "Personalizacja");
            SettingsPageAppearanceMenuItemDesc = GetLanguageValue(settingsPageAppearanceMenuItemDesc, "Konfiguracja motywu i wyglądu aplikacji.");
            SettingsPageGeneralMenuItem = GetLanguageValue(settingsPageGeneralMenuItem, "Ogólne");
            SettingsPageGeneralMenuItemDesc = GetLanguageValue(settingsPageGeneralMenuItemDesc, "Ogólne opcje konfiguracji.");
            SettingsPageInfoMenuItem = GetLanguageValue(settingsPageInfoMenuItem, "Informacje");
            SettingsPageInfoMenuItemDesc = GetLanguageValue(settingsPageInfoMenuItemDesc, "Informacje o aplikacji.");

            #endregion SettingsPage

            #region AppearanceSettingsPage

            SettingsAppearancePageTitle = GetLanguageValue(settingsAppearancePageTitle, "Personalizacja");
            SettingsAppearanceTheme = GetLanguageValue(settingsAppearanceTheme, "Motyw");
            SettingsAppearanceThemeDesc = GetLanguageValue(settingsAppearanceThemeDesc, "Ustaw motyw aplikacji.");
            SettingsAppearanceThemeDark = GetLanguageValue(settingsAppearanceThemeDark, "Ciemny");
            SettingsAppearanceThemeLight = GetLanguageValue(settingsAppearanceThemeLight, "Jasny");
            SettingsAppearnaceAccentColors = GetLanguageValue(settingsAppearnaceAccentColors, "Wybór koloru akcentu:");
            SettingsAppearanceAccentColorsHistory = GetLanguageValue(settingsAppearanceAccentColorsHistory, "Ostatnio wybierane kolory:");

            #endregion AppearanceSettingsPage

            #region GeneralSettingsPage

            SettingsGeneralPageTitle = GetLanguageValue(settingsGeneralPageTitle, "Ogólne");
            SettingsGeneralPageLanguage = GetLanguageValue(settingsGeneralPageLanguage, "Język");
            SettingsGeneralPageLanguageDesc = GetLanguageValue(settingsGeneralPageLanguageDesc, "Wybór języka aplikacji.");

            #endregion GeneralSettingsPage

            #region InfoSettingsPage

            SettingsInfoPageTitle = GetLanguageValue(settingsInfoPageTitle, "Informacje");
            SettingsInfoPageDescription = GetLanguageValue(settingsInfoPageDescription, "Opis:");
            SettingsInfoPageAuthor = GetLanguageValue(settingsInfoPageAuthor, "Autor:");
            SettingsInfoPageCopyright = GetLanguageValue(settingsInfoPageCopyright, "Prawa autorskie:");
            SettingsInfoPageLocation = GetLanguageValue(settingsInfoPageLocation, "Lokalizacja:");

            #endregion InfoSettingsPage

            #region MpkCzestochowaLinesView

            MpkCzestochowaMessageDateTitle = GetLanguageValue(mpkCzestochowaMessageDateTitle, "Data obowiązywania: ");
            MpkCzestochowaMessageLinesTitle = GetLanguageValue(mpkCzestochowaMessageLinesTitle, "Dotyczy linii: ");

            #endregion MpkCzestochowaLinesView

            #region MpkCzestochowaTransportTypes

            MpkCzestochowaTransportTypeBus = GetLanguageValue(mpkCzestochowaTransportTypeBus, "Autobus");
            MpkCzestochowaTransportTypeBusNight = GetLanguageValue(mpkCzestochowaTransportTypeBusNight, "Autobus Nocny");
            MpkCzestochowaTransportTypeBusSuburban = GetLanguageValue(mpkCzestochowaTransportTypeBusSuburban, "Autobus Podmiejski");
            MpkCzestochowaTransportTypeTram = GetLanguageValue(mpkCzestochowaTransportTypeTram, "Tramwaj");

            #endregion MpkCzestochowaTransportTypes

            #region ZtmArrivalsIM

            ZtmArrivalsLoadingTitle = GetLanguageValue(ztmArrivalsLoadingTitle, "Pobieranie danych");
            ZtmArrivalsLoadingDesc = GetLanguageValue(ztmArrivalsLoadingDesc, "Trwa pobieranie danych godzin przyjazdów. \r\nProszę czekać ...");
            ZtmArrivalsDownloadErrorTitle = GetLanguageValue(ztmArrivalsDownloadErrorTitle, "Błąd pobierania danych");
            ZtmArrivalsDownloadErrorDesc = GetLanguageValue(ztmArrivalsDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin przyjazdów. \r\nProszę spróbować ponownie za jakiś czas.");
            ZtmArrivalsTitle = GetLanguageValue(ztmArrivalsTitle, "Godziny przyjazdów");
            ZtmArrivalsCancelButton = GetLanguageValue(ztmArrivalsCancelButton, "Zamknij");
            ZtmArrivalStopHeader = GetLanguageValue(ztmArrivalStopHeader, "Nazwa przystanku");
            ZtmArrivalDepartureHeader = GetLanguageValue(ztmArrivalDepartureHeader, "Godz.");
            ZtmArrivalTripTimeHeader = GetLanguageValue(ztmArrivalTripTimeHeader, "Czas podróży");
            ZtmArrivalTripDistanceHeader = GetLanguageValue(ztmArrivalTripDistanceHeader, "Odległość");

            #endregion ZtmArrivalsIM

            #region ZtmDeparturesView

            ZtmDeparturesViewLoadingTitle = GetLanguageValue(ztmDeparturesViewLoadingTitle, "Pobieranie danych");
            ZtmDeparturesViewLoadingDesc = GetLanguageValue(ztmDeparturesViewLoadingDesc, "Trwa pobieranie danych godzin odjazdów. \r\nProszę czekać ...");
            ZtmDeparturesViewDownloadErrorTitle = GetLanguageValue(ztmDeparturesViewDownloadErrorTitle, "Błąd pobierania danych");
            ZtmDeparturesViewDownloadErrorDesc = GetLanguageValue(ztmDeparturesViewDownloadErrorDesc, "Wystąpił problem podczas pobierania godzin odjazdów. \r\nProszę spróbować ponownie za jakiś czas.");

            #endregion ZtmDeparturesView

            #region ZtmLineDetailsViewPage

            ZtmLineDetailsViewLoadingTitle = GetLanguageValue(ztmLineDetailsViewLoadingTitle, "Pobieranie danych");
            ZtmLineDetailsViewLoadingDesc = GetLanguageValue(ztmLineDetailsViewLoadingDesc, "Trwa pobieranie szczegółowych danych linii. \r\nProszę czekać ...");
            ZtmLineDetailsViewPageDirection = GetLanguageValue(ztmLineDetailsViewPageDirection, "Wybór kierunku:");
            ZtmLineDetailsViewPageMessages = GetLanguageValue(ztmLineDetailsViewPageMessages, "Komunikaty:");
            ZtmLineDetailsViewPageDepartures = GetLanguageValue(ztmLineDetailsViewPageDepartures, "Godziny odjazdów:");
            ZtmLineDetailsViewDownloadErrorTitle = GetLanguageValue(ztmLineDetailsViewDownloadErrorTitle, "Błąd pobierania danych");
            ZtmLineDetailsViewDownloadErrorDesc = GetLanguageValue(ztmLineDetailsViewDownloadErrorDesc, "Wystąpił problem podczas pobierania szczegółowych danych linii. \r\nProszę spróbować ponownie za jakiś czas.");
            
            #endregion ZtmLineDetailsViewPage

            #region ZtmLinesViewPage

            ZtmLineViewPageTitle = GetLanguageValue(ztmLineViewPageTitle, "Wybór linii");
            ZtmLineViewLinesMenuItem = GetLanguageValue(ztmLineViewLinesMenuItem, "Wybór linii");
            ZtmLineViewLoadingTitle = GetLanguageValue(ztmLineViewLoadingTitle, "Pobieranie danych");
            ZtmLineViewLoadingDesc = GetLanguageValue(ztmLineViewLoadingDesc, "Trwa pobieranie danych linii. \r\nProszę czekać...");
            ZtmLineViewRefreshButton = GetLanguageValue(ztmLineViewRefreshButton, "Odśwież");
            ZtmLineViewDownloadErrorTitle = GetLanguageValue(ztmLineViewDownloadErrorTitle, "Błąd pobierania danych");
            ZtmLineViewDownloadErrorDesc = GetLanguageValue(ztmLineViewDownloadErrorDesc, "Wystąpił problem podczas pobierania danych linii. \r\nProszę spróbować ponownie za jakiś czas.");

            #endregion ZtmLinesViewPage

            #region ZtmTimeTableSelectionIM

            ZtmTimeTableSelectionLoadingTitle = GetLanguageValue(ztmTimeTableSelectionLoadingTitle, "Pobieranie danych");
            ZtmTimeTableSelectionLoadingDesc = GetLanguageValue(ztmTimeTableSelectionLoadingDesc, "Trwa pobieranie danych rozkładów jazdy. \r\nProszę czekać ...");
            ZtmTimeTableSelectionTitle = GetLanguageValue(ztmTimeTableSelectionTitle, "Wybór rozkładu jazdy");
            ZtmTimeTableSelectionOkButton = GetLanguageValue(ztmTimeTableSelectionOkButton, "Wybierz");
            ZtmTimeTableSelectionCancelButton = GetLanguageValue(ztmTimeTableSelectionCancelButton, "Anuluj");
            ZtmTimeTableSelectionFromToTitle = GetLanguageValue(ztmTimeTableSelectionFromToTitle, "Rozkład ważny");
            ZtmTimeTableSelectionFrom = GetLanguageValue(ztmTimeTableSelectionFrom, "od");
            ZtmTimeTableSelectionTo = GetLanguageValue(ztmTimeTableSelectionTo, "do");
            ZtmTimeTableSelectionDownloadErrorTitle = GetLanguageValue(ztmTimeTableSelectionDownloadErrorTitle, "Błąd pobierania danych");
            ZtmTimeTableSelectionDownloadErrorDesc = GetLanguageValue(ztmTimeTableSelectionDownloadErrorDesc, "Wystąpił problem podczas pobierania danych rozkładów jazdy. \r\nProszę spróbować ponownie za jakiś czas.");

            #endregion ZtmTimeTableSelectionIM

            #region ZtmTransportTypes

            ZtmTransportTypeBus = GetLanguageValue(ztmTransportTypeBus, "Autobus");
            ZtmTransportTypeBusAirport = GetLanguageValue(ztmTransportTypeBusAirport, "Autobus na lotnisko");
            ZtmTransportTypeBusMetropolitan = GetLanguageValue(ztmTransportTypeBusMetropolitan, "Autobus metropolitalny");
            ZtmTransportTypeBusNight = GetLanguageValue(ztmTransportTypeBusNight, "Autobus nocny");
            ZtmTransportTypeBusReplacement = GetLanguageValue(ztmTransportTypeBusReplacement, "Autobus zastępczy");
            ZtmTransportTypeTram = GetLanguageValue(ztmTransportTypeTram, "Tramwaj");
            ZtmTransportTypeTrolley = GetLanguageValue(ztmTransportTypeTrolley, "Trolejbus");

            #endregion ZtmTransportTypes
        }

        #endregion CLASS METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method for invoking PropertyChangedEventHandler external method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region PROPERTIES HELPER

        //  --------------------------------------------------------------------------------
        /// <summary> Get language value or default value instead. </summary>
        /// <param name="value"> Language value. </param>
        /// <param name="defaultValue"> Default language value. </param>
        private string GetLanguageValue(string? value, string defaultValue)
        {
            return !string.IsNullOrWhiteSpace(value) ? value : defaultValue;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Set property value helper method. </summary>
        /// <param name="privatePropertyName"> Private property name. </param>
        /// <param name="publicPropertyName"> Public property name. </param>
        /// <param name="value"> Value to set. </param>
        private void SetStringProperty(ref string privateProperty, string propertyName, string value)
        {
            if (privateProperty != null)
            {
                privateProperty = value;
                OnPropertyChanged(propertyName);
            }
        }

        #endregion PROPERTIES HELPER

    }
}
