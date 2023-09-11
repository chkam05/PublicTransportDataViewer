using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using ZtmDataDownloader.Data.Static;
using ZtmDataDownloader.Mappers;

namespace ZtmDataDownloader.Data.Lines
{
    public class Line
    {

        //  CONST

        private static readonly Color _defaultColor = Colors.White;
        private static readonly string _updatedAttribute = "btn-line-updated";
        private static readonly string _changedPathColor = "#ffffb3";


        //  VARIABLES

        public List<string> Attributes { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public TransportType Type { get; set; }
        public string URL { get; set; }
        public string Value { get; set; }


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
        /// <summary> Line class constructor. </summary>
        public Line()
        {
            if (Attributes == null)
                Attributes = new List<string>();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Return value as string data. </summary>
        /// <returns> Value as string. </returns>
        public override string ToString()
        {
            var transportType = TransportTypesMapper.MapToName(Type);

            return $"{transportType} {Value}";
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

    }
}
