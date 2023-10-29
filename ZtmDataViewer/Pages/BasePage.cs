using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZtmDataViewer.Components;
using ZtmDataViewer.Data.MainMenu;

namespace ZtmDataViewer.Pages
{
    public class BasePage : Page, INotifyPropertyChanged
    {

        //  DEPENDENCY PROPERTIES

        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register(
            nameof(HeaderContent),
            typeof(object),
            typeof(BasePage),
            new PropertyMetadata(null));


        //  EVENTS

        public event PropertyChangedEventHandler PropertyChanged;


        //  VARIABLES

        private PackIconKind _iconKind = PackIconKind.None;
        protected PagesController _pagesController;

        public virtual List<MainMenuItem> MainMenuItems { get; }


        //  GETTERS & SETTERS

        public object HeaderContent
        {
            get => GetValue(HeaderContentProperty);
            set
            {
                SetValue(HeaderContentProperty, value);
                OnPropertyChanged(nameof(HeaderContent));
            }
        }

        public PackIconKind IconKind
        {
            get => _iconKind;
            set
            {
                _iconKind = value;
                OnPropertyChanged(nameof(IconKind));
            }
        }


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> BasePage class constructor. </summary>
        /// <param name="pagesController"> Pages controller. </param>
        public BasePage(PagesController pagesController)
        {
            _pagesController = pagesController;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Static BasePage class constructor. </summary>
        static BasePage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BasePage),
                new FrameworkPropertyMetadata(typeof(BasePage)));
        }

        #endregion CLASS METHODS

        #region INTERACTIONS WITH PAGES MANAGER METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked by PagesManager when GoBack is called. </summary>
        /// <param name="previousPage"> Page to return to. </param>
        /// <returns> True - allow to go back; False - otherwise. </returns>
        public virtual bool OnGoBackFromPage(BasePage previousPage)
        {
            return true;
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked by PagesManager when Load(another)Page is called. </summary>
        /// <param name="pageToLoad"> Page to load. </param>
        /// <returns> True - allow to load another page; False - otherwise. </returns>
        public virtual bool OnGoForwardFromPage(BasePage pageToLoad)
        {
            return true;
        }

        #endregion INTERACTIONS WITH PAGES MANAGER METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method for invoking PropertyChangedEventHandler external method. </summary>
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
