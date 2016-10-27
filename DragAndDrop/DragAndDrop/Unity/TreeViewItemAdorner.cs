using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DragAndDrop.Unity
{
    public class TreeViewItemAdorner : Adorner
    {
        private readonly Border _treeViewItemBorder;
        private readonly TreeViewItemAdorner _parentAdorner;
        private readonly bool _isParentFirstElement;
        private AdornerLayer _layer;
        private DropType _dropType;

        public TreeViewItemAdorner(UIElement adornedElement) : base(adornedElement)
        {
            var treeViewItem = adornedElement.ParentsUntil<TreeViewItem>();
            var parentTreeViewItem = treeViewItem.ParentsUntil<TreeViewItem>();

            _treeViewItemBorder = treeViewItem.FindChild<Border>("Bd");
            _parentAdorner = parentTreeViewItem?.FindBehaviorInChildren<TreeViewItemDroppableBehavior>().Adorner;
            _isParentFirstElement = parentTreeViewItem?.Items[0] == treeViewItem.DataContext;

            IsHitTestVisible = false;
            BorderBrush = Helpers.FindResource<SolidColorBrush>("DroppableAdornerBorderBrush");
            BackgroundBrush = Helpers.FindResource<SolidColorBrush>("DroppableAdornerBackgroundBrush");
            LineBrush = Helpers.FindResource<SolidColorBrush>("DroppableAdornerLineBrush");
        }

        public void Update(DropType dropType)
        {
            if (_layer == null)
            {
                _layer = AdornerLayer.GetAdornerLayer(AdornedElement);
                _layer.Add(this);
            }

            _parentAdorner?.Remove();
            _dropType = dropType;
            _layer.Update(AdornedElement);
            Visibility = Visibility.Visible;
        }
        public void Remove()
        {
            _dropType = 0;
            _parentAdorner?.Remove();
            Visibility = Visibility.Collapsed;
        }

        public SolidColorBrush BorderBrush { get; set; }
        public SolidColorBrush BackgroundBrush { get; set; }
        public SolidColorBrush LineBrush { get; set; }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (_dropType == DropType.Inside)
            {
                DrawBox(drawingContext);
            }
            else if (_dropType == DropType.InsideOnTop)
            {
                DrawBox(drawingContext);
                DrawLine(drawingContext, true);
            }
            else if (_dropType == DropType.Bellow)
            {
                _parentAdorner?.Update(DropType.Inside);
                DrawLine(drawingContext, true);
            }
            else if (_dropType == DropType.Above)
            {
                if (_isParentFirstElement)
                {
                    _parentAdorner.Update(DropType.InsideOnTop);
                }
                else
                {
                    _parentAdorner?.Update(DropType.Inside);
                    DrawLine(drawingContext, false);
                }
            }
        }
        private void DrawBox(DrawingContext drawingContext)
        {
            var rect = new Rect(_treeViewItemBorder.DesiredSize);
            var pen = new Pen(BorderBrush, 2);

            drawingContext.DrawRoundedRectangle(BackgroundBrush, pen, rect, 2, 2);
        }
        private void DrawLine(DrawingContext drawingContext, bool onBottom)
        {
            var rect = new Rect(AdornedElement.RenderSize);
            var start = new Point(rect.Left, onBottom ? rect.Bottom : rect.Top);
            var end = new Point(Math.Max(70, rect.Width), start.Y);
            var pen = new Pen(LineBrush, 2);

            drawingContext.DrawLine(pen, start, end);
            drawingContext.DrawEllipse(LineBrush, pen, start, 2, 2);
        }
    }
}