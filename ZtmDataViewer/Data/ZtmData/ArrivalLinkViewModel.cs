using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Arrivals;
using ZtmDataDownloader.Data.Global;

namespace ZtmDataViewer.Data.ZtmData
{
    public class ArrivalLinkViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private ArrivalLink _arrivalLink;


        //  GETTERS & SETTERS

        public ArrivalLink ArrivalLink
        {
            get => _arrivalLink;
            private set
            {
                _arrivalLink = value;
                OnPropertyChanged(nameof(ArrivalLink));
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Title
        {
            get => _arrivalLink.Title;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalLinkViewModel class constructor. </summary>
        /// <param name="arrivalLink"> Arrival link data. </param>
        public ArrivalLinkViewModel(ArrivalLink arrivalLink)
        {
            ArrivalLink = arrivalLink;
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
