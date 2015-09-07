using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FolderStructure.Annotations;

namespace FolderStructure.Components
{
    public class NotificationCollectionWrapper<T> : IObservableCollection<T>
    {
        private readonly ObservableCollection<T> _collection;

        public NotificationCollectionWrapper()
        {
            _collection = new ObservableCollection<T>();
        }

        public virtual void Add(T t)
        {
            _collection.Add(t);
        }
        public virtual void Remove(T t)
        {
            _collection.Remove(t);
        }

        #region ObservableCollection

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { _collection.CollectionChanged += value; }
            remove { _collection.CollectionChanged -= value; }
        }
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _propertyChanged += value;
                ((INotifyPropertyChanged)_collection).PropertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
                ((INotifyPropertyChanged)_collection).PropertyChanged -= value;
            }
        }

        #endregion
        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
        #region INotifyPropertyChanged

        // ReSharper disable once InconsistentNaming
        private event PropertyChangedEventHandler _propertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}