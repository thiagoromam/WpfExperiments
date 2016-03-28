using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FolderStructure.Components
{
    public interface IObservableCollection : IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
    {
    }

    public interface IReadOnlyObservableCollection<T> : IEnumerable<T>, IObservableCollection
    {
    }

    public interface IObservableCollection<T> : IReadOnlyObservableCollection<T>
    {
        void Add(T t);
        bool Remove(T t);
    }
}