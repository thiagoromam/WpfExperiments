using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DragAndDrop.Unity
{
    public class DroppableBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty DropCommandProperty;
        private bool _canBeDropped;

        static DroppableBehavior()
        {
            DropCommandProperty = DependencyProperty.Register(
                "DropCommand",
                typeof(ICommand),
                typeof(DroppableBehavior),
                new PropertyMetadata(default(ICommand))
            );
        }

        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }
        protected object Data { get; private set; }
        protected DropType DropType { get; private set; }
        protected DropType LastDropType { get; private set; }

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

        protected virtual void OnDragEnter(object sender, DragEventArgs e)
        {
            Data = e.Data.GetData(typeof(object));
            _canBeDropped = Data != null;

            e.Handled = true;
        }
        protected virtual void OnDragOver(object sender, DragEventArgs e)
        {
            if (Data != null && Data != AssociatedObject.DataContext)
            {
                LastDropType = DropType;
                DropType = GetDropType(e);

                if (DropType != LastDropType)
                {
                    if (DropCommand != null)
                        _canBeDropped = DropCommand.CanExecute(GetCommandParameter());
                }
            }

            if (!_canBeDropped)
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }
        protected virtual void OnDragLeave(object sender, DragEventArgs e)
        {
            _canBeDropped = false;
            LastDropType = 0;
            DropType = 0;

            e.Handled = true;
        }
        protected virtual void OnDrop(object sender, DragEventArgs e)
        {
            if (_canBeDropped && Data != AssociatedObject.DataContext)
                DropCommand?.Execute(GetCommandParameter());

            e.Handled = true;
        }

        protected virtual DropType GetDropType(DragEventArgs e)
        {
            return DropType.Inside;
        }
        private object GetCommandParameter()
        {
            return new DropEventArgs(Data, AssociatedObject.DataContext, DropType);
        }
    }
}