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
    public class RouteVariantViewModel : BaseViewModel
    {

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
            }
        }

        public string Name
        {
            get => _routeVariant.Name;
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

    }
}
