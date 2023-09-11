using chkam05.Tools.ControlsEx.Colors;
using chkam05.Tools.ControlsEx.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZtmDataViewer.Components;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Utilities;

namespace ZtmDataViewer.Pages.Settings
{
    public partial class AppearanceSettingsPage : BasePage
    {

        //  VARIABLES

        private ObservableCollection<AppearanceThemeType> _appearanceThemeTypesCollection;
        private ObservableCollection<ColorPaletteItem> _palleteColorsCollection;


        //  GETTERS & SETTERS

        public ObservableCollection<AppearanceThemeType> AppearanceThemeTypesCollection
        {
            get => _appearanceThemeTypesCollection;
            private set
            {
                _appearanceThemeTypesCollection = value;
                _appearanceThemeTypesCollection.CollectionChanged += OnAppearanceThemeTypesCollectionChanged;
                OnPropertyChanged(nameof(AppearanceThemeTypesCollection));
            }
        }

        public ObservableCollection<ColorPaletteItem> PalleteColorsCollection
        {
            get => _palleteColorsCollection;
            set
            {
                _palleteColorsCollection = value;
                _palleteColorsCollection.CollectionChanged += OnPalleteColorsCollectionChanged;
                OnPropertyChanged(nameof(PalleteColorsCollection));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> AppearanceSettingsPage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public AppearanceSettingsPage(PagesController pagesController) : base(pagesController)
        {
            var configManager = ConfigManager.Instance;
            var colorsList = configManager.ConfigData.AppearanceConfig.AppearanceColorsList;

            //  Initialize data.
            AppearanceThemeTypesCollection = new ObservableCollection<AppearanceThemeType>(
                EnumHelper.GetEnumValues<AppearanceThemeType>());

            PalleteColorsCollection = new ObservableCollection<ColorPaletteItem>(
                colorsList.Select(c => c.ToColorPaletteItem()));

            //  Initialize user interface.
            InitializeComponent();
        }

        #endregion CLASS METHODS

        #region INTERACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after changing color in Appearance colors palette. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Colors Palette Selection Changed Event Arguments. </param>
        private void AccentColorPaletteEx_ColorSelectionChanged(object sender, ColorsPaletteSelectionChangedEventArgs e)
        {
            var configManager = ConfigManager.Instance;

            if (e?.SelectedColorItem != null)
            {
                configManager.ConfigData.AppearanceConfig.AccentColor = e.SelectedColorItem.Color;
                configManager.ConfigData.AppearanceConfig.AppearanceColorsList = PalleteColorsCollection
                    .Select(c => new AppearanceColor(c)).ToList();
            }
        }

        #endregion INTERACTION METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after appearance theme types collection change. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        protected void OnAppearanceThemeTypesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(AppearanceThemeTypesCollection));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after pallete colors collection change. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        protected void OnPalleteColorsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(PalleteColorsCollection));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region PAGE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after unloading page. </summary>
        /// <param name="sender"> Object that invoked method. </param>
        /// <param name="e"> Routed Event Arguments. </param>
        private void BasePage_Unloaded(object sender, RoutedEventArgs e)
        {
            ConfigManager.Instance.SaveConfigurationToFile();
        }

        #endregion PAGE METHODS

    }
}
