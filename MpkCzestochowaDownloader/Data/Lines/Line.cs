﻿using MpkCzestochowaDownloader.Data.Static;
using MpkCzestochowaDownloader.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MpkCzestochowaDownloader.Data.Lines
{
    public class Line
    {

        //  VARIABLES

        public List<string> Attributes { get; set; }
        public TransportType TransportType { get; set; }
        public string? URL { get; set; }
        public string Value { get; set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Line class constructor. </summary>
        public Line()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
