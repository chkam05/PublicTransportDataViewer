using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace chkam05.ZtmDataViewer.Windows
{
    public partial class MainWindow : Window
    {

        //  VARIABLES


        //  GETTERS & SETTERS


        //  METHODS

        #region CLASS METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> MainWindow class constructor. </summary>
        public MainWindow()
        {
            //  Initialize interface components.
            InitializeComponent();
        }

        #endregion CLASS METHODS

        #region WINDOW METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Method invoked after loading window. </summary>
        /// <param name="sender"> Object that invoked an event. </param>
        /// <param name="e"> Routed event arguments. </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }

        #endregion WINDOW METHODS

    }
}
