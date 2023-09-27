using DownloaderCore;
using DownloaderCore.Utilities;
using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MpkCzestochowaDownloader.Serializers
{
    public class LineDetailsSerializer : BaseSerializer<LineDetailsResponseModel>
    {

        //  CONST

        private static readonly string _startTrimmer = "<section class=\"section\">";
        private static readonly string _endTrimmer = "</section>";


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsSerializer class constructor. </summary>
        public LineDetailsSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw lines data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized lines data from web response. </returns>
        public override LineDetailsResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new LineDetailsResponseModel();
            string preprocessedData = PreprocessData(rawData);
            XElement xmlData = DeserializeToXML(preprocessedData);

            if (xmlData != null)
            {
                //
            }

            if (_errorMessages.Any())
                _errorMessages.ForEach(e => result.AddError(e));

            return result;
        }

        #endregion SERIALIZATION METHODS

        #region PREPROCESSING DATA METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Preprocess raw data recived from web response to be correct XML data. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Preprocessed raw data. </returns>
        private string PreprocessData(string rawData)
        {
            string trimmedData = TrimData(rawData)
                .Trim()
                .Replace("\t", "")
                .Replace("&", "&amp;");

            trimmedData = StringUtils.RemoveExcessSpaces(trimmedData);

            trimmedData = trimmedData
                .Replace("selected ", "selected=\"\" ");

            return trimmedData;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Trim the data to get concrete results. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Trimmed raw data. </returns>
        private string TrimData(string rawData)
        {
            List<string> lines = rawData.Split(new string[] { Environment.NewLine, "\n" },
                StringSplitOptions.RemoveEmptyEntries).ToList();

            var startIndexes = FindPhrase(lines, _startTrimmer);
            var startIndex = startIndexes[1];

            if (startIndex.Item2 > 0)
                lines[startIndex.Item1] = lines[startIndex.Item1].Substring(startIndex.Item2);

            var endIndexes = FindPhrase(lines, _endTrimmer, startIndex.Item1);
            var endIndex = endIndexes[0];

            if (endIndex.Item2 > 0)
                lines[endIndex.Item1] = lines[endIndex.Item1].Substring(0, (endIndex.Item2 + _endTrimmer.Length));

            List<string> dataLines = lines.GetRange(startIndex.Item1, endIndex.Item1 - startIndex.Item1 + 1);
            CorrectInvalidLines(dataLines);

            return string.Join(string.Empty, dataLines);
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Find phrase in lines of texts. </summary>
        /// <param name="textLines"> List of texts. </param>
        /// <param name="phrase"> Phrase to find. </param>
        /// <param name="lineStartIndex"> Line start index. </param>
        /// <returns> Tuple of line index, phrase position in line. </returns>
        private List<(int, int)> FindPhrase(List<string> textLines, string phrase, int lineStartIndex = 0)
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
        /// <summary> Correct lines contains broken data. </summary>
        /// <param name="dataLines"> Raw data as lines. </param>
        private void CorrectInvalidLines(List<string> dataLines)
        {
            for (int index = 0; index < dataLines.Count; index++)
            {
                string dataLine = dataLines[index];

                if (FixImage(dataLine, out string imageCorrected))
                {
                    dataLine = imageCorrected;
                    dataLines[index] = imageCorrected;
                }

                if (FixInput(dataLine, out string inputCorrected))
                {
                    dataLine = inputCorrected;
                    dataLines[index] = inputCorrected;
                }
            }
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Fix image nodes. </summary>
        /// <param name="dataLine"> Raw single line data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        private bool FixImage(string dataLine, out string correctedDataLine)
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
        /// <summary> Fix image nodes. </summary>
        /// <param name="dataLine"> Raw single line data. </param>
        /// <param name="correctedDataLine"> Output corrected line data. </param>
        /// <returns> True - correction applied; False - otherwise. </returns>
        private bool FixInput(string dataLine, out string correctedDataLine)
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

        #endregion PREPROCESSING DATA METHODS

    }
}
