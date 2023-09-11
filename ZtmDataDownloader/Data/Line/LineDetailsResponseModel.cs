using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataDownloader.Data.Line
{
    public class LineDetailsResponseModel : BaseResponseModel
    {

        //  VARIABLES

        public bool HasSpecialTimeTables { get; set; }
        public LineDetails Line { get; set; }


        //  GETTERS & SETTERS

        public bool HasData
        {
            get => Line != null;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsResponseModel class constructor. </summary>
        public LineDetailsResponseModel() : base()
        {
            HasSpecialTimeTables = false;
        }

        #endregion CLASS METHODS

    }
}
