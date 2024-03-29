﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZtmDataDownloader.Data.Arrivals;

namespace PublicTransportDataViewer.Data.ZtmData
{
    public class ArrivalViewModel : BaseViewModel
    {

        //  VARIABLES

        private Arrival _arrival;


        //  GETTERS & SETTERS

        public Arrival Arrival
        {
            get => _arrival;
            private set
            {
                _arrival = value;
                OnPropertyChanged(nameof(Arrival));
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(SpecialDistance));
                OnPropertyChanged(nameof(SpecialTime));
                OnPropertyChanged(nameof(Value));
            }
        }

        public Color Color
        {
            get => _arrival.GetColor();
        }

        public string Name
        {
            get => _arrival.Name;
        }

        public string SpecialDistance
        {
            get => _arrival.SpecialDistance;
        }

        public string SpecialTime
        {
            get => _arrival.SpecialTime;
        }

        public string Value
        {
            get => _arrival.Value;
        }

        
        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> ArrivalViewModel class constructor. </summary>
        /// <param name="arrival"> Arrival link data. </param>
        public ArrivalViewModel(Arrival arrival)
        {
            Arrival = arrival;
        }

        #endregion CLASS METHODS

    }
}
