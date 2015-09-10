﻿using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using FolderStructure.Components;

namespace FolderStructure.Structures
{
    public class NodeCollection : ObservableCollection<Node>
    {
        private readonly INodeSelector _nodeSelector;

        internal NodeCollection(IObservableCollection source, INodeSelector nodeSelector)
        {
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
            foreach (var newItem in items)
                Add(_nodeSelector.Create(newItem));
        }
        private void RemoveItems(IEnumerable items)
        {
            foreach (var oldItem in items)
                Remove(this.Single(n => n.WrappedObject == oldItem));
        }
    }
}