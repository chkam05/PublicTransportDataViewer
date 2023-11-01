using chkam05.Tools.ControlsEx.Colors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZtmDataViewer.Utilities;

namespace ZtmDataViewer.Data.Config
{
    public class AppearanceConfig : BaseViewModel
    {

        //  CONST

        private static readonly Color ACCENT_COLOR = Color.FromArgb(255, 214, 0, 111);
        private static readonly Color THEME_DARK_COLOR = Color.FromArgb(255, 36, 36, 36);
        private static readonly Color THEME_LIGHT_COLOR = Color.FromArgb(255, 219, 219, 219);

        private static readonly int FACTOR_INACTIVE = 15;
        private static readonly int FACTOR_MOUSE_OVER = 15;
        private static readonly int FACTOR_PRESSED = 10;
        private static readonly int FACTOR_SELECTED = 5;


        //  VARIABLES

        private Color _accentColor = ACCENT_COLOR;
        private AppearanceThemeType _appearanceThemeType = AppearanceThemeType.Dark;
        private List<AppearanceColor> _appearanceColorsList;

        private Brush _accentColorBrush;
        private Brush _accentForegroundColorBrush;
        private Brush _accentMouseOverColorBrush;
        private Brush _accentPressedColorBrush;
        private Brush _accentSelectedColorBrush;
        private Brush _themeBackgroundColorBrush;
        private Brush _themeForegroundColorBrush;
        private Brush _themeMouseOverColorBrush;
        private Brush _themePressedColorBrush;
        private Brush _themeSelectedColorBrush;
        private Brush _themeShadeBackgroundColorBrush;


        //  GETTERS & SETTERS

        public Color AccentColor
        {
            get => _accentColor;
            set
            {
                _accentColor = value;
                UpdateAccentAttributes();
                OnPropertyChanged(nameof(AccentColor));
            }
        }

        public AppearanceThemeType AppearanceThemeType
        {
            get => _appearanceThemeType;
            set
            {
                _appearanceThemeType = value;
                UpdateThemeAttributes();
                OnPropertyChanged(nameof(AppearanceThemeType));
            }
        }

        public List<AppearanceColor> AppearanceColorsList
        {
            get => _appearanceColorsList;
            set
            {
                _appearanceColorsList = value;
                OnPropertyChanged(nameof(AppearanceColorsList));
            }
        }

        [JsonIgnore]
        public Brush AccentColorBrush
        {
            get => _accentColorBrush;
            set
            {
                _accentColorBrush = value;
                OnPropertyChanged(nameof(AccentColorBrush));
            }
        }

        [JsonIgnore]
        public Brush AccentForegroundColorBrush
        {
            get => _accentForegroundColorBrush;
            set
            {
                _accentForegroundColorBrush = value;
                OnPropertyChanged(nameof(AccentForegroundColorBrush));
            }
        }

        [JsonIgnore]
        public Brush AccentMouseOverColorBrush
        {
            get => _accentMouseOverColorBrush;
            set
            {
                _accentMouseOverColorBrush = value;
                OnPropertyChanged(nameof(AccentMouseOverColorBrush));
            }
        }

        [JsonIgnore]
        public Brush AccentPressedColorBrush
        {
            get => _accentPressedColorBrush;
            set
            {
                _accentPressedColorBrush = value;
                OnPropertyChanged(nameof(AccentPressedColorBrush));
            }
        }

        [JsonIgnore]
        public Brush AccentSelectedColorBrush
        {
            get => _accentSelectedColorBrush;
            set
            {
                _accentSelectedColorBrush = value;
                OnPropertyChanged(nameof(AccentSelectedColorBrush));
            }
        }

        [JsonIgnore]
        public Brush ThemeBackgroundColorBrush
        {
            get => _themeBackgroundColorBrush;
            set
            {
                _themeBackgroundColorBrush = value;
                OnPropertyChanged(nameof(ThemeBackgroundColorBrush));
            }
        }

        [JsonIgnore]
        public Brush ThemeForegroundColorBrush
        {
            get => _themeForegroundColorBrush;
            set
            {
                _themeForegroundColorBrush = value;
                OnPropertyChanged(nameof(ThemeForegroundColorBrush));
            }
        }

        [JsonIgnore]
        public Brush ThemeMouseOverColorBrush
        {
            get => _themeMouseOverColorBrush;
            set
            {
                _themeMouseOverColorBrush = value;
                OnPropertyChanged(nameof(ThemeMouseOverColorBrush));
            }
        }

        [JsonIgnore]
        public Brush ThemePressedColorBrush
        {
            get => _themePressedColorBrush;
            set
            {
                _themePressedColorBrush = value;
                OnPropertyChanged(nameof(ThemePressedColorBrush));
            }
        }

        [JsonIgnore]
        public Brush ThemeSelectedColorBrush
        {
            get => _themeSelectedColorBrush;
            set
            {
                _themeSelectedColorBrush = value;
                OnPropertyChanged(nameof(ThemeSelectedColorBrush));
            }
        }

