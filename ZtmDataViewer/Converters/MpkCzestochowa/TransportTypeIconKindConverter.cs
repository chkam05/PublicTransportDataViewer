using MaterialDesignThemes.Wpf;
using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PublicTransportDataViewer.Converters.MpkCzestochowa
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
                case TransportType.BusSuburban:
                    return PackIconKind.Bus;
                case TransportType.BusNight:
                    return PackIconKind.Bus;
                case TransportType.Tram:
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
