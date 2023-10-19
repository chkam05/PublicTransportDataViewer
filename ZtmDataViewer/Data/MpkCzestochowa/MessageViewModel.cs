using MpkCzestochowaDownloader.Data.Lines;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class MessageViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private Message _message;


        //  GETTERS & SETTERS

        public Message Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(Lines));
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Date
        {
            get => _message.Date?.ToString("yyyy-MM-dd") ?? string.Empty;
        }

        public string Lines
        {
            get => string.Join(", ", _message.Lines);
        }

        public string Value
        {
            get => _message.Value ?? string.Empty;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MessageViewModel class constructor. </summary>
        /// <param name="message"> Message data. </param>
        public MessageViewModel(Message message)
        {
            Message = message;
        }

        #endregion CLASS METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Invoke PropertyChangedEventHandler event method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
