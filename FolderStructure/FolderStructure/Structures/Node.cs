using System.Windows;

namespace FolderStructure.Structures
{
    public class Node : DependencyObject
    {
        public static readonly DependencyProperty NameProperty;

        static Node()
        {
            NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Node), new PropertyMetadata(default(string)));
        }
        internal Node(object wrappedObject)
        {
            WrappedObject = wrappedObject;
        }
        internal Node(object wrappedObject, NodeCollection children) : this(wrappedObject)
        {
            IsFolder = true;
            Children = children;
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public bool IsFolder { get; }
        public NodeCollection Children { get; }
        public object WrappedObject { get; }
    }
}