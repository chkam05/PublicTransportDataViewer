using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Events
{
    public static class EventsDefinitions
    {

        //  MENUS

        public delegate void MainMenuItemSelectEventHandler(object sender, MainMenuItemSelectEventArgs e);


        //  PAGES

        public delegate void PagesManagerNavigatedEventHandler(object sender, OnPageLoadedEventArgs e);

    }
}
