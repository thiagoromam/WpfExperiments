using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DragAndDrop.Unity
{
    public class DraggableBehavior : Behavior<FrameworkElement>
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

            if (AssociatedObject.DataContext != null)
            {
                var dataObject = new DataObject(typeof(object), AssociatedObject.DataContext);
                DragDrop.DoDragDrop(AssociatedObject, dataObject, DragDropEffects.Move);
            }

            _isMouseClicked = false;
        }
    }
}