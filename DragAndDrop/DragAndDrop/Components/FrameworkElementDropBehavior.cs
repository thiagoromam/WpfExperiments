using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DragAndDrop.Components
{
    public class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty DropCommandProperty;
        private object _data;

        static FrameworkElementDropBehavior()
        {
            DropCommandProperty = DependencyProperty.Register(
                "DropCommand",
                typeof(ICommand),
                typeof(FrameworkElementDropBehavior),
                new PropertyMetadata(default(ICommand))
            );
        }

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

        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            var dataType = (AssociatedObject.DataContext as IDroppable)?.DataType ?? typeof(object);

            _data = e.Data.GetData(dataType);

            if (_data == AssociatedObject.DataContext)
                _data = null;

            if (!DropCommand?.CanExecute(GetCommandParameter()) ?? false)
                _data = null;

            e.Handled = true;
        }
        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (_data == null)
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }
        private void OnDragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
        private void OnDrop(object sender, DragEventArgs e)
        {
            if (_data != null)
            {
                if (DropCommand != null)
                {
                    DropCommand.Execute(GetCommandParameter());
                }
                else
                {
                    var draggable = _data as IDraggable;
                    var droppable = AssociatedObject.DataContext as IDroppable;

                    if (draggable != null && droppable != null)
                    {
                        draggable.Drag();
                        droppable.Drop(draggable);
                    }
                }
            }

            e.Handled = true;
        }

        private object GetCommandParameter()
        {
            return new[] {_data, AssociatedObject.DataContext};
        }
    }
}