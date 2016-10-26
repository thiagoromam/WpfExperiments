using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DragAndDrop.Unity
{
    public class TreeViewItemDroppableBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty DropCommandProperty;
        private TreeViewItem _treeViewItem;
        private object _data;
        private DropType _dropType;
        private DropType _lastDropType;
        private bool _canBeDropped;
        private bool _allowDropAbove;

        static TreeViewItemDroppableBehavior()
        {
            DropCommandProperty = DependencyProperty.Register(
                "DropCommand",
                typeof(ICommand),
                typeof(TreeViewItemDroppableBehavior),
                new PropertyMetadata(default(ICommand))
            );
        }

        internal TreeViewItemAdorner Adorner { get; private set; }
        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }
        public double EdgeDropMargin { get; set; } = 4;

        protected override void OnAttached()
        {
            base.OnAttached();

            Adorner = new TreeViewItemAdorner(AssociatedObject);
            _treeViewItem = AssociatedObject.ParentsUntil<TreeViewItem>();

            var treeView = _treeViewItem.ParentsUntil<TreeView>();
            _allowDropAbove = Equals(treeView.ItemContainerGenerator.ContainerFromIndex(0), _treeViewItem);

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragEnter += OnDragEnter;
            AssociatedObject.DragOver += OnDragOver;
            AssociatedObject.DragLeave += OnDragLeave;
            AssociatedObject.Drop += OnDrop;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.DragEnter -= OnDragEnter;
            AssociatedObject.DragOver -= OnDragOver;
            AssociatedObject.DragLeave -= OnDragLeave;
            AssociatedObject.Drop -= OnDrop;

            base.OnDetaching();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            _data = e.Data.GetData(typeof(object));
            _canBeDropped = _data != null;

            e.Handled = true;
        }
        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (_data != null && _data != AssociatedObject.DataContext)
            {
                _lastDropType = _dropType;
                _dropType = GetDropType(e);

                if (_dropType != _lastDropType)
                {
                    Adorner.Update(_dropType);

                    if (DropCommand != null)
                        _canBeDropped = DropCommand.CanExecute(GetCommandParameter());
                }
            }

            if (!_canBeDropped)
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }
        private void OnDragLeave(object sender, DragEventArgs e)
        {
            _canBeDropped = false;
            _lastDropType = 0;
            _dropType = 0;
            Adorner.Remove();
            e.Handled = true;
        }
        private void OnDrop(object sender, DragEventArgs e)
        {
            if (_canBeDropped && _data != AssociatedObject.DataContext)
                DropCommand?.Execute(GetCommandParameter());

            Adorner.Remove();
            e.Handled = true;
        }

        private DropType GetDropType(DragEventArgs e)
        {
            var pos = e.GetPosition(AssociatedObject);

            if (_allowDropAbove && pos.Y <= EdgeDropMargin)
                return DropType.Above;

            if (pos.Y >= AssociatedObject.ActualHeight - EdgeDropMargin)
                return DropType.Bellow;

            return DropType.Inside;
        }
        private object GetCommandParameter()
        {
            return new DropEventArgs(_data, AssociatedObject.DataContext, _dropType);
        }
    }
}