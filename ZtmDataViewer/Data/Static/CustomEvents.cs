using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicTransportDataViewer.Data.Config.Lang;
using PublicTransportDataViewer.Data.MainMenu;

namespace PublicTransportDataViewer.Data.Static
{
    public static class CustomEvents
    {

        //  DELEGATES

        public delegate void LanguageUpdateEventHandler(LangConfig languageConfig);

        public delegate void HeaderPageForceUpdateEventHandler(PackIconKind icon, string title);
        public delegate void MainMenuItemsForceUpdateEventHandler(List<MainMenuItem> mainMenuItems);

    }
}
