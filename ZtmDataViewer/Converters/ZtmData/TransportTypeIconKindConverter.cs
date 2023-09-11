using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZtmDataDownloader.Data.Static;
using ZtmDataViewer.Data.Config;

namespace ZtmDataViewer.Converters.ZtmData
{
    public class TransportTypeIconKindConverter : IValueConverter
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var transportType = (TransportType)value;

            switch (transportType)
            {
                case TransportType.Bus:
                    return PackIconKind.Bus;
                case TransportType.BusAirport:
                    return PackIconKind.Bus;
                case TransportType.BusMetropolitan:
                    return PackIconKind.Bus;
                case TransportType.BusNight:
                    return PackIconKind.Bus;
                case TransportType.BusReplacement:
                    return PackIconKind.BusWarning;
                case TransportType.Tram:
                    return PackIconKind.Tram;
                case TransportType.Trolley:
                    return PackIconKind.Tram;
            }

            return PackIconKind.Commute;
        }

        //  --------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
