using chkam05.ZtmDataDownloader.Data.Static;
using chkam05.ZtmDataDownloader.Mappers;
using System.Collections.Generic;
using System.Windows.Media;


namespace chkam05.ZtmDataDownloader.Data.Lines
{
    public class LineItem
    {

        //  CONST

        private static readonly Color _defaultColor = Colors.White;
        private static readonly string _updatedAttribute = "btn-line-updated";
        private static readonly string _changedPathColor = "#ffffb3";


        //  VARIABLES

        public List<string> Attributes { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public LineItem Self { get => this; }
        public TransportType Type { get; set; }
        public string URL { get; set; }
        public string Value { get; set; }

        public Line.Line LineDetails { get; set; }


        //  GETTERS & SETTERS

        public bool IsPathChanged
        {
            get => !string.IsNullOrEmpty(Color) && Color.ToLower() == _changedPathColor;
        }

        public bool IsUpdated
        {
            get => Attributes.Contains(_updatedAttribute);
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineItem class constructor. </summary>
        public LineItem()
        {
            if (Attributes == null)
                Attributes = new List<string>();
        }

        #endregion CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Get line color. </summary>
        /// <returns> Line color. </returns>
        public Color GetColor()
        {
            if (string.IsNullOrEmpty(this.Color))
                return _defaultColor;

            try
            {
                return (Color)ColorConverter.ConvertFromString(Color);
            }
            catch
            {
                return _defaultColor;
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            var transportType = TransportTypesMapper.MapToName(Type);

            return $"{transportType} {Value}";
        }

    }
}
