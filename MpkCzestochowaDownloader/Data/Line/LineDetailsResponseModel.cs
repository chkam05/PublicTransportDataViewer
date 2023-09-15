using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Line
{
    public class LineDetailsResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public LineDetails LineDetails { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => LineDetails != null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsResponseModel class constructor. </summary>
        public LineDetailsResponseModel() : base()
        {
            //
        }

        #endregion CLASS METHODS

    }
}
