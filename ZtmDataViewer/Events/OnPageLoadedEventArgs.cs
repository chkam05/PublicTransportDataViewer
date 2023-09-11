using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataViewer.Pages;

namespace ZtmDataViewer.Events
{
    public class OnPageLoadedEventArgs : EventArgs
    {

        //  VARIABLES

        public BasePage NowLoadedPage { get; private set; }
        public BasePage PreviousPage { get; private set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> OnPageLoadedEventArgs class constructor. </summary>
        /// <param name="nowLoadedPage"> Now loaded page. </param>
        /// <param name="previousPage"> Previous page. </param>
        public OnPageLoadedEventArgs(BasePage nowLoadedPage, BasePage previousPage)
        {
            NowLoadedPage = nowLoadedPage;
            PreviousPage = previousPage;
        }

        #endregion CLASS METHODS

    }
}
