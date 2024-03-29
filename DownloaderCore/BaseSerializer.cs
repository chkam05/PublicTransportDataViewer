﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using DownloaderCore.Data;

namespace DownloaderCore
{
    public class BaseSerializer<TResultObject> where TResultObject : BaseResponseModel
    {

        //  VARIABLES

        protected List<string> _errorMessages;


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> BaseSerializer class constructor. </summary>
        public BaseSerializer()
        {
            _errorMessages = new List<string>();
        }

        #endregion CLASS METHODS

        #region SERIALIZATION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Deserialize raw data recived from web response. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> Deserialized response. </returns>
        public virtual TResultObject Deserialize(string rawData, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Deserializa raw data to XML structure. </summary>
        /// <param name="rawData"> Raw data recived from web response. </param>
        /// <returns> XML data structure. </returns>
        protected XElement DeserializeToXML(string rawData)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rawData);
                return XElement.Load(new XmlNodeReader(doc));
            }
            catch (XmlException exc)
            {
                _errorMessages.Add($"An error occurred while deserializing the data from XML. {exc.Message}");
                return null;
            }
        }

        #endregion SERIALIZATION METHODS

    }
}
