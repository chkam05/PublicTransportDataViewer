using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Departures;
using ZtmDataDownloader.Data.Global;
using static System.Formats.Asn1.AsnWriter;

namespace PublicTransportDataViewer.Data.ZtmData
{
    public class LineDepartureGroupViewModel : BaseViewModel
    {

        //  VARIABLES

        private DepartureGroup _departureGroup;
        private ObservableCollection<LineDepartureViewModel> _departures;


        //  GETTERS & SETTERS

        public DepartureGroup DepartureGroup
        {
            get => _departureGroup;
            private set
            {
                _departureGroup = value;
                OnPropertyChanged(nameof(DepartureGroup));
                OnPropertyChanged(nameof(Description));
            }
        }

        public ObservableCollection<LineDepartureViewModel> Departures
        {
            get => _departures;
            private set
            {
                _departures = value;
                AddCollectionChangedMethod(_departures, nameof(Departures));
                OnPropertyChanged(nameof(Departures));
            }
        }

        public string Description
        {
            get => _departureGroup.Description;
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDepartureGroupViewModel class constructor. </summary>
        /// <param name="departureGroup"> Departure group data. </param>
        /// <param name="departures"> Departures data. </param>
        public LineDepartureGroupViewModel(DepartureGroup departureGroup, List<Departure> departures)
        {
            DepartureGroup = departureGroup;
            Departures = new ObservableCollection<LineDepartureViewModel>(
                departures.Select(d => new LineDepartureViewModel(d)));
        }

        #endregion CLASS METHODS

    }
}
