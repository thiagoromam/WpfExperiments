using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FolderStructure.Components
{
    public interface IObservableCollection : IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
    {
    }

    public interface IObservableCollection<T> : IEnumerable<T>, IObservableCollection
    {
    }
}