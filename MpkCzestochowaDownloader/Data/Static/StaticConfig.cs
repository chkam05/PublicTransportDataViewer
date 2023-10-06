using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Data.Static
{
    public static class StaticConfig
    {

        //  VARIABLES

        public static readonly string BaseURL = @"https://www.czestochowa.pl/";
        public static readonly string LinesURL = @"https://www.czestochowa.pl/rozklady-jazdy";
        public static readonly string DeparturesURL = @"https://www.czestochowa.pl/rozklady-jazdy?linia=1&przystanek=70-FIELDORFA-NILA&kierunek=0-KUCELIN&trasa=1490";
        public static readonly string ArrivesURL = @"https://www.czestochowa.pl/timetables/trip/stops?t(data-trip)=4236&ft(data-time)=04:33:00&dt(date)=2023-10-06";

    }
}
