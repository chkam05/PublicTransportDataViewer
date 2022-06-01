using chkam05.ZtmDataDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;


namespace chkam05.ZtmDataDownloader.Mappers
{
    public class TransportTypesMapper
    {

        //  VARIABLES

        private static readonly Dictionary<TransportType, string> _namesDict = new Dictionary<TransportType, string>()
        {
            { TransportType.Bus, "Bus" },
            { TransportType.BusAirport, "Bus Airport" },
            { TransportType.BusMetropolitan, "Bus Metropolitan" },
            { TransportType.BusNight, "Bus Night" },
            { TransportType.BusReplacement, "Bus Replacement" },
            { TransportType.Tram, "Tram" },
            { TransportType.Trolley, "Trolley" }
        };

        private static readonly Dictionary<TransportType, int> _webIndexesDict = new Dictionary<TransportType, int>()
        {
            { TransportType.Bus, 1 },
            { TransportType.BusAirport, 1 },
            { TransportType.BusMetropolitan, 1 },
            { TransportType.BusNight, 1 },
            { TransportType.BusReplacement, 1 },
            { TransportType.Tram, 2 },
            { TransportType.Trolley, 5 }
        };

        private static readonly Dictionary<TransportType, string> _webInitialsDict = new Dictionary<TransportType, string>()
        {
            { TransportType.Bus, "" },
            { TransportType.BusAirport, "" },
            { TransportType.BusMetropolitan, "" },
            { TransportType.BusNight, "" },
            { TransportType.BusReplacement, "" },
            { TransportType.Tram, "T" },
            { TransportType.Trolley, "" }
        };

        private static readonly Dictionary<TransportType, string> _nodeStrongNamesDict = new Dictionary<TransportType, string>()
        {
            { TransportType.Bus, "Autobus" },
            { TransportType.BusAirport, "Autobus" },
            { TransportType.BusMetropolitan, "Autobus" },
            { TransportType.BusNight, "Autobus" },
            { TransportType.BusReplacement, "Autobus" },
            { TransportType.Tram, "Tramwaj" },
            { TransportType.Trolley, "Trolejbus" }
        };

        private static readonly string _nodeValueDefault = "Normalna";

        private static readonly Dictionary<TransportType, string> _nodeValuesDict = new Dictionary<TransportType, string>()
        {
            { TransportType.Bus, _nodeValueDefault },
            { TransportType.BusAirport, "lotnisko" },
            { TransportType.BusMetropolitan, "metropolitalna" },
            { TransportType.BusNight, "Nocna" },
            { TransportType.BusReplacement, "zastępcza" },
            { TransportType.Tram, _nodeValueDefault },
            { TransportType.Trolley, _nodeValueDefault }
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
        /// <summary> Get index of particular transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Transport type index. </returns>
        public static int IndexOf(TransportType transportType)
        {
            return (int)transportType;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get name of particular transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Transport type name. </returns>
        public static string MapToName(TransportType transportType)
        {
            return _namesDict[transportType];
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
        /// <summary> Get web index of particular transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Web index of transport type. </returns>
        public static int MapToWebIndex(TransportType transportType)
        {
            return _webIndexesDict[transportType];
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get web initial of particular transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Initial of transport type. </returns>
        public static string MapToWebInitial(TransportType transportType)
        {
            return _webInitialsDict[transportType];
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get node strong name of particular transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Transport type node strong name. </returns>
        public static string MapToNodeStrongName(TransportType transportType)
        {
            return _nodeStrongNamesDict[transportType];
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Get node value of particular transport type. </summary>
        /// <param name="transportType"> Transport type. </param>
        /// <returns> Transport type node value. </returns>
        public static string MapToNodeValue(TransportType transportType)
        {
            return _nodeValuesDict[transportType];
        }

    }
}
