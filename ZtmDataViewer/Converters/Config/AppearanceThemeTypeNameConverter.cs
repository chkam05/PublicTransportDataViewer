using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZtmDataViewer.Data.Config;

namespace ZtmDataViewer.Converters.Config
{
    public class AppearanceThemeTypeNameConverter : IValueConverter
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var appearanceThemeType = (AppearanceThemeType)value;
            var configManager = ConfigManager.Instance;

            switch (appearanceThemeType)
            {
                case AppearanceThemeType.Light:
                    return configManager.LangConfig.Settings.Appearance.ThemeOptionLight;

                case AppearanceThemeType.Dark:
                default:
                    return configManager.LangConfig.Settings.Appearance.ThemeOptionDark;
            }
        }

        //  --------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
