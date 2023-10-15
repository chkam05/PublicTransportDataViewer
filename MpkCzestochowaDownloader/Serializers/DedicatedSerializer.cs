using DownloaderCore;
using DownloaderCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpkCzestochowaDownloader.Serializers
{
    public class DedicatedSerializer<T> : BaseSerializer<T> where T : BaseResponseModel
    {

        #region PREPROCESSING DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Find phrase in lines of texts. </summary>
        /// <param name="textLines"> List of texts. </param>
        /// <param name="phrase"> Phrase to find. </param>
        /// <param name="lineStartIndex"> Line start index. </param>
        /// <returns> Tuple of line index, phrase position in line. </returns>
        protected virtual List<(int, int)> FindPhrase(List<string> textLines, string phrase, int lineStartIndex = 0)
        {
            var indexes = new List<(int, int)>();
            var startIndex = Math.Max(0, Math.Min(lineStartIndex, textLines.Count - 1));

            for (int index = startIndex; index < textLines.Count; index++)
            {
                int foundIndex = -1;

                while (true)
                {
                    foundIndex = textLines[index].IndexOf(phrase, foundIndex + 1);

                    if (foundIndex == -1)
                        break;

                    indexes.Add((index, foundIndex));
                }
            }

            return indexes;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fix break line nodes. </summary>
        /// <param name="dataLine"> Raw single line data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        protected virtual bool FixBreakLine(string dataLine, out string correctedDataLine)
        {
            correctedDataLine = dataLine;

            if (dataLine.Contains("<br>"))
            {
                while (correctedDataLine.Contains("<br>"))
                    correctedDataLine = correctedDataLine.Replace("<br>", "<br />");

                return true;
            }

            return false;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fix image nodes. </summary>
        /// <param name="dataLine"> Raw XML data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        protected virtual bool FixImage(string dataLine, out string correctedDataLine)
        {
            correctedDataLine = dataLine;

            if (dataLine.Contains("<img"))
            {
                int startIndex = dataLine.IndexOf("<img");
                int endIndex = dataLine.IndexOf(">", startIndex) + 1;

                string imgNode = dataLine.Substring(startIndex, endIndex - startIndex);

                if (!imgNode.EndsWith("/>"))
                {
                    correctedDataLine = dataLine.Replace(imgNode, imgNode.Replace(">", "/>"));
                    return true;
                }
            }

            return false;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fix input nodes. </summary>
        /// <param name="dataLine"> Raw XML data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        protected virtual bool FixInput(string dataLine, out string correctedDataLine)
        {
            correctedDataLine = dataLine;

            if (dataLine.Contains("<input"))
            {
                int startIndex = dataLine.IndexOf("<input");
                int endIndex = dataLine.IndexOf(">", startIndex) + 1;

                string imgNode = dataLine.Substring(startIndex, endIndex - startIndex);

                if (!imgNode.EndsWith("/>"))
                {
                    correctedDataLine = dataLine.Replace(imgNode, imgNode.Replace(">", "/>"));
                    return true;
                }
            }

            return false;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fix paragraph nodes. </summary>
        /// <param name="dataLine"> Raw single line data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        protected virtual bool FixParagraph(string dataLine, out string correctedDataLine)
        {
            correctedDataLine = dataLine;

            if (dataLine.Contains("<p"))
            {
                int startIndex = dataLine.IndexOf("<p");
                int endIndex = dataLine.IndexOf(">", startIndex) + 1;

                string imgNode = dataLine.Substring(startIndex, endIndex - startIndex);

                if (!imgNode.EndsWith("/>"))
                {
                    correctedDataLine = dataLine.Replace(imgNode, imgNode.Replace(">", "/>"));
                    return true;
                }
            }

            return false;
        }

        #endregion PREPROCESSING DATA METHODS

    }
}
