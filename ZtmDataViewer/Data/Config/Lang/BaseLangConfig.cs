using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.Config.Lang
{
    public class BaseLangConfig : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LangConfig class constructor. </summary>
        [JsonConstructor]
        public BaseLangConfig()
        {
            //
        }

        #endregion CLASS METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method for invoking PropertyChangedEventHandler external method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region PROPERTIES HELPER

        //  --------------------------------------------------------------------------------
        /// <summary> Get language value or default value instead. </summary>
        /// <param name="value"> Language value. </param>
        /// <param name="defaultValue"> Default language value. </param>
        protected virtual string SetLanguageValue(string? value, string defaultValue)
        {
            return !string.IsNullOrWhiteSpace(value) ? value : defaultValue;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Set property value helper method. </summary>
        /// <param name="property"> Private property name. </param>
        /// <param name="name"> Public property name. </param>
        /// <param name="value"> Value to set. </param>
        protected virtual void SetStringProperty(ref string property, string name, string value)
        {
            if (property != null)
            {
                property = value;
                OnPropertyChanged(name);
            }
        }

        #endregion PROPERTIES HELPER

    }
}
