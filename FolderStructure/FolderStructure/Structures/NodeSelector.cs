using System.ComponentModel;
using System.Windows.Data;
using FolderStructure.Components;

namespace FolderStructure.Structures
{
    internal interface INodeSelector
    {
        Node Create(Node parent, INotifyPropertyChanged obj);
    }

    internal class NodeSelector<TFolder, TItem> : INodeSelector
    {
        public delegate IObservableCollection GetFolderCollection(TFolder folder);

        private readonly string _namePath;
        private readonly GetFolderCollection _getFolderCollection;

        public NodeSelector(string namePath, GetFolderCollection getFolderCollection)
        {
            _namePath = namePath;
            _getFolderCollection = getFolderCollection;
        }

        public Node Create(Node parent, INotifyPropertyChanged obj)
        {
            var node = new Node(parent, obj);

            if (obj is TFolder)
                node.Children = new NodeCollection(node, _getFolderCollection((TFolder)obj), this);

            BindingOperations.SetBinding(node, Node.NameProperty, new Binding(_namePath) { Source = obj });

            return node;
        }
    }
}