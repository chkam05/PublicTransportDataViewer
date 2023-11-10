using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloaderCore.Utilities
{
    public static class StringUtils
    {

        //  METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Remove excess spaces from string. </summary>
        /// <param name="str"> String with excess spaces. </param>
        /// <returns> String without excess spaces. </returns>
        public static string RemoveExcessSpaces(string str)
        {
            string strCopy = str;

            while (strCopy.IndexOf("  ") >= 0)
                strCopy = strCopy.Replace("  ", " ");

            return strCopy;
        }

    }
}
