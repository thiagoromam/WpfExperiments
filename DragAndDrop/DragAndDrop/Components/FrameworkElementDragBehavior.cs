using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DragAndDrop.Components
{
    public class FrameworkElementDragBehavior : Behavior<FrameworkElement>
    {
        private bool _isMouseClicked;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseButtonUp;
            AssociatedObject.MouseLeave += OnMouseLeave;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseButtonDown;
            AssociatedObject.MouseLeftButtonUp -= OnMouseButtonUp;
            AssociatedObject.MouseLeave -= OnMouseLeave;

            base.OnDetaching();
        }

        private void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = true;
        }
        private void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = false;
        }
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!_isMouseClicked) return;
            
            var data = AssociatedObject.DataContext;
            var type = (data as IDraggable)?.DataType ?? typeof (object);

            if (data != null)
            {
                var dataObject = new DataObject(type, data);
                DragDrop.DoDragDrop(AssociatedObject, dataObject, DragDropEffects.Move);
            }

            _isMouseClicked = false;
        }
    }
}