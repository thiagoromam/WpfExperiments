using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace DragAndDrop.Unity
{
    public class TreeViewItemAdorner : Adorner
    {
        private readonly Border _treeViewItemBorder;
        private readonly TreeViewItemAdorner _parentAdorner;
        private AdornerLayer _layer;
        private DropType _dropType;

        public TreeViewItemAdorner(UIElement adornedElement) : base(adornedElement)
        {
            var treeViewItem = adornedElement.ParentsUntil<TreeViewItem>();
            var parentTreeViewItem = treeViewItem.ParentsUntil<TreeViewItem>();
            var partHeader = parentTreeViewItem?.FindChild<ContentPresenter>("PART_Header");
            var partHeaderChild = partHeader?.GetChildren().First();

            _treeViewItemBorder = treeViewItem.FindChild<Border>("Bd");
            _parentAdorner = partHeaderChild?.GetBehavior<TreeViewItemDroppableBehavior>().Adorner;

            IsHitTestVisible = false;
        }

        public void Update(DropType dropType)
        {
            if (_layer == null)
            {
                _layer = AdornerLayer.GetAdornerLayer(AdornedElement);
                _layer.Add(this);
            }
            
            if (_parentAdorner?._dropType == DropType.Inside)
                _parentAdorner.Remove();

            _dropType = dropType;
            _layer.Update(AdornedElement);
            Visibility = Visibility.Visible;
        }
        public void Remove()
        {
            if (_parentAdorner?._dropType == DropType.Inside)
                _parentAdorner.Remove();

            Visibility = Visibility.Collapsed;
            _dropType = 0;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawBox(drawingContext);
            DrawLine(drawingContext);
        }
        private void DrawBox(DrawingContext drawingContext)
        {
            if (_dropType != DropType.Inside)
            {
                _parentAdorner?.Update(DropType.Inside);
                return;
            }
            
            var rect = new Rect(_treeViewItemBorder.DesiredSize);

            var brush = new SolidColorBrush(Colors.Blue) { Opacity = 0.5 };
            var pen = new Pen(new SolidColorBrush(Colors.White) { Opacity = 0.5 }, 2);

            drawingContext.DrawRoundedRectangle(brush, pen, rect, 2, 2);
        }
        private void DrawLine(DrawingContext drawingContext)
        {
            if (_dropType == DropType.Inside)
                return;

            var rect = new Rect(AdornedElement.RenderSize);
            var start = new Point(rect.Left, _dropType == DropType.Above ? rect.Top + 3 : rect.Bottom);
            var end = new Point(Math.Max(70, rect.Width), start.Y);

            var elipseColor = Brushes.Blue;
            var highlightLineColor = new Pen(Brushes.Blue, 2);
            
            drawingContext.DrawLine(highlightLineColor, start, end);
            drawingContext.DrawEllipse(elipseColor, highlightLineColor, start, 2, 2);
        }
    }
}