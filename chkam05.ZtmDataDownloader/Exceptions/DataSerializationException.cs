using System;


namespace chkam05.ZtmDataDownloader.Exceptions
{
    public class DataSerializationException : Exception
    {

        //  CONST

        private static readonly string _baseMessage = "Could not serialize/deserialize \"{0}\" object data.";


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> DataSerializationException class constructor. </summary>
        /// <param name="dataType"> Type of data being serialized or deserialized. </param>
        /// <param name="additionalMessage"> Additional serialization message. </param>
        public DataSerializationException(Type dataType, string additionalMessage = null)
            : base(BuildMessage(dataType, additionalMessage))
        {
            //
        }

        #endregion CLASS METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Build exception message. </summary>
        /// <param name="dataType"> Type of data being serialized or deserialized. </param>
        /// <param name="additionalMessage"> Additional serialization message. </param>
        /// <returns> Exception message. </returns>
        private static string BuildMessage(Type dataType, string additionalMessage)
        {
            string message = string.Format(_baseMessage, dataType.Name);

            if (!string.IsNullOrEmpty(additionalMessage))
                message += $" {additionalMessage}";

            return message;
        }

        #endregion UTILITY METHODS

    }
}
