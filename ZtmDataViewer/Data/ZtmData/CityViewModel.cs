using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZtmDataDownloader.Data.Global;

namespace ZtmDataViewer.Data.ZtmData
{
    public class CityViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private City _city;


        //  GETTERS & SETTERS

        public City City
        {
            get => _city;
            private set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Name));
            }
        }

        public Color Color
        {
            get => _city.GetColor();
        }

        public string Name
        {
            get => _city.Name;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> CityViewModel class constructor. </summary>
        /// <param name="city"> City data. </param>
        public CityViewModel(City city)
        {
            City = city;
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
