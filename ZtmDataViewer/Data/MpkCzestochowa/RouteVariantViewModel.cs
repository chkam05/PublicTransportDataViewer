using MpkCzestochowaDownloader.Data.Line;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MpkCzestochowa
{
    public class RouteVariantViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private RouteVariant _routeVariant;


        //  GETTERS & SETTERS

        public RouteVariant RouteVariant
        {
            get => _routeVariant;
            set
            {
                _routeVariant = value;
                OnPropertyChanged(nameof(RouteVariant));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(IsSelected));
                OnPropertyChanged(nameof(Variant));
            }
        }

        public string Name
        {
            get => _routeVariant.Name;
        }

        public bool IsSelected
        {
            get => _routeVariant.Selected;
        }

        public string? Variant
        {
            get => _routeVariant.Variant;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> RouteVariantViewModel class constructor. </summary>
        /// <param name="routeVariant"> Route variant. </param>
        public RouteVariantViewModel(RouteVariant routeVariant)
        {
            RouteVariant = routeVariant;
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
