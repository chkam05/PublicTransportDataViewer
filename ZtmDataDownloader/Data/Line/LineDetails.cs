using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Mappers;

namespace ZtmDataDownloader.Data.Line
{
    public class LineDetails
    {

        //  VARIABLES

        public TransportType TransportType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public List<Message> Messages { get; set; }
        public List<LineDirection> Directions { get; set; }


        //  GETTERS & SETTERS

        public bool HasDirections
        {
            get => Directions != null && Directions.Any();
        }

        public bool HasMessages
        {
            get => Messages != null && Messages.Any();
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetails class constructor. </summary>
        public LineDetails()
        {
            if (Messages == null)
                Messages = new List<Message>();

            if (Directions == null)
                Directions = new List<LineDirection>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            var transportType = TransportTypesMapper.MapToName(TransportType);

            return $"{transportType} {Name}";
        }

        #endregion CLASS METHODS

    }
}
