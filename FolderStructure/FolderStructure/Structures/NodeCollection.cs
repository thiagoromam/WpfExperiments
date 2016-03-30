using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using FolderStructure.Components;

namespace FolderStructure.Structures
{
    public class NodeCollection : ObservableCollection<Node>, IDisposable
    {
        private readonly Node _node;
        private readonly INodeSelector _nodeSelector;

        internal NodeCollection(Node node, IObservableCollection source, INodeSelector nodeSelector)
        {
            _node = node;
            _nodeSelector = nodeSelector;

            AddItems(source);
            source.CollectionChanged += OnSourceCollectionChanged;
        }

        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                AddItems(e.NewItems);

            if (e.OldItems != null)
                RemoveItems(e.OldItems);
        }

        private void AddItems(IEnumerable items)
        {
            foreach (INotifyPropertyChanged newItem in items)
                Add(_nodeSelector.Create(_node, newItem));
        }
        private void RemoveItems(IEnumerable items)
        {
            foreach (var oldItem in items)
            {
                var item = this.Single(n => Equals(n.WrappedObject, oldItem));
                Remove(item);
                item.Dispose();
            }
        }

        public void Dispose()
        {
            foreach (var item in Items)
                item.Dispose();
        }
    }
}