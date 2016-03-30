using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using FolderStructure.Components;

namespace FolderStructure.Structures
{
    public class Node : ExtendedDependencyObject, IDisposable
    {
        public static readonly DependencyProperty NameProperty;

        private readonly Node _parent;
        private bool _isExpanded;
        private NodeCollection _children;

        static Node()
        {
            NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Node), new PropertyMetadata(default(string)));
        }
        public Node(Node parent, INotifyPropertyChanged wrappedObject)
        {
            _parent = parent;
            WrappedObject = wrappedObject;

            wrappedObject.PropertyChanged += OnWrappedObjectPropertyChanged;
        }
        
        public INotifyPropertyChanged WrappedObject { get; }
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value == _isExpanded)
                    return;

                _isExpanded = value;
                OnPropertyChanged();
            }
        }
        public NodeCollection Children
        {
            get { return _children; }
            internal set
            {
                if (_children == null)
                {
                    _children = value;
                    _children.CollectionChanged += OnChildrenCollectionChanged;
                }
            }
        }
        public bool IsFolder => Children != null;

        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                IsExpanded = true;
        }
        private void OnWrappedObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(WrappedObject));
            _parent?.OnWrappedObjectPropertyChanged(null, e);
        }
        
        public void Dispose()
        {
            WrappedObject.PropertyChanged -= OnWrappedObjectPropertyChanged;

            if (IsFolder)
            {
                Children.CollectionChanged -= OnChildrenCollectionChanged;
                Children.Dispose();
            }
        }
    }
}