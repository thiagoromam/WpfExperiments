using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using FolderStructure.Components;
using FolderStructure.Helpers;

namespace FolderStructure.Structures
{
    public abstract class FolderStructureCollection<TNode, TFolder, TItem> : IObservableCollection<Node>
        where TFolder : TNode
        where TItem : TNode
    {
        private NodeCollection _nodes;

        private NodeCollection Nodes
        {
            get
            {
                if (_nodes == null)
                {
                    var namePath = ExpressionHelper.GetExpressionText(GetNameExpression());
                    var selector = new NodeSelector<TFolder, TItem>(namePath, GetChildrenFromFolder);
                    _nodes = new NodeCollection(GetSource(), selector);
                }

                return _nodes;
            }
        }

        protected abstract IObservableCollection<TNode> GetSource();
        protected abstract Expression<Func<TNode, string>> GetNameExpression();
        protected abstract IObservableCollection<TNode> GetChildrenFromFolder(TFolder folder);

        #region IObservableCollection

        public IEnumerator<Node> GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { Nodes.CollectionChanged += value; }
            remove { Nodes.CollectionChanged -= value; }
        }
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { ((INotifyPropertyChanged)Nodes).PropertyChanged += value; }
            remove { ((INotifyPropertyChanged)Nodes).PropertyChanged -= value; }
        }

        #endregion
    }
}