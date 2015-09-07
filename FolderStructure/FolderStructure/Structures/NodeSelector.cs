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
        private readonly Func<TFolder, IObservableCollection> _getFolderCollection;
        private readonly string _namePath;

        public NodeSelector(string namePath, Func<TFolder, IObservableCollection> getFolderCollection)
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