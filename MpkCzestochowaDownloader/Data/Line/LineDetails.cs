﻿using MpkCzestochowaDownloader.Data.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class LineDetails
    {

        //  VARIABLES

        public List<string> Attributes { get; set; }
        public List<TimeTableDate> Dates { get; set; }
        public string? DirectionFrom { get; set; }
        public string? DirectionTo { get; set; }
        public List<LineDirection> Directions { get; set; }
        public string? LineId { get; set; }
        public List<RouteVariant> RouteVariants { get; set; }
        public TransportType TransportType { get; set; }
        public string? Value { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetails class constructor. </summary>
        public LineDetails()
        {
            if (Attributes == null)
                Attributes = new List<string>();

            if (Dates == null)
                Dates = new List<TimeTableDate>();

            if (Directions == null)
                Directions = new List<LineDirection>();

            if (RouteVariants == null)
                RouteVariants = new List<RouteVariant>();
        }

        #endregion CLASS METHODS

    }
}
