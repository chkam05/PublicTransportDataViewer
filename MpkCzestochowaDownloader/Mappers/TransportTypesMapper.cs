using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Mappers
{
    public static class TransportTypesMapper
    {

        //  VARIABLES

        private static readonly Dictionary<TransportType, string> _namesDict = new Dictionary<TransportType, string>()
        {
            { TransportType.Tram, "Tram" },
            { TransportType.Bus, "Bus" },
            { TransportType.BusSuburban, "Suburban bus" },
            { TransportType.BusNight, "Night bus" },
        };

        private static readonly Dictionary<TransportType, string[]> _webClassDict = new Dictionary<TransportType, string[]>()
        {
            { TransportType.Tram, new string[] { "fa-subway", "mr-3" } },
            { TransportType.Bus, new string[] { "fa-bus", "mr-3" } },
            { TransportType.BusSuburban, new string[] { "fa-tree", "mr-1" } },
            { TransportType.BusNight, new string[] { "fa-moon-o", "mr-1" } },
        };


        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get list of names of transport types. </summary>
        /// <returns> Transport types names list. </returns>
        public static List<string> GetNames()
        {
            return _namesDict.Values.ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get list of transport types. </summary>
        /// <returns> Transport types list. </returns>
        public static List<TransportType> GetTypes()
        {
            return Enum.GetValues(typeof(TransportType)).Cast<TransportType>().ToList();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get transport type by class names. </summary>
        /// <param name="classNames"> List of class names. </param>
        /// <returns> Transport type or null. </returns>
        public static TransportType? MapFromClass(string[] classNames)
        {
            var keyValuePairs = _webClassDict.Where(c => c.Value.All(
                v => classNames.Contains(v)));

            return keyValuePairs.Any() ? keyValuePairs.First().Key : null;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get transport type by name. </summary>
        /// <param name="name"> Transport type name. </param>
        /// <returns> Transport type. </returns>
        public static TransportType MapFromName(string name)
        {
            return _namesDict.FirstOrDefault(kvp => kvp.Value.ToLower() == name.ToLower()).Key;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get name of particular transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Transport type name. </returns>
        public static string MapToName(TransportType transportType)
        {
            return _namesDict[transportType];
        }

    }
}
