using System;
using System.Windows.Data;
using FolderStructure.Components;

namespace FolderStructure.Structures
{
    internal interface INodeSelector
    {
        Node Create(object obj);
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

        public Node Create(object obj)
        {
            Node node = null;

            if (obj is TFolder)
                node = new Node(obj, new NodeCollection(_getFolderCollection((TFolder)obj), this));
            else if (obj is TItem)
                node = new Node(obj);

            if (node == null)
                throw new Exception("Object type not valid.");

            BindingOperations.SetBinding(node, Node.NameProperty, new Binding(_namePath) { Source = obj });

            return node;
        }
    }
}