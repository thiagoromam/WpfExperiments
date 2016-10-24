using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DragAndDrop.Components
{
    public class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty DropCommandProperty;
        private object _data;
        private DropType _lastDropType;
        private DropType _dropType;
        private bool _canBeDropped;
        private FrameworkElementAdorner _adorner;

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

            if (ShowAdorner)
                _adorner = new FrameworkElementAdorner(AssociatedObject);
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
        public double EdgeDropMargin { get; set; } = 4;
        public bool ShowAdorner { get; set; } = true;

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            var dataType = (AssociatedObject.DataContext as IDroppable)?.DataType ?? typeof(object);

            _data = e.Data.GetData(dataType);
            _canBeDropped = true;

            if (_data == AssociatedObject.DataContext)
            {
                _data = null;
                _canBeDropped = false;
            }

            e.Handled = true;
        }
        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (_data != null)
            {
                _lastDropType = _dropType;
                _dropType = GetDropType(e);

                if (_dropType != _lastDropType)
                {
                    _adorner?.Update(_dropType);

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
            _adorner?.Remove();
            e.Handled = true;
        }
        private void OnDrop(object sender, DragEventArgs e)
        {
            if (_canBeDropped)
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
                        droppable.Drop(draggable, _dropType);
                    }
                }
            }

            _adorner?.Remove();
            e.Handled = true;
        }

        private DropType GetDropType(DragEventArgs e)
        {
            var pos = e.GetPosition(AssociatedObject);

            if (pos.Y <= EdgeDropMargin)
                return DropType.Top;

            if (pos.Y >= AssociatedObject.ActualHeight - EdgeDropMargin)
                return DropType.Bottom;

            return DropType.Normal;
        }
        private object GetCommandParameter()
        {
            return new DropEventArgs(_data, AssociatedObject.DataContext, _dropType);
        }
    }
}