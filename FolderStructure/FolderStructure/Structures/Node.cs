using System.Collections.Specialized;
using System.Windows;

namespace FolderStructure.Structures
{
    public class Node : DependencyObject
    {
        public static readonly DependencyProperty NameProperty;
        public static readonly DependencyProperty IsExpandedProperty;

        static Node()
        {
            NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Node), new PropertyMetadata(default(string)));
            IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(Node), new PropertyMetadata(true));
        }
        internal Node(object wrappedObject)
        {
            WrappedObject = wrappedObject;
        }
        internal Node(object wrappedObject, NodeCollection children) : this(wrappedObject)
        {
            IsFolder = true;
            Children = children;

            Children.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    IsExpanded = true;
            };
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public bool IsFolder { get; }
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }
        public NodeCollection Children { get; }
        public object WrappedObject { get; }
    }
}