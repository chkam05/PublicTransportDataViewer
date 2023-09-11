using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtmDataViewer.Data.MainMenu
{
    public class MainMenuItem : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<EventArgs> Action;


        //  VARIABLES

        private PackIconKind _iconKind = PackIconKind.None;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;


        //  GETTERS & SETTERS

        public PackIconKind IconKind
        {
            get => _iconKind;
            set
            {
                _iconKind = value;
                OnPropertyChanged(nameof(IconKind));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Subtitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value;
                OnPropertyChanged(nameof(Subtitle));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MainMenuItem class constructor. </summary>
        public MainMenuItem()
        {
            //
        }

        //  --------------------------------------------------------------------------------
        /// <summary> MainMenuItem class constructor. </summary>
        /// <param name="title"> Menu item title. </param>
        /// <param name="iconKind"> Menu item icon. </param>
        /// <param name="selectAction"> Menu item select action. </param>
        public MainMenuItem(string title, PackIconKind iconKind = PackIconKind.None, 
            EventHandler<EventArgs>? selectAction = null, string subtitle = "")
        {
            Title = title;
            IconKind = iconKind;
            Subtitle = subtitle;

            if (selectAction != null)
                Action += selectAction;
        }

        #endregion CLASS METHODS

        #region ACTION METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Invoke action method assigned to the menu item. </summary>
        public void InvokeAction()
        {
            Action?.Invoke(this, EventArgs.Empty);
        }

        #endregion ACTION METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method for invoking PropertyChangedEventHandler event. </summary>
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
