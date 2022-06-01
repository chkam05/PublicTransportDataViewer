
namespace chkam05.ZtmDataDownloader.Utilities
{
    internal class StringUtils
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
