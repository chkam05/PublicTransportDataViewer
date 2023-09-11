using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ZtmDataDownloader.Data.Static;
using ZtmDataViewer.Data.Config;

namespace ZtmDataViewer.Converters.ZtmData
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
                    return langConfig.ZtmTransportTypeBus;
                case TransportType.BusAirport:
                    return langConfig.ZtmTransportTypeBusAirport;
                case TransportType.BusMetropolitan:
                    return langConfig.ZtmTransportTypeBusMetropolitan;
                case TransportType.BusNight:
                    return langConfig.ZtmTransportTypeBusNight;
                case TransportType.BusReplacement:
                    return langConfig.ZtmTransportTypeBusReplacement;
                case TransportType.Tram:
                    return langConfig.ZtmTransportTypeTram;
                case TransportType.Trolley:
                    return langConfig.ZtmTransportTypeTrolley;
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
