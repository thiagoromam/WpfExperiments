using System;
using System.Windows;
using System.Windows.Interactivity;

namespace DragAndDrop.Components
{
    public class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        private Type _dataType;

        protected override void OnAttached()
        {
            base.OnAttached();

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
            _dataType = (AssociatedObject.DataContext as IDropable)?.DataType;

            e.Handled = true;
        }
        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (_dataType == null || !e.Data.GetDataPresent(_dataType))
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }
        private void OnDragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
        private void OnDrop(object sender, DragEventArgs e)
        {
            if (_dataType != null && e.Data.GetDataPresent(_dataType))
            {
                var dragable = (IDragable)e.Data.GetData(_dataType);
                var dropable = (IDropable)AssociatedObject.DataContext;

                if (dragable == dropable)
                    return;

                dragable.Drag();
                dropable.Drop(dragable);
            }

            e.Handled = true;
        }
    }
}