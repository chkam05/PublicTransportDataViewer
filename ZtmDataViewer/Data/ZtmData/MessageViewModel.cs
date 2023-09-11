using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Global;
using ZtmDataDownloader.Data.Line;

namespace ZtmDataViewer.Data.ZtmData
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
            private set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(DateDescription));
                OnPropertyChanged(nameof(HasStartDate));
                OnPropertyChanged(nameof(HasEndDate));
                OnPropertyChanged(nameof(MessageText));
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public string DateDescription
        {
            get => _message.DateDescription;
        }

        public bool HasDates
        {
            get => _message.StartDate.HasValue || _message.EndDate.HasValue;
        }

        public bool HasStartDate
        {
            get => _message.StartDate.HasValue;
        }

        public bool HasEndDate
        {
            get => _message.EndDate.HasValue;
        }

        public string MessageText
        {
            get => _message.MessageText;
        }

        public string? StartDate
        {
            get => _message.StartDate?.ToString("yyyy.MM.dd");
        }

        public string? EndDate
        {
            get => _message.EndDate?.ToString("yyyy.MM.dd");
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MessageViewModel class constructor. </summary>
        /// <param name="message"> Line message data. </param>
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