        [JsonIgnore]
        public Brush ThemeShadeBackgroundColorBrush
        {
            get => _themeShadeBackgroundColorBrush;
            set
            {
                _themeShadeBackgroundColorBrush = value;
                OnPropertyChanged(nameof(ThemeShadeBackgroundColorBrush));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> AppearanceConfig class constructor. </summary>
        /// <param name="accentColor"> Accent color. </param>
        /// <param name="appearanceThemeType"> Appearance theme type. </param>
        [JsonConstructor]
        public AppearanceConfig(
            Color? accentColor = null,
            AppearanceThemeType? appearanceThemeType = null,
            List<AppearanceColor>? appearanceColorsList = null)
        {
            AccentColor = accentColor != null ? accentColor.Value : ACCENT_COLOR;
            AppearanceThemeType = appearanceThemeType != null ? appearanceThemeType.Value : AppearanceThemeType.Dark;
            AppearanceColorsList = (appearanceColorsList?.Any() ?? false) ? appearanceColorsList : new List<AppearanceColor>()
            {
                new AppearanceColor("#D6006F", "Rose"),
                new AppearanceColor("#0078D7", "Blue"),
                new AppearanceColor("#E81123", "Red"),
                new AppearanceColor("#FFB900", "Gold Yellow"),
                new AppearanceColor("#107C10", "Green")
            };
        }

        #endregion CLASS METHODS

        #region UPDATE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Trigger properties update. </summary>
        public void TriggerPropertiesUpdate()
        {
            OnPropertyChanged(nameof(AccentColor));
            OnPropertyChanged(nameof(AppearanceThemeType));

            UpdateAccentAttributes();
            UpdateThemeAttributes();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Update accent color attributes. </summary>
        private void UpdateAccentAttributes()
        {
            var accentAhslColor = AHSLColor.FromColor(AccentColor);
            var accentForegroundColor = ColorsHelper.GetContrastColor(AccentColor);

            //  Create colors.
            var accentMouseOverColor = ColorsHelper.UpdateColor(
                accentAhslColor, lightness: accentAhslColor.L + FACTOR_MOUSE_OVER).ToColor();

            var accentPressedColor = ColorsHelper.UpdateColor(
                accentAhslColor, lightness: accentAhslColor.L - FACTOR_PRESSED).ToColor();

            var accentSelectedColor = ColorsHelper.UpdateColor(
                accentAhslColor, lightness: accentAhslColor.L - FACTOR_SELECTED).ToColor();

            //  Set properties.
            AccentColorBrush = new SolidColorBrush(AccentColor);
            AccentForegroundColorBrush = new SolidColorBrush(accentForegroundColor);
            AccentMouseOverColorBrush = new SolidColorBrush(accentMouseOverColor);
            AccentPressedColorBrush = new SolidColorBrush(accentPressedColor);
            AccentSelectedColorBrush = new SolidColorBrush(accentSelectedColor);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Update theme color attributes. </summary>
        private void UpdateThemeAttributes()
        {
            Color? backgroundColor = null;
            Color? foregroundColor = null;
            Color? shadeBackgroundColor = null;

            switch (AppearanceThemeType)
            {
                case AppearanceThemeType.Dark:
                    backgroundColor = Colors.Black;
                    foregroundColor = Colors.White;
                    shadeBackgroundColor = THEME_DARK_COLOR;
                    break;

                case AppearanceThemeType.Light:
                default:
                    backgroundColor = Colors.White;
                    foregroundColor = Colors.Black;
                    shadeBackgroundColor = THEME_LIGHT_COLOR;
                    break;
            }

            var themeAhslColor = AHSLColor.FromColor(backgroundColor.Value);

            //  Create colors.
            var themeMouseOverColor = ColorsHelper.UpdateColor(
                themeAhslColor,
                lightness: themeAhslColor.S > 50 ? themeAhslColor.S + FACTOR_MOUSE_OVER : themeAhslColor.L - FACTOR_MOUSE_OVER,
                saturation: 0).ToColor();

            var themePressedColor = ColorsHelper.UpdateColor(
                themeAhslColor,
                lightness: themeAhslColor.S > 50
                    ? themeAhslColor.S - FACTOR_PRESSED
                    : themeAhslColor.L + FACTOR_PRESSED,
                saturation: 0).ToColor();

            var themeSelectedColor = ColorsHelper.UpdateColor(
                themeAhslColor,
                lightness: themeAhslColor.S > 50
                    ? themeAhslColor.S - FACTOR_SELECTED
                    : themeAhslColor.L + FACTOR_SELECTED,
                saturation: 0).ToColor();

            //  Set properties.
            ThemeBackgroundColorBrush = new SolidColorBrush(backgroundColor.Value);
            ThemeForegroundColorBrush = new SolidColorBrush(foregroundColor.Value);
            ThemeMouseOverColorBrush = new SolidColorBrush(themeMouseOverColor);
            ThemePressedColorBrush= new SolidColorBrush(themePressedColor);
            ThemeSelectedColorBrush= new SolidColorBrush(themeSelectedColor);
            ThemeShadeBackgroundColorBrush = new SolidColorBrush(shadeBackgroundColor.Value);
        }

        #endregion UPDATE METHODS

    }
}
