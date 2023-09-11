using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ZtmDataDownloader.Data.Lines;
using ZtmDataViewer.Data.Config;
using ZtmDataViewer.Utilities;

namespace ZtmDataViewer.Converters.ZtmData
{
    public class LineColorSolidColorBrushConverter : IValueConverter
    {

        //  CONST

        private static readonly Color DEFAULT_COLOR = Colors.White;


        //  METHODS

        //  --------------------------------------------------------------------------------
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var appearanceConfig = ConfigManager.Instance.ConfigData.AppearanceConfig;
            var color = value as Color?;

            if (parameter is string param && !string.IsNullOrEmpty(param))
            {
                if (color != null && color != DEFAULT_COLOR)
                {
                    if (param == "Black")
                        return new SolidColorBrush(Colors.Black);
                    else if (param == "White")
                        return new SolidColorBrush(Colors.White);
                }

                return appearanceConfig.ThemeForegroundColorBrush;
            }
            else
            {
                if (color != null && color != DEFAULT_COLOR)
                    return new SolidColorBrush(color.Value);

                return appearanceConfig.ThemeShadeBackgroundColorBrush;
            }
        }

        //  --------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
