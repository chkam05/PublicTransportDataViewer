using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZtmDataDownloader.Data.Line;
using ZtmDataDownloader.Data.Lines;
using ZtmDataDownloader.Data.Static;

namespace ZtmDataViewer.Data.ZtmData
{
    public class LineDetailsViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private Line _line;
        private LineDetails _lineDetails;
        private string? _timeTableId;
        private ObservableCollection<LineDirectionViewModel> _directions;
        private ObservableCollection<MessageViewModel> _messages;


        //  GETTERS & SETTERS

        public Line Line
        {
            get => _line;
            private set
            {
                _line = value;
                OnPropertyChanged(nameof(Line));
            }
        }

        public LineDetails LineDetails
        {
            get => _lineDetails;
            set
            {
                _lineDetails = value;
                OnPropertyChanged(nameof(LineDetails));
                OnPropertyChanged(nameof(Description));
            }
        }

        public string? TimeTableId
        {
            get => _timeTableId;
            private set
            {
                _timeTableId = value;
                OnPropertyChanged(nameof(TimeTableId));
            }
        }

        public string Description
        {
            get => _lineDetails.Description;
        }

        public TransportType TransportType
        {
            get => _lineDetails.TransportType;
        }

        public ObservableCollection<LineDirectionViewModel> Directions
        {
            get => _directions;
            private set
            {
                _directions = value;
                _directions.CollectionChanged += OnDirectionsCollectionChanged;
                OnPropertyChanged(nameof(Directions));
            }
        }

        public ObservableCollection<MessageViewModel> Messages
        {
            get => _messages;
            private set
            {
                _messages = value;
                _messages.CollectionChanged += OnMessagesCollectionChanged;
                OnPropertyChanged(nameof(Messages));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> LineDetailsViewModel class constructor. </summary>
        /// <param name="line"> Line data. </param>
        /// <param name="lineDetails"> Line details data. </param>
        /// <param name="timeTableId"> Time table id. </param>
        public LineDetailsViewModel(Line line, LineDetails lineDetails, string? timeTableId = null)
        {
            Line = line;
            LineDetails = lineDetails;
            TimeTableId = timeTableId;

            Directions = new ObservableCollection<LineDirectionViewModel>(
                lineDetails.Directions.Select(d => new LineDirectionViewModel(d)));

            if (lineDetails.HasMessages)
                Messages = new ObservableCollection<MessageViewModel>(
                    lineDetails.Messages.Select(m => new MessageViewModel(m)));
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

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after messages collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnMessagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Messages));
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after directions collection changed. </summary>
        /// <param name="sender"> Object that invoked the method. </param>
        /// <param name="e"> Notify Collection Changed Event Arguments. </param>
        private void OnDirectionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Directions));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

    }
}
