using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZtmDataViewer.Data.Config;

namespace ZtmDataViewer.Converters.MpkCzestochowa
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
                    return langConfig.MpkCzestochowaTransportTypeBus;
                case TransportType.BusSuburban:
                    return langConfig.MpkCzestochowaTransportTypeBusSuburban;
                case TransportType.BusNight:
                    return langConfig.MpkCzestochowaTransportTypeBusNight;
                case TransportType.Tram:
                    return langConfig.MpkCzestochowaTransportTypeTram;
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
