using System.Windows;
using System.Windows.Controls;

namespace DragAndDrop.Unity
{
    public class TreeViewItemDroppableBehavior : DroppableBehavior
    {
        private TreeViewItem _treeViewItem;

        internal TreeViewItemAdorner Adorner { get; private set; }
        public double EdgeDropMargin { get; set; } = 4;

        protected override void OnAttached()
        {
            base.OnAttached();
            
            _treeViewItem = AssociatedObject.ParentsUntil<TreeViewItem>();
            Adorner = new TreeViewItemAdorner(AssociatedObject);
        }

        protected override void OnDragOver(object sender, DragEventArgs e)
        {
            base.OnDragOver(sender, e);

            if (DropType != LastDropType)
                Adorner.Update(DropType);
        }
        protected override void OnDragLeave(object sender, DragEventArgs e)
        {
            base.OnDragLeave(sender, e);

            Adorner.Remove();
        }
        protected override void OnDrop(object sender, DragEventArgs e)
        {
            base.OnDrop(sender, e);
            
            Adorner.Remove();
        }

        protected override DropType GetDropType(DragEventArgs e)
        {
            var pos = e.GetPosition(AssociatedObject);

            if (pos.Y <= EdgeDropMargin)
                return DropType.Above;

            if (pos.Y < AssociatedObject.ActualHeight - EdgeDropMargin)
                return DropType.Inside;

            if (_treeViewItem.IsExpanded && _treeViewItem.Items.Count > 0)
                return DropType.InsideOnTop;

            return DropType.Bellow;
        }
    }
}