using DownloaderCore.Utilities;
using MpkCzestochowaDownloader.Data.Arrives;
using MpkCzestochowaDownloader.Data.Departures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MpkCzestochowaDownloader.Serializers
{
    public class LineArrivalsSerializer : DedicatedSerializer<LineArrivalsResponseModel>
    {

        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineArrivalsSerializer class constructor. </summary>
        public LineArrivalsSerializer() : base()
        {
            //
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw line arrivals data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized line arrivals data from web response. </returns>
        public override LineArrivalsResponseModel Deserialize(string rawData, params object[] parameters)
        {
            var result = new LineArrivalsResponseModel();
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
                .Replace("selected ", "selected=\"\" ")
                .Replace("<br>", "<br/>");

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

            CorrectInvalidLines(lines);

            return string.Join(string.Empty, lines);
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

        #endregion PREPROCESSING DATA METHODS

    }
}
