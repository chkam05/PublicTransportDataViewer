using chkam05.ZtmDataDownloader.Data.Global;
using chkam05.ZtmDataDownloader.Data.Static;
using chkam05.ZtmDataDownloader.Mappers;
using System.Collections.Generic;
using System.Linq;


namespace chkam05.ZtmDataDownloader.Data.Line
{
    public class Line
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
        /// <summary> Line class constructor. </summary>
        public Line()
        {
            if (Messages == null)
                Messages = new List<Message>();

            if (Directions == null)
                Directions = new List<LineDirection>();
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            var transportType = TransportTypesMapper.MapToName(TransportType);

            return $"{transportType} {Name}";
        }

    }
}
