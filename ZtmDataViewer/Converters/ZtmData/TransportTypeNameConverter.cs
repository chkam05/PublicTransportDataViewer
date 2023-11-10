using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ZtmDataDownloader.Data.Static;
using PublicTransportDataViewer.Data.Config;

namespace PublicTransportDataViewer.Converters.ZtmData
{
    public class TransportTypeNameConverter : IValueConverter
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var langConfig = ConfigManager.Instance.LangConfig;
            var transportType = (TransportType)value;

            switch (transportType)
            {
                case TransportType.Bus:
                    return langConfig.Ztm.TransportTypes.Bus;
                case TransportType.BusAirport:
                    return langConfig.Ztm.TransportTypes.BusAirport;
                case TransportType.BusMetropolitan:
                    return langConfig.Ztm.TransportTypes.BusMetropolitan;
                case TransportType.BusNight:
                    return langConfig.Ztm.TransportTypes.BusNight;
                case TransportType.BusReplacement:
                    return langConfig.Ztm.TransportTypes.BusReplacement;
                case TransportType.Tram:
                    return langConfig.Ztm.TransportTypes.Tram;
                case TransportType.Trolley:
                    return langConfig.Ztm.TransportTypes.Trolley;
            }

            return string.Empty;
        }

        //  --------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
