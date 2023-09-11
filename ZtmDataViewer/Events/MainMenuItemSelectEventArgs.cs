using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataViewer.Data.MainMenu;

namespace ZtmDataViewer.Events
{
    public class MainMenuItemSelectEventArgs : EventArgs
    {

        //  VARIABLES

        public MainMenuItem ItemSelected { get; private set; }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MainMenuItemSelectEventArgs class constructor. </summary>
        /// <param name="itemSelected"> Main menu item selected. </param>
        public MainMenuItemSelectEventArgs(MainMenuItem itemSelected)
        {
            ItemSelected = itemSelected;
        }

        #endregion CLASS METHODS

    }
}
