using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicTransportDataViewer.Data
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        //  EVENTS

        public event PropertyChangedEventHandler? PropertyChanged;


        //  METHODS

        #region NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Invoke PropertyChangedEventHandler event method. </summary>
        /// <param name="propertyName"> Changed property name. </param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion NOTIFY PROPERTIES CHANGED INTERFACE METHODS

        #region UTILITY METHODS

        //  --------------------------------------------------------------------------------
        /// <summary> Add collection changed method to observable collection. </summary>
        /// <typeparam name="T"> Observable collection object type. </typeparam>
        /// <param name="observableCollection"> Observable collection. </param>
        /// <param name="propertyName"> Property name. </param>
        protected void AddCollectionChangedMethod<T>(ObservableCollection<T> observableCollection, string propertyName)
            where T : class
        {
            observableCollection.CollectionChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(propertyName));
            };
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Add collection changed method to observable collection. </summary>
        /// <typeparam name="TKey"> Observable collection KeyValuePair key object type. </typeparam>
        /// <typeparam name="TValue"> Observable collection KeyValuePair value object type. </typeparam>
        /// <param name="observableCollection"> Observable collection. </param>
        /// <param name="propertyName"> Property name. </param>
        protected void AddCollectionChangedMethod<TKey, TValue>(
            ObservableCollection<KeyValuePair<TKey,TValue>> observableCollection, string propertyName)
        {
            observableCollection.CollectionChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(propertyName));
            };
        }

        //  --------------------------------------------------------------------------------
        /// <summary> Check if PropertyChanged has a registered method. </summary>
        /// <param name="method"> Method. </param>
        /// <returns> True - PropertyChanged has a registered method; False - otherwise. </returns>
        public bool HasPropertyChangedRegisteredMethod(Delegate method)
        {
            return PropertyChanged?.GetInvocationList()
                .Any(h => h == method) ?? false;
        }

        #endregion UTILITY METHODS

    }
}
